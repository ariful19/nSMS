const fs = require("fs");
const path = require("path");
const os = require("os");
const request = require("supertest");

let dataDir;
let sessionStore;
let app;

describe("Application bootstrap", () => {
  beforeAll(() => {
    dataDir = fs.mkdtempSync(path.join(os.tmpdir(), "nsms-app-test-"));
    process.env.NODE_ENV = "test";
    process.env.APP_DATA_DIR = dataDir;
    process.env.SESSION_STORE_DIR = path.join(dataDir, "sessions");
    process.env.LOGS_DIR = path.join(dataDir, "logs");
    process.env.SESSION_SECRET = "test_secret";
    process.env.DATABASE_URL = "file:./var/test.db";

    jest.resetModules();
    jest.doMock("connect-sqlite3", () => (session) => {
      const Store = jest.requireActual("connect-sqlite3")(session);
      return class TestStore extends Store {
        constructor(options) {
          super(options);
          sessionStore = this;
        }
      };
    });
    // eslint-disable-next-line global-require
    app = require("../src/app");
  });

  afterAll(async () => {
    if (sessionStore) {
      await new Promise((resolve, reject) => sessionStore.db.close((error) => (error ? reject(error) : resolve())));
    }
    fs.rmSync(dataDir, { recursive: true, force: true });
    jest.dontMock("connect-sqlite3");
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

  it.each([undefined, "invalid-token"])("renders a friendly error for an invalid CSRF token (%s)", async (token) => {
    const agent = request.agent(app);
    await agent.get("/auth/login");
    const response = await agent.post("/auth/login").type("form").send(token ? { _csrf: token } : {});

    expect(response.status).toBe(403);
    expect(response.text).toContain("Form has expired or is invalid. Please try again.");
    expect(response.text).toContain("Return home");
    expect(response.text).not.toContain("ReferenceError");
    expect(response.text).not.toContain("isAuthenticated is not defined");
  });

  it("recovers after a rejected CSRF submission", async () => {
    const agent = request.agent(app);
    await agent.get("/auth/login");
    const rejected = await agent.post("/auth/login").type("form").send({});
    expect(rejected.status).toBe(403);

    const login = await agent.get("/auth/login");
    const token = login.text.match(/name="_csrf" value="([^"]+)"/)[1];
    const response = await agent.post("/auth/login").type("form").send({ _csrf: token });
    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/auth/login");
    const recovered = await agent.get("/auth/login");
    expect(recovered.text).toContain("Email and password are required.");
    expect(recovered.text).not.toContain("Form has expired or is invalid. Please try again.");
  });

});
