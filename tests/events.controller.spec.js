jest.mock("../src/services/eventService");

const request = require("supertest");
const eventService = require("../src/services/eventService");
const eventRoutes = require("../src/routes/eventRoutes");
const { createRouterTestApp } = require("./support/testAppFactory");

describe("Event routes", () => {
  let app;

  beforeEach(() => {
    jest.clearAllMocks();
    eventService.getEventLookups.mockResolvedValue({
      statuses: ["PUBLISHED", "SCHEDULED"],
      visibilities: ["INTERNAL"],
      roles: [{ id: 1, name: "Student" }],
      rsvpStatuses: ["GOING", "INTERESTED", "DECLINED"],
    });
    app = createRouterTestApp(eventRoutes, { basePath: "/events" });
  });

  test("renders event index for staff", async () => {
    const events = [
      {
        id: 10,
        title: "Open House",
        summary: "Tour the campus",
        status: "PUBLISHED",
        startAt: new Date().toISOString(),
        audiences: [],
      },
    ];

    eventService.listEvents.mockResolvedValue(events);

    const response = await request(app).get("/events").set("x-test-session", "staff");

    expect(response.status).toBe(200);
    expect(response.body.view).toBe("pages/events/index");
    expect(response.body.events).toEqual(events);
    expect(eventService.listEvents).toHaveBeenCalled();
  });

  test("teachers cannot access creation form", async () => {
    const response = await request(app).get("/events/create").set("x-test-session", "teacher");

    expect(response.status).toBe(403);
    expect(response.body.view).toBe("pages/error");
  });

  test("creates an event and redirects", async () => {
    eventService.createEvent.mockResolvedValue({ id: 77 });

    const response = await request(app)
      .post("/events/create")
      .set("x-test-session", "staff")
      .send({
        title: "Retreat",
        description: "Planning session",
        startAt: "2025-01-01T10:00",
      });

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/events/77");
    expect(eventService.createEvent).toHaveBeenCalledWith(
      expect.objectContaining({ title: "Retreat", startAt: expect.any(Date) })
    );
  });

  test("supports RSVP for authenticated users", async () => {
    eventService.getEventById.mockResolvedValue({ id: 5, title: "Workshop", registrations: [], audiences: [] });
    eventService.setRegistration.mockResolvedValue({ id: 1 });

    const response = await request(app)
      .post("/events/5/rsvp")
      .set("x-test-session", "teacher")
      .send({ status: "GOING" });

    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/events/5");
    expect(eventService.setRegistration).toHaveBeenCalledWith(5, 201, "GOING", undefined);
  });

  test("redirects unauthenticated visitors", async () => {
    const response = await request(app).get("/events");
    expect(response.status).toBe(302);
    expect(response.headers.location).toBe("/auth/login");
  });
});
