jest.mock("../src/services/libraryService");
jest.mock("../src/services/studentService");

const request = require("supertest");
const libraryService = require("../src/services/libraryService");
const studentService = require("../src/services/studentService");
const libraryRoutes = require("../src/routes/libraryRoutes");
const { createRouterTestApp } = require("./support/testAppFactory");

describe("Library routes", () => {
  let app;

  beforeEach(() => {
    jest.clearAllMocks();
    app = createRouterTestApp(libraryRoutes, { basePath: "/library" });
  });

  test("renders index view with catalog data", async () => {
    const categories = [{ id: 1, name: "Fiction" }];
    const books = [{ id: 10, title: "Sample", copySummary: { available: 1, total: 2 } }];
    const recentLoans = [{ id: 100, book: { title: "Sample" } }];

    libraryService.listCategories.mockResolvedValue(categories);
    libraryService.searchBooks.mockResolvedValue(books);
    libraryService.getRecentLoans.mockResolvedValue(recentLoans);

    const response = await request(app).get("/library").set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.view).toBe("pages/library/index");
    expect(response.body.categories).toEqual(categories);
    expect(response.body.books).toEqual(books);
    expect(response.body.recentLoans).toEqual(recentLoans);
    expect(libraryService.listCategories).toHaveBeenCalledWith({ includeInactive: false });
    expect(libraryService.searchBooks).toHaveBeenCalledWith("", { limit: 25 });
    expect(libraryService.getRecentLoans).toHaveBeenCalledWith(10);
  });

  test("blocks unauthorized roles", async () => {
    const response = await request(app).get("/library").set("x-test-session", "teacher");

    expect(response.status).toBe(403);
    expect(response.body.view).toBe("pages/error");
  });

  test("redirects unauthenticated visitors", async () => {
    const response = await request(app).get("/library");

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/auth/login");
  });

  test("supports catalog search API", async () => {
    const results = [{ id: 1, title: "Book" }];
    libraryService.searchBooks.mockResolvedValue(results);

    const response = await request(app)
      .get("/library/api/books")
      .query({ term: "Book", categoryId: "2" })
      .set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.results).toEqual(results);
    expect(libraryService.searchBooks).toHaveBeenCalledWith("Book", { categoryId: 2 });
  });

  test("returns book detail as JSON when requested", async () => {
    const detail = {
      book: { id: 10, title: "Book" },
      activeLoans: [],
      recentLoans: [],
    };
    const categories = [{ id: 1, name: "Fiction" }];
    libraryService.getBookById.mockResolvedValue(detail);
    libraryService.listCategories.mockResolvedValue(categories);

    const response = await request(app)
      .get("/library/books/10")
      .set("accept", "application/json")
      .set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.book).toEqual(detail.book);
    expect(response.body.categories).toEqual(categories);
  });

  test("validates book creation payload", async () => {
    const response = await request(app)
      .post("/library/books")
      .set("x-test-session", "staff")
      .send({ title: "" })
      .set("accept", "application/json");

    expect(response.status).toBe(400);
    expect(response.body.error).toMatch(/Title is required/);
    expect(libraryService.createBook).not.toHaveBeenCalled();
  });

  test("issues loans and returns 400 on service error", async () => {
    libraryService.issueLoan.mockRejectedValue(new Error("Copy is not available"));

    const response = await request(app)
      .post("/library/loans")
      .set("x-test-session", "staff")
      .set("accept", "application/json")
      .send({ copyId: 5, studentId: 2 });

    expect(response.status).toBe(400);
    expect(response.body.error).toContain("Copy is not available");
    expect(libraryService.issueLoan).toHaveBeenCalledWith(5, 2, expect.any(Object));
  });

  test("supports student search for borrowers", async () => {
    const results = [{ id: 5, label: "Student" }];
    studentService.searchStudents.mockResolvedValue(results);

    const response = await request(app)
      .get("/library/api/students")
      .query({ term: "Stu" })
      .set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.results).toEqual(results);
    expect(studentService.searchStudents).toHaveBeenCalledWith("Stu", 10);
  });
});
