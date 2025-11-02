const { slugify, ensureUniqueSlug } = require("../../src/utils/slug");

describe("slugify", () => {
  test("converts basic string to slug", () => {
    expect(slugify("Hello World")).toBe("hello-world");
  });

  test("removes special characters", () => {
    expect(slugify("Hello! @World# $123%")).toBe("hello-world-123");
  });

  test("handles multiple spaces", () => {
    expect(slugify("Hello    World")).toBe("hello-world");
  });

  test("removes leading and trailing dashes", () => {
    expect(slugify("---hello-world---")).toBe("hello-world");
  });

  test("handles empty string", () => {
    expect(slugify("")).toBe("");
  });

  test("handles null value", () => {
    expect(slugify(null)).toBe("");
  });

  test("handles undefined value", () => {
    expect(slugify(undefined)).toBe("");
  });

  test("converts to lowercase", () => {
    expect(slugify("HELLO WORLD")).toBe("hello-world");
  });

  test("handles numbers", () => {
    expect(slugify("Test 123 456")).toBe("test-123-456");
  });

  test("handles accented characters", () => {
    expect(slugify("Café Résumé")).toBe("caf-r-sum");
  });

  test("respects maxLength parameter", () => {
    const longString = "This is a very long string that should be truncated to a specific length";
    const result = slugify(longString, 20);
    expect(result.length).toBeLessThanOrEqual(20);
    expect(result).not.toMatch(/-$/); // Should not end with dash
  });

  test("truncates at default max length", () => {
    const longString = "a".repeat(100);
    const result = slugify(longString);
    expect(result.length).toBeLessThanOrEqual(60);
  });

  test("handles consecutive special characters", () => {
    expect(slugify("Hello!!!World???")).toBe("hello-world");
  });

  test("preserves existing dashes between words", () => {
    expect(slugify("hello-world-foo-bar")).toBe("hello-world-foo-bar");
  });
});

describe("ensureUniqueSlug", () => {
  let mockModel;

  beforeEach(() => {
    mockModel = {
      findUnique: jest.fn(),
    };
  });

  test("returns slug when it does not exist", async () => {
    mockModel.findUnique.mockResolvedValue(null);

    const result = await ensureUniqueSlug(mockModel, "Hello World");

    expect(result).toBe("hello-world");
    expect(mockModel.findUnique).toHaveBeenCalledWith({
      where: { slug: "hello-world" },
      select: { id: true },
    });
  });

  test("adds suffix when slug exists", async () => {
    mockModel.findUnique
      .mockResolvedValueOnce({ id: 1 }) // First slug exists
      .mockResolvedValueOnce(null);      // Second slug is available

    const result = await ensureUniqueSlug(mockModel, "Hello World");

    expect(result).toBe("hello-world-2");
    expect(mockModel.findUnique).toHaveBeenCalledTimes(2);
  });

  test("increments suffix until unique slug found", async () => {
    mockModel.findUnique
      .mockResolvedValueOnce({ id: 1 }) // -1 exists
      .mockResolvedValueOnce({ id: 2 }) // -2 exists
      .mockResolvedValueOnce({ id: 3 }) // -3 exists
      .mockResolvedValueOnce(null);      // -4 is available

    const result = await ensureUniqueSlug(mockModel, "Hello World");

    expect(result).toBe("hello-world-4");
    expect(mockModel.findUnique).toHaveBeenCalledTimes(4);
  });

  test("excludes specified ID when checking uniqueness", async () => {
    mockModel.findUnique.mockResolvedValue({ id: 5 });

    const result = await ensureUniqueSlug(mockModel, "Hello World", { excludeId: 5 });

    expect(result).toBe("hello-world");
    expect(mockModel.findUnique).toHaveBeenCalledTimes(1);
  });

  test("uses prefix when value is empty", async () => {
    mockModel.findUnique.mockResolvedValue(null);

    const result = await ensureUniqueSlug(mockModel, "", { prefix: "notice" });

    // When value is empty, it uses the prefix directly if available
    expect(result).toMatch(/^notice/);
  });

  test("uses custom prefix", async () => {
    mockModel.findUnique.mockResolvedValue(null);

    const result = await ensureUniqueSlug(mockModel, "Test", { prefix: "event" });

    expect(result).toBe("test");
  });

  test("handles long slugs with suffixes", async () => {
    const longString = "a".repeat(60);
    mockModel.findUnique
      .mockResolvedValueOnce({ id: 1 })
      .mockResolvedValueOnce(null);

    const result = await ensureUniqueSlug(mockModel, longString);

    expect(result.length).toBeLessThanOrEqual(60);
    expect(result).toMatch(/-2$/);
  });
});
