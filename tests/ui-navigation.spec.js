const fs = require("fs");
const path = require("path");
const ejs = require("ejs");
const nav = fs.readFileSync(path.join(__dirname, "../src/views/partials/nav.ejs"), "utf8");
function render(role) {
  return ejs.render(nav, { isAuthenticated: Boolean(role), hasRole: (value) => value === role, currentUser: { displayName: "Test" }, csrfToken: "test" });
}
describe("Role-aware navigation", () => {
  it.each(["Admin", "Staff"])("shows management links for %s", (role) => {
    expect(render(role)).toContain('href="/classes"');
    expect(render(role)).toContain('href="/reports/grades"');
  });
  it("gives teachers reports without linking the forbidden class index", () => {
    expect(render("Teacher")).toContain('href="/reports/grades"');
    expect(render("Teacher")).not.toContain('href="/classes"');
  });
  it.each(["Student", null])("omits academic management for %s", (role) => {
    expect(render(role)).not.toContain('href="/classes"');
    expect(render(role)).not.toContain('href="/reports/grades"');
  });
  it("connects the menu toggle to its controlled navigation", () => {
    const html = render("Admin");
    expect(html).toContain('aria-expanded="false"');
    expect(html).toContain('aria-controls="main-navigation"');
    expect(html).toContain('id="main-navigation"');
  });
});
