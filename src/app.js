const fs = require("fs");
const path = require("path");
const express = require("express");
const helmet = require("helmet");
const morgan = require("morgan");
const session = require("express-session");
const cookieParser = require("cookie-parser");
const csrf = require("csurf");
const expressLayouts = require("express-ejs-layouts");
const SQLiteStoreFactory = require("connect-sqlite3");

const config = require("./config");
const rootRouter = require("./routes");
const notFound = require("./middleware/notFound");
const errorHandler = require("./middleware/errorHandler");

const SQLiteStore = SQLiteStoreFactory(session);

const app = express();

const accessLogStream = config.isProduction
  ? fs.createWriteStream(path.join(config.paths.logsDir, "access.log"), { flags: "a" })
  : null;

app.set("trust proxy", 1);
app.set("views", config.paths.views);
app.set("view engine", "ejs");
app.set("layout", "layouts/base");

app.use(helmet());
app.use(expressLayouts);
app.use(
  morgan(config.isProduction ? "combined" : "dev", {
    stream: accessLogStream || process.stdout,
  })
);

app.use(express.urlencoded({ extended: true }));
app.use(express.json());
app.use(cookieParser());

const sessionStore = new SQLiteStore({
  dir: config.session.store.dir,
  db: config.session.store.db,
});

app.use(
  session({
    name: config.session.cookieName,
    secret: config.session.secret,
    resave: false,
    saveUninitialized: false,
    store: sessionStore,
    cookie: {
      httpOnly: true,
      sameSite: "lax",
      secure: config.isProduction,
      maxAge: config.session.ttlMs,
    },
  })
);

const csrfProtection = csrf({ cookie: false });
app.use(csrfProtection);
app.use((req, res, next) => {
  if (typeof req.csrfToken === "function") {
    try {
      res.locals.csrfToken = req.csrfToken();
    } catch (error) {
      return next(error);
    }
  }

  res.locals.currentUser = req.session?.user || null;
  res.locals.isAuthenticated = Boolean(req.session?.userId);
  res.locals.env = config.env;
  next();
});

app.use(express.static(config.paths.public));

app.use("/", rootRouter);

app.use(notFound);
app.use(errorHandler);

module.exports = app;
