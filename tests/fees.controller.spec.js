jest.mock("../src/services/feeService");
jest.mock("../src/services/studentService");

const request = require("supertest");
const feeService = require("../src/services/feeService");
const studentService = require("../src/services/studentService");
const feeRoutes = require("../src/routes/feeRoutes");
const { createRouterTestApp } = require("./support/testAppFactory");

describe("Fee routes", () => {
  let app;

  beforeEach(() => {
    jest.clearAllMocks();
    app = createRouterTestApp(feeRoutes, { basePath: "/fees" });
  });

  test("renders index with categories and recent entries", async () => {
    const categories = [{ id: 1, name: "Tuition", isActive: true }];
    const recentEntries = [
      { id: 10, entryType: "CHARGE", entryDate: "2024-09-01", amount: 350, description: "Fall term" },
    ];

    feeService.listCategories.mockResolvedValue(categories);
    feeService.getRecentLedgerEntries.mockResolvedValue(recentEntries);

    const response = await request(app).get("/fees").set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.view).toBe("pages/fees/index");
    expect(response.body.categories).toEqual(categories);
    expect(response.body.recentEntries).toEqual(recentEntries);
    expect(feeService.listCategories).toHaveBeenCalledWith({ includeInactive: true });
    expect(feeService.getRecentLedgerEntries).toHaveBeenCalledWith(10);
  });

  test("blocks unauthorized users", async () => {
    const response = await request(app).get("/fees").set("x-test-session", "teacher");

    expect(response.status).toBe(403);
    expect(response.body.view).toBe("pages/error");
    expect(response.body.message).toContain("permission");
    expect(feeService.listCategories).not.toHaveBeenCalled();
  });

  test("redirects unauthenticated visitors to login", async () => {
    const response = await request(app).get("/fees");

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/auth/login");
  });

  test("supports student search API", async () => {
    const results = [{ id: 1, label: "Student One" }];
    studentService.searchStudents.mockResolvedValue(results);

    const response = await request(app)
      .get("/fees/api/students")
      .query({ term: "Stu" })
      .set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.results).toEqual(results);
    expect(studentService.searchStudents).toHaveBeenCalledWith("Stu", 10);
  });

  test("returns ledger data as JSON", async () => {
    const ledger = {
      student: { id: 5, studentNumber: "STU-0005" },
      entries: [],
      totals: { charges: 0, payments: 0, adjustments: 0 },
      balance: 0,
    };
    const categories = [{ id: 1, name: "Tuition" }];

    feeService.getStudentLedger.mockResolvedValue(ledger);
    feeService.listCategories.mockResolvedValue(categories);

    const response = await request(app)
      .get("/fees/ledger/5")
      .set("x-test-session", "staff")
      .set("accept", "application/json");

    expect(response.status).toBe(200);
    expect(response.body.ledger).toEqual(ledger);
    expect(response.body.categories).toEqual(categories);
    expect(feeService.getStudentLedger).toHaveBeenCalledWith(5, {});
    expect(feeService.listCategories).toHaveBeenCalledWith({ includeInactive: false });
  });

  test("creates a charge and returns entries", async () => {
    const entries = [{ id: 99, entryType: "CHARGE", amount: 100 }];
    feeService.createCharges.mockResolvedValue(entries);

    const response = await request(app)
      .post("/fees/ledger/8/charges")
      .set("x-test-session", "staff")
      .set("accept", "application/json")
      .send({ lineItems: [{ amount: "100.00", entryDate: "2024-09-01" }] });

    expect(response.status).toBe(200);
    expect(response.body.entries).toEqual(entries);
    expect(feeService.createCharges).toHaveBeenCalledWith(8, expect.any(Array), null);
  });

  test("validates charge payload", async () => {
    const response = await request(app)
      .post("/fees/ledger/9/charges")
      .set("x-test-session", "staff")
      .set("accept", "application/json")
      .send({ lineItems: [] });

    expect(response.status).toBe(400);
    expect(response.body.error).toMatch(/Add at least one line item/);
    expect(feeService.createCharges).not.toHaveBeenCalled();
  });
});
