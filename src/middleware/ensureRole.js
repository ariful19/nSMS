module.exports = (requiredRoles = []) => {
  const normalizedRequired = requiredRoles.map((role) => role.toLowerCase());

  return (req, res, next) => {
    if (!req.session || !req.session.userId) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Please sign in to continue.");
      }
      return res.redirect("/auth/login");
    }

    const roleNames = Array.isArray(req.session.roleNames)
      ? req.session.roleNames
      : [];

    const hasAllRoles = normalizedRequired.every((role) =>
      roleNames.includes(role)
    );

    if (!hasAllRoles) {
      if (req.accepts(["html", "json"]) === "json") {
        return res.status(403).json({ message: "Forbidden" });
      }
      return res.status(403).render("pages/error", {
        title: "Forbidden",
        message: "You do not have permission to view this resource.",
      });
    }

    return next();
  };
};
