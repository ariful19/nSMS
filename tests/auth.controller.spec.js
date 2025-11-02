const request = require("supertest");
const bcrypt = require("bcryptjs");

jest.mock("../src/db/client", () => ({
  prisma: {
    user: {
      findUnique: jest.fn(),
      update: jest.fn(),
    },
  },
}));

const { prisma } = require("../src/db/client");
const authRoutes = require("../src/routes/authRoutes");
const { createRouterTestApp } = require("./support/testAppFactory");

describe("Auth routes", () => {
  let app;

  beforeEach(() => {
    jest.clearAllMocks();
    app = createRouterTestApp(authRoutes, { 
      basePath: "/auth",
      sessionResolver: null // Don't auto-authenticate
    });
  });

  describe("GET /auth/login", () => {
    test("renders login page for unauthenticated users", async () => {
      const response = await request(app).get("/auth/login");

      expect(response.status).toBe(200);
      expect(response.body.view).toBe("pages/auth/login");
      expect(response.body.title).toContain("Sign in");
    });

    test("renders login page with expected structure", async () => {
      const response = await request(app).get("/auth/login");

      expect(response.status).toBe(200);
      expect(response.body.view).toBe("pages/auth/login");
      // CSRF token is provided by testAppFactory in res.locals
    });
  });

  describe("POST /auth/login", () => {
    test("rejects login with invalid email", async () => {
      prisma.user.findUnique.mockResolvedValue(null);

      const response = await request(app)
        .post("/auth/login")
        .send({
          email: "nonexistent@example.com",
          password: "AnyPassword123!",
          _csrf: "test-token"
        });

      expect(response.status).toBe(302);
      expect(response.headers.location).toContain("/auth/login");
      expect(prisma.user.findUnique).toHaveBeenCalledWith(
        expect.objectContaining({
          where: { email: "nonexistent@example.com" }
        })
      );
    });

    test("rejects login with invalid password", async () => {
      const hashedPassword = await bcrypt.hash("CorrectPass123!", 10);
      const mockUser = {
        id: 1,
        email: "admin@example.com",
        passwordHash: hashedPassword,
        isActive: true,
        roles: [
          { role: { id: 1, name: "Admin" } }
        ]
      };

      prisma.user.findUnique.mockResolvedValue(mockUser);

      const response = await request(app)
        .post("/auth/login")
        .send({
          email: "admin@example.com",
          password: "WrongPassword123!",
          _csrf: "test-token"
        });

      expect(response.status).toBe(302);
      expect(response.headers.location).toContain("/auth/login");
    });

    test("rejects login for inactive user", async () => {
      const hashedPassword = await bcrypt.hash("ValidPass123!", 10);
      const mockUser = {
        id: 1,
        email: "inactive@example.com",
        passwordHash: hashedPassword,
        isActive: false,
        roles: []
      };

      prisma.user.findUnique.mockResolvedValue(mockUser);

      const response = await request(app)
        .post("/auth/login")
        .send({
          email: "inactive@example.com",
          password: "ValidPass123!",
          _csrf: "test-token"
        });

      expect(response.status).toBe(302);
      expect(response.headers.location).toContain("/auth/login");
    });

    test("requires email field", async () => {
      const response = await request(app)
        .post("/auth/login")
        .send({
          password: "ValidPass123!",
          _csrf: "test-token"
        });

      expect(response.status).toBe(302);
      expect(response.headers.location).toContain("/auth/login");
    });

    test("requires password field", async () => {
      const response = await request(app)
        .post("/auth/login")
        .send({
          email: "admin@example.com",
          _csrf: "test-token"
        });

      expect(response.status).toBe(302);
      expect(response.headers.location).toContain("/auth/login");
    });
  });
});
