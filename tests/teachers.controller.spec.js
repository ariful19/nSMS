const request = require("supertest");

jest.mock("../src/services/teacherService");

const teacherService = require("../src/services/teacherService");
const teacherRoutes = require("../src/routes/teacherRoutes");
const { createRouterTestApp } = require("./support/testAppFactory");

describe("Teacher routes", () => {
  let app;

  beforeEach(() => {
    jest.clearAllMocks();
    app = createRouterTestApp(teacherRoutes, { basePath: "/teachers" });
  });

  test("lists teachers for authorized staff", async () => {
    const teachers = [
      {
        id: 10,
        employeeNumber: "EMP-0456",
        person: { firstName: "Daniel", lastName: "Okafor" },
      },
    ];
    const lookups = {
      staffTypes: [{ id: 1, name: "Teacher" }],
      employmentStatuses: [{ id: 2, name: "Active" }],
    };

    teacherService.listTeachers.mockResolvedValue(teachers);
    teacherService.getTeacherLookups.mockResolvedValue({
      genders: [],
      ...lookups,
    });

    const response = await request(app)
      .get("/teachers")
      .set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.view).toBe("pages/teachers/index");
    expect(response.body.teachers).toEqual(teachers);
    expect(teacherService.listTeachers).toHaveBeenCalledWith({ search: undefined });
  });

  test("rejects teacher creation with validation errors", async () => {
    teacherService.getTeacherLookups.mockResolvedValue({
      genders: [],
      staffTypes: [],
      employmentStatuses: [],
    });

    const response = await request(app)
      .post("/teachers/create")
      .set("x-test-session", "staff")
      .send({
        firstName: "",
        lastName: "",
        staffTypeId: "",
        employmentStatusId: "",
      });

    expect(response.status).toBe(422);
    expect(response.body.view).toBe("pages/teachers/form");
    expect(response.body.errors).toEqual(
      expect.arrayContaining([
        "First name is required.",
        "Last name is required.",
        "Staff type is required.",
        "Employment status is required.",
      ])
    );
    expect(teacherService.createTeacher).not.toHaveBeenCalled();
  });

  test("creates a teacher/staff member and redirects on success", async () => {
    teacherService.getTeacherLookups.mockResolvedValue({
      genders: [],
      staffTypes: [],
      employmentStatuses: [],
    });
    teacherService.createTeacher.mockResolvedValue({ id: 77 });

    const response = await request(app)
      .post("/teachers/create")
      .set("x-test-session", "staff")
      .send({
        firstName: "Amina",
        lastName: "Chowdhury",
        staffTypeId: "1",
        employmentStatusId: "1",
      });

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/teachers/77");
    expect(teacherService.createTeacher).toHaveBeenCalledWith(
      expect.objectContaining({
        person: expect.objectContaining({
          firstName: "Amina",
          lastName: "Chowdhury",
        }),
        teacher: expect.objectContaining({
          staffTypeId: 1,
          employmentStatusId: 1,
        }),
      })
    );
  });

  test("redirects unauthenticated visitors to login", async () => {
    const response = await request(app).get("/teachers");

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/auth/login");
  });

  test("blocks users without a staff role", async () => {
    const response = await request(app)
      .get("/teachers")
      .set("x-test-session", "teacher");

    expect(response.status).toBe(403);
    expect(response.body.view).toBe("pages/error");
    expect(response.body.message).toContain("do not have permission");
    expect(teacherService.listTeachers).not.toHaveBeenCalled();
  });
});
