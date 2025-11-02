const ensureAuth = require("../../src/middleware/ensureAuth");

describe("ensureAuth middleware", () => {
  let req;
  let res;
  let next;

  beforeEach(() => {
    req = {
      session: {},
      originalUrl: "/protected",
    };
    res = {
      redirect: jest.fn(),
    };
    next = jest.fn();
  });

  test("calls next() when user is authenticated", () => {
    req.session.userId = 1;

    ensureAuth(req, res, next);

    expect(next).toHaveBeenCalled();
    expect(res.redirect).not.toHaveBeenCalled();
  });

  test("redirects to login when user is not authenticated", () => {
    ensureAuth(req, res, next);

    expect(res.redirect).toHaveBeenCalledWith("/auth/login");
    expect(next).not.toHaveBeenCalled();
  });

  test("redirects when session is missing", () => {
    req.session = null;

    ensureAuth(req, res, next);

    expect(res.redirect).toHaveBeenCalledWith("/auth/login");
    expect(next).not.toHaveBeenCalled();
  });

  test("redirects when userId is null", () => {
    req.session.userId = null;

    ensureAuth(req, res, next);

    expect(res.redirect).toHaveBeenCalledWith("/auth/login");
    expect(next).not.toHaveBeenCalled();
  });

  test("redirects when userId is undefined", () => {
    req.session.userId = undefined;

    ensureAuth(req, res, next);

    expect(res.redirect).toHaveBeenCalledWith("/auth/login");
    expect(next).not.toHaveBeenCalled();
  });
});
