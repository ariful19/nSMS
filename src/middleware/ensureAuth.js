module.exports = (req, res, next) => {
  if (req.session && req.session.userId) {
    return next();
  }

  if (req.session) {
    req.session.returnTo = req.originalUrl;
  }

  if (typeof req.flash === "function") {
    req.flash("warning", "Please sign in to continue.");
  }

  return res.redirect("/auth/login");
};
