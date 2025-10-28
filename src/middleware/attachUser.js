const { prisma } = require("../db/client");

module.exports = async (req, res, next) => {
  res.locals.csrfToken = null;
  res.locals.currentUser = null;
  res.locals.isAuthenticated = false;
  res.locals.hasRole = () => false;

  if (typeof req.csrfToken === "function") {
    try {
      res.locals.csrfToken = req.csrfToken();
    } catch (error) {
      return next(error);
    }
  }

  if (!req.session || !req.session.userId) {
    return next();
  }

  try {
    const user = await prisma.user.findUnique({
      where: { id: req.session.userId },
      include: {
        roles: {
          include: {
            role: true,
          },
        },
      },
    });

    if (!user || !user.isActive) {
      if (req.session) {
        req.session.userId = undefined;
        req.session.user = undefined;
        req.session.roleIds = undefined;
        req.session.roleNames = undefined;
      }
      return next();
    }

    const roleSummaries = user.roles.map((userRole) => ({
      id: userRole.role.id,
      name: userRole.role.name,
    }));

    const roleIds = roleSummaries.map((role) => role.id);
    const roleNames = roleSummaries.map((role) => role.name.toLowerCase());

    const sessionUser = {
      id: user.id,
      email: user.email,
      username: user.username,
      displayName: user.username || user.email,
      roles: roleSummaries,
    };

    req.session.user = sessionUser;
    req.session.roleIds = roleIds;
    req.session.roleNames = roleNames;

    res.locals.currentUser = sessionUser;
    res.locals.isAuthenticated = true;
    res.locals.hasRole = (role) => {
      if (!role) {
        return false;
      }
      const normalized = role.toLowerCase();
      return roleNames.includes(normalized);
    };
  } catch (error) {
    return next(error);
  }

  return next();
};
