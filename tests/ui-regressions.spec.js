const fs = require("fs");
const path = require("path");
const vm = require("vm");
describe("QA regression behavior", () => {
  it.each([["students", "Student"], ["teachers", "Teacher"]])("names actions from real %s person records", (domain, kind) => {
    const source = fs.readFileSync(path.join(__dirname, `../src/client/${domain}/components/${kind}ListApp.vue`), "utf8");
    const body = source.match(/function displayName\(record\) \{[\s\S]*?\n\}/)[0];
    const context = {};
    vm.runInNewContext(body, context);
    expect(context.displayName({ person: { firstName: "Sara", lastName: "Nguyen" } })).toBe("Sara Nguyen");
    expect(context.displayName({ person: { firstName: "Daniel", lastName: "Okafor" } })).toBe("Daniel Okafor");
    expect(context.displayName({})).toBe("this person");
  });
  it.each(["toggle", "navigation"])("closes with Escape from %s and restores focus", (target) => {
    const nodes = {};
    for (const name of ["toggle", "navigation"]) nodes[name] = { handlers: {}, expanded: "false", addEventListener(event, fn) { this.handlers[event] = fn; }, getAttribute() { return this.expanded; }, setAttribute(key, value) { this.expanded = value; }, classList: { toggle: jest.fn(), remove: jest.fn() }, focus: jest.fn() };
    vm.runInNewContext(fs.readFileSync(path.join(__dirname, "../src/public/js/navigation.js"), "utf8"), { document: { querySelector: () => nodes.toggle, getElementById: () => nodes.navigation } });
    nodes.toggle.handlers.click();
    expect(nodes.toggle.expanded).toBe("true");
    nodes[target].handlers.keydown({ key: "Escape" });
    expect(nodes.toggle.expanded).toBe("false");
    expect(nodes.toggle.focus).toHaveBeenCalled();
    expect(nodes.navigation.classList.remove).toHaveBeenCalledWith("is-open");
  });
});
