const fs = require("fs");
const path = require("path");
const request = require("supertest");

const dataDir = path.join(__dirname, "..", "var", "test-data");

let app;

describe("Application bootstrap", () => {
  beforeAll(() => {
    process.env.NODE_ENV = "test";
    process.env.APP_DATA_DIR = dataDir;
    process.env.SESSION_STORE_DIR = path.join(dataDir, "sessions");
    process.env.LOGS_DIR = path.join(dataDir, "logs");
    process.env.SESSION_SECRET = "test_secret";
    process.env.DATABASE_URL = "file:./var/test.db";

    jest.resetModules();
    // eslint-disable-next-line global-require
    app = require("../src/app");
  });

  afterAll(() => {
    if (fs.existsSync(path.join(__dirname, "..", "var"))) {
      fs.rmSync(path.join(__dirname, "..", "var"), { recursive: true, force: true });
    }
  });

  it("renders the home page", async () => {
    const response = await request(app).get("/");

    expect(response.status).toBe(200);
    expect(response.text).toContain("Welcome");
  });

  it("renders the login form with a CSRF token", async () => {
    const response = await request(app).get("/auth/login");

    expect(response.status).toBe(200);
    expect(response.text).toContain("name=\"_csrf\"");
    expect(response.text).toContain("Sign in");
  });
});
