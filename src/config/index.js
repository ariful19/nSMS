const fs = require("fs");
const path = require("path");
const dotenv = require("dotenv");

dotenv.config();

const env = process.env.NODE_ENV || "development";
const isProduction = env === "production";

const appRoot = process.cwd();
const dataDir = process.env.APP_DATA_DIR
  ? path.resolve(process.env.APP_DATA_DIR)
  : path.join(appRoot, "var", "data");
const sessionDir = process.env.SESSION_STORE_DIR
  ? path.resolve(process.env.SESSION_STORE_DIR)
  : path.join(dataDir, "sessions");
const logsDir = process.env.LOGS_DIR
  ? path.resolve(process.env.LOGS_DIR)
  : path.join(dataDir, "logs");

const ensurePath = (dir) => {
  if (!fs.existsSync(dir)) {
    fs.mkdirSync(dir, { recursive: true });
  }
  return dir;
};

const ensuredDataDir = ensurePath(dataDir);
const ensuredSessionDir = ensurePath(sessionDir);
const ensuredLogsDir = ensurePath(logsDir);

module.exports = {
  env,
  isProduction,
  port: Number(process.env.PORT) || 3000,
  databaseUrl: process.env.DATABASE_URL,
  session: {
    cookieName: process.env.SESSION_COOKIE_NAME || "nsms.sid",
    secret: process.env.SESSION_SECRET || "insecure_dev_secret",
    ttlMs: Number(process.env.SESSION_MAX_AGE_MS) || 1000 * 60 * 60,
    store: {
      dir: ensuredSessionDir,
      db: process.env.SESSION_STORE_DB || "sessions.sqlite",
    },
  },
  csrf: {
    secret: process.env.CSRF_SECRET,
  },
  paths: {
    root: appRoot,
    dataDir: ensuredDataDir,
    sessionDir: ensuredSessionDir,
    logsDir: ensuredLogsDir,
    views: path.join(__dirname, "..", "views"),
    public: path.join(__dirname, "..", "public"),
  },
};
