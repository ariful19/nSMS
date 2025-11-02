const ensureAnyRole = require("../../src/middleware/ensureAnyRole");

describe("ensureAnyRole middleware", () => {
  let req;
  let res;
  let next;

  beforeEach(() => {
    req = {
      session: {
        userId: 1,
        roleNames: [],
      },
      flash: jest.fn(),
      accepts: jest.fn().mockReturnValue("html"),
    };
    res = {
      status: jest.fn().mockReturnThis(),
      render: jest.fn(),
      redirect: jest.fn(),
      locals: {},
    };
    next = jest.fn();
  });

  test("calls next() when user has one of the required roles", () => {
    req.session.roleNames = ["teacher"];
    const middleware = ensureAnyRole(["Admin", "Teacher", "Staff"]);

    middleware(req, res, next);

    expect(next).toHaveBeenCalled();
    expect(res.status).not.toHaveBeenCalled();
  });

  test("calls next() when user has multiple matching roles", () => {
    req.session.roleNames = ["admin", "staff"];
    const middleware = ensureAnyRole(["Admin", "Staff"]);

    middleware(req, res, next);

    expect(next).toHaveBeenCalled();
    expect(res.status).not.toHaveBeenCalled();
  });

  test("returns 403 when user has none of the required roles", () => {
    req.session.roleNames = ["student"];
    const middleware = ensureAnyRole(["Admin", "Staff"]);

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

  test("redirects to login when session is missing", () => {
    req.session = null;
    const middleware = ensureAnyRole(["Admin"]);

    middleware(req, res, next);

    expect(res.redirect).toHaveBeenCalledWith("/auth/login");
    expect(next).not.toHaveBeenCalled();
  });

  test("calls next() when empty roles array is provided", () => {
    req.session.roleNames = ["teacher"];
    const middleware = ensureAnyRole([]);

    middleware(req, res, next);

    // Empty array means no role restriction
    expect(next).toHaveBeenCalled();
    expect(res.status).not.toHaveBeenCalled();
  });

  test("handles case-insensitive role matching", () => {
    req.session.roleNames = ["teacher"]; // lowercase in session
    const middleware = ensureAnyRole(["Teacher"]); // Title case in definition

    middleware(req, res, next);

    expect(next).toHaveBeenCalled();
  });

  test("returns 403 when roleNames is not an array", () => {
    req.session.roleNames = null;
    const middleware = ensureAnyRole(["Admin"]);

    middleware(req, res, next);

    expect(res.status).toHaveBeenCalledWith(403);
  });
});
