jest.mock("../src/services/noticeService");

const request = require("supertest");
const noticeService = require("../src/services/noticeService");
const noticeRoutes = require("../src/routes/noticeRoutes");
const { createRouterTestApp } = require("./support/testAppFactory");

describe("Notice routes", () => {
  let app;

  beforeEach(() => {
    jest.clearAllMocks();
    noticeService.getNoticeLookups.mockResolvedValue({
      statuses: ["PUBLISHED", "DRAFT"],
      roles: [{ id: 1, name: "Student" }],
    });
    app = createRouterTestApp(noticeRoutes, { basePath: "/notices" });
  });

  test("renders index for authorized staff", async () => {
    const notices = [
      {
        id: 1,
        title: "Welcome back",
        summary: "Term overview",
        status: "PUBLISHED",
        publishAt: new Date().toISOString(),
        audiences: [],
      },
    ];

    noticeService.listNotices.mockResolvedValue(notices);

    const response = await request(app).get("/notices").set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.view).toBe("pages/notices/index");
    expect(response.body.notices).toEqual(notices);
    expect(noticeService.listNotices).toHaveBeenCalledWith({
      search: undefined,
      status: undefined,
      includeExpired: false,
      audienceRoleIds: [],
    });
  });

  test("blocks creation form for teachers", async () => {
    const response = await request(app).get("/notices/create").set("x-test-session", "teacher");

    expect(response.status).toBe(403);
    expect(response.body.view).toBe("pages/error");
  });

  test("creates notice and redirects", async () => {
    noticeService.createNotice.mockResolvedValue({ id: 42 });

    const response = await request(app)
      .post("/notices/create")
      .set("x-test-session", "staff")
      .send({
        title: "New notice",
        content: "Details",
        status: "PUBLISHED",
      });

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/notices/42");
    expect(noticeService.createNotice).toHaveBeenCalledWith(
      expect.objectContaining({ title: "New notice", content: "Details", status: "PUBLISHED" })
    );
  });

  test("rejects unauthenticated visitors", async () => {
    const response = await request(app).get("/notices");
    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/auth/login");
  });
});
