const bcrypt = require("bcryptjs");
const { prisma } = require("../db/client");
const config = require("../config");

exports.showLogin = (req, res) => {
  const formData = req.session?.loginForm || {};
  if (req.session) {
    delete req.session.loginForm;
  }

  res.render("pages/auth/login", {
    title: "Sign in",
    formData,
  });
};

exports.handleLogin = async (req, res, next) => {
  const { email, password } = req.body;
  const trimmedEmail = typeof email === "string" ? email.trim().toLowerCase() : "";
  const sanitizedPassword = typeof password === "string" ? password : "";

  if (req.session) {
    req.session.loginForm = { email: trimmedEmail };
  }

  if (!trimmedEmail || !sanitizedPassword) {
    if (typeof req.flash === "function") {
      req.flash("danger", "Email and password are required.");
    }
    return res.redirect("/auth/login");
  }

  try {
    const user = await prisma.user.findUnique({
      where: { email: trimmedEmail },
      include: {
        roles: {
          include: {
            role: true,
          },
        },
      },
    });

    if (!user || !user.isActive) {
      if (typeof req.flash === "function") {
        req.flash("danger", "Invalid email or password.");
      }
      return res.redirect("/auth/login");
    }

    const passwordMatches = await bcrypt.compare(
      sanitizedPassword,
      user.passwordHash
    );

    if (!passwordMatches) {
      if (typeof req.flash === "function") {
        req.flash("danger", "Invalid email or password.");
      }
      return res.redirect("/auth/login");
    }

    const roleSummaries = user.roles.map((userRole) => ({
      id: userRole.role.id,
      name: userRole.role.name,
    }));

    const roleIds = roleSummaries.map((role) => role.id);
    const roleNames = roleSummaries.map((role) => role.name.toLowerCase());

    req.session.userId = user.id;
    req.session.roleIds = roleIds;
    req.session.roleNames = roleNames;
    req.session.user = {
      id: user.id,
      email: user.email,
      username: user.username,
      displayName: user.username || user.email,
      roles: roleSummaries,
    };

    if (typeof req.flash === "function") {
      req.flash("success", `Welcome back ${req.session.user.displayName}!`);
    }

    const redirectTo = req.session.returnTo || config.auth.defaultRedirects.loginSuccess;
    if (req.session) {
      delete req.session.loginForm;
      delete req.session.returnTo;
    }

    await prisma.user.update({
      where: { id: user.id },
      data: {
        lastLoginAt: new Date(),
      },
    });

    return req.session.save((saveError) => {
      if (saveError) {
        return next(saveError);
      }
      return res.redirect(redirectTo);
    });
  } catch (error) {
    return next(error);
  }
};

exports.handleLogout = (req, res, next) => {
  if (!req.session) {
    return res.redirect(config.auth.defaultRedirects.logoutRedirect);
  }

  req.session.regenerate((error) => {
    if (error) {
      return next(error);
    }

    if (typeof req.flash === "function") {
      req.flash("success", "You have been signed out.");
    }
    return res.redirect(config.auth.defaultRedirects.logoutRedirect);
  });
};
