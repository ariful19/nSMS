/**
 * Global error handler used as the final Express middleware.
 * Normalizes known errors (CSRF, validation) and logs unexpected ones.
 */
module.exports = (err, req, res, next) => {
  if (res.headersSent) {
    return next(err);
  }

  const status = err.status || err.statusCode || 500;
  let message = err.message || "Unexpected error";

  if (err.code === "EBADCSRFTOKEN") {
    message = "Form has expired or is invalid. Please try again.";
  }

  if (status >= 500) {
    // eslint-disable-next-line no-console
    console.error(err);
  }

  res.status(status).render("pages/error", {
    title: "Error",
    message,
    errorId: status >= 500 ? req.id : undefined,
  });
};
