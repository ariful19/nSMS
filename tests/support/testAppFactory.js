const express = require("express");

function defaultSessionResolver(req) {
  const preset = req.get("x-test-session");
  if (preset === "staff") {
    return { userId: 200, roleNames: ["admin", "staff"] };
  }
  if (preset === "teacher") {
    return { userId: 201, roleNames: ["teacher"] };
  }
  if (preset === "student") {
    return { userId: 202, roleNames: ["student"] };
  }
  return null;
}

function createRouterTestApp(router, { basePath = "/", sessionResolver = defaultSessionResolver } = {}) {
  const app = express();

  app.use(express.urlencoded({ extended: true }));
  app.use(express.json());

  app.use((req, res, next) => {
    req.flash = jest.fn();
    res.locals = res.locals || {};
    res.locals.csrfToken = "test-token";
    res.locals.hasRole = (role) => {
      const normalized = typeof role === "string" ? role.toLowerCase() : "";
      const roleNames = Array.isArray(req.session?.roleNames) ? req.session.roleNames : [];
      return normalized ? roleNames.includes(normalized) : false;
    };
    next();
  });

  app.use((req, res, next) => {
    const sessionData = sessionResolver ? sessionResolver(req) : null;
    if (sessionData) {
      req.session = sessionData;
    }
    next();
  });

  app.use((req, res, next) => {
    res.render = (view, options = {}) => {
      res.status(res.statusCode || 200);
      return res.json({ view, ...options });
    };
    next();
  });

  app.use(basePath, router);

  app.use((error, req, res, next) => {
    // eslint-disable-next-line no-console
    console.error("Router test app error", error);
    if (res.headersSent) {
      return next(error);
    }
    return res.status(500).json({ message: error.message });
  });

  return app;
}

module.exports = {
  createRouterTestApp,
  defaultSessionResolver,
};
