const ensureAnyRole = require("../../src/middleware/ensureAnyRole");

describe("ensureAnyRole middleware", () => {
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

  test("calls next() when user has one of the required roles", () => {
    res.locals.hasRole = jest.fn((role) => role === "Teacher");
    const middleware = ensureAnyRole(["Admin", "Teacher", "Staff"]);

    middleware(req, res, next);

    expect(next).toHaveBeenCalled();
    expect(res.status).not.toHaveBeenCalled();
  });

  test("calls next() when user has multiple matching roles", () => {
    res.locals.hasRole = jest.fn((role) => role === "Admin" || role === "Staff");
    const middleware = ensureAnyRole(["Admin", "Staff"]);

    middleware(req, res, next);

    expect(next).toHaveBeenCalled();
    expect(res.status).not.toHaveBeenCalled();
  });

  test("returns 403 when user has none of the required roles", () => {
    res.locals.hasRole = jest.fn().mockReturnValue(false);
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

  test("returns 403 when hasRole function is missing", () => {
    const middleware = ensureAnyRole(["Admin"]);

    middleware(req, res, next);

    expect(res.status).toHaveBeenCalledWith(403);
    expect(next).not.toHaveBeenCalled();
  });

  test("handles empty roles array", () => {
    res.locals.hasRole = jest.fn().mockReturnValue(true);
    const middleware = ensureAnyRole([]);

    middleware(req, res, next);

    expect(res.status).toHaveBeenCalledWith(403);
    expect(next).not.toHaveBeenCalled();
  });

  test("checks all roles until one matches", () => {
    const hasRoleMock = jest.fn()
      .mockReturnValueOnce(false) // Admin: no
      .mockReturnValueOnce(false) // Staff: no
      .mockReturnValueOnce(true);  // Teacher: yes
    
    res.locals.hasRole = hasRoleMock;
    const middleware = ensureAnyRole(["Admin", "Staff", "Teacher"]);

    middleware(req, res, next);

    expect(hasRoleMock).toHaveBeenCalledWith("Admin");
    expect(hasRoleMock).toHaveBeenCalledWith("Staff");
    expect(hasRoleMock).toHaveBeenCalledWith("Teacher");
    expect(next).toHaveBeenCalled();
  });

  test("short-circuits when first role matches", () => {
    const hasRoleMock = jest.fn()
      .mockReturnValueOnce(true); // Admin: yes (stops checking)
    
    res.locals.hasRole = hasRoleMock;
    const middleware = ensureAnyRole(["Admin", "Staff", "Teacher"]);

    middleware(req, res, next);

    expect(hasRoleMock).toHaveBeenCalledTimes(1);
    expect(hasRoleMock).toHaveBeenCalledWith("Admin");
    expect(next).toHaveBeenCalled();
  });
});
