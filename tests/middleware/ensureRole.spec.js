const ensureRole = require("../../src/middleware/ensureRole");

describe("ensureRole middleware", () => {
  let req;
  let res;
  let next;

  beforeEach(() => {
    req = {
      session: {
        userId: 1,
      },
    };
    res = {
      status: jest.fn().mockReturnThis(),
      render: jest.fn(),
      locals: {},
    };
    next = jest.fn();
  });

  test("calls next() when user has all required roles", () => {
    req.session.roleNames = ["admin", "staff"];
    const middleware = ensureRole(["admin"]);

    middleware(req, res, next);

    expect(next).toHaveBeenCalled();
    expect(res.status).not.toHaveBeenCalled();
  });

  test("calls next() when user has multiple required roles", () => {
    req.session.roleNames = ["admin", "staff"];
    const middleware = ensureRole(["admin", "staff"]);

    middleware(req, res, next);

    expect(next).toHaveBeenCalled();
    expect(res.status).not.toHaveBeenCalled();
  });

  test("returns 403 when user does not have required role", () => {
    req.session.roleNames = ["teacher"];
    const middleware = ensureRole(["admin"]);

    middleware(req, res, next);

    expect(res.status).toHaveBeenCalledWith(403);
    expect(res.render).toHaveBeenCalledWith(
      "pages/error",
      expect.objectContaining({
        message: expect.stringContaining("do not have permission"),
      })
    );
    expect(next).not.toHaveBeenCalled();
  });

  test("returns 403 when user lacks one of multiple required roles", () => {
    req.session.roleNames = ["admin"];
    const middleware = ensureRole(["admin", "staff"]);

    middleware(req, res, next);

    expect(res.status).toHaveBeenCalledWith(403);
    expect(next).not.toHaveBeenCalled();
  });

  test("redirects to login when session is missing", () => {
    req.session = null;
    res.redirect = jest.fn();
    const middleware = ensureRole(["admin"]);

    middleware(req, res, next);

    expect(res.redirect).toHaveBeenCalledWith("/auth/login");
    expect(next).not.toHaveBeenCalled();
  });

  test("handles empty role names array", () => {
    req.session.roleNames = [];
    const middleware = ensureRole(["admin"]);

    middleware(req, res, next);

    expect(res.status).toHaveBeenCalledWith(403);
    expect(next).not.toHaveBeenCalled();
  });
});
