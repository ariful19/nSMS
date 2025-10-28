const request = require("supertest");

jest.mock("../src/services/studentService");

const studentService = require("../src/services/studentService");
const studentRoutes = require("../src/routes/studentRoutes");
const { createRouterTestApp } = require("./support/testAppFactory");

describe("Student routes", () => {
  let app;

  beforeEach(() => {
    jest.clearAllMocks();
    app = createRouterTestApp(studentRoutes, { basePath: "/students" });
  });

  test("lists students for authorized staff", async () => {
    const students = [
      {
        id: 1,
        studentNumber: "STU-0001",
        person: { firstName: "Sara", lastName: "Nguyen" },
      },
    ];
    const lookups = {
      statuses: [{ id: 1, name: "Active" }],
      gradeLevels: [{ id: 9, name: "Grade 9" }],
    };

    studentService.listStudents.mockResolvedValue(students);
    studentService.getStudentLookups.mockResolvedValue({
      genders: [],
      ...lookups,
    });

    const response = await request(app)
      .get("/students")
      .set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.view).toBe("pages/students/index");
    expect(response.body.students).toEqual(students);
    expect(studentService.listStudents).toHaveBeenCalledWith({ search: undefined });
  });

  test("rejects student creation with validation errors", async () => {
    studentService.getStudentLookups.mockResolvedValue({
      genders: [],
      statuses: [],
      gradeLevels: [],
    });

    const response = await request(app)
      .post("/students/create")
      .set("x-test-session", "staff")
      .send({
        firstName: "",
        lastName: "",
        enrollmentStatusId: "",
      });

    expect(response.status).toBe(422);
    expect(response.body.view).toBe("pages/students/form");
    expect(response.body.errors).toEqual(
      expect.arrayContaining([
        "First name is required.",
        "Last name is required.",
        "Enrollment status is required.",
      ])
    );
    expect(studentService.createStudent).not.toHaveBeenCalled();
  });

  test("creates a student and redirects on success", async () => {
    studentService.getStudentLookups.mockResolvedValue({
      genders: [],
      statuses: [],
      gradeLevels: [],
    });
    studentService.createStudent.mockResolvedValue({ id: 42 });

    const response = await request(app)
      .post("/students/create")
      .set("x-test-session", "staff")
      .send({
        firstName: "Jamie",
        lastName: "Lee",
        enrollmentStatusId: "1",
      });

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/students/42");
    expect(studentService.createStudent).toHaveBeenCalledWith(
      expect.objectContaining({
        person: expect.objectContaining({ firstName: "Jamie", lastName: "Lee" }),
        student: expect.objectContaining({ enrollmentStatusId: 1 }),
      })
    );
  });

  test("redirects unauthenticated visitors to login", async () => {
    const response = await request(app).get("/students");

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/auth/login");
  });

  test("blocks users without a staff role", async () => {
    const response = await request(app)
      .get("/students")
      .set("x-test-session", "teacher");

    expect(response.status).toBe(403);
    expect(response.body.view).toBe("pages/error");
    expect(response.body.message).toContain("do not have permission");
    expect(studentService.listStudents).not.toHaveBeenCalled();
  });
});
