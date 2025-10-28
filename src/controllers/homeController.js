/**
 * Landing page controller.
 * For now this simply renders a placeholder until
 * the authentication flow is implemented in Phase 1.
 */
exports.getHome = (req, res) => {
  res.render("pages/home", {
    title: "Welcome",
  });
};
