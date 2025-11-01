const MAX_SLUG_LENGTH = 60;

function slugify(value, maxLength = MAX_SLUG_LENGTH) {
  if (!value) {
    return "";
  }

  const normalized = value
    .toString()
    .trim()
    .toLowerCase()
    .replace(/[^a-z0-9]+/g, "-")
    .replace(/^-+|-+$/g, "");

  if (normalized.length <= maxLength) {
    return normalized;
  }

  return normalized.slice(0, maxLength).replace(/-+$/g, "");
}

async function ensureUniqueSlug(model, value, { excludeId, prefix = "item" } = {}) {
  const base = slugify(value) || slugify(prefix) || `${prefix}-${Date.now()}`;
  let candidate = base.length > 0 ? base : `${prefix}-${Date.now()}`;
  let attempt = 1;

  // eslint-disable-next-line no-constant-condition
  while (true) {
    const existing = await model.findUnique({
      where: { slug: candidate },
      select: { id: true },
    });

    if (!existing || (excludeId && existing.id === excludeId)) {
      return candidate;
    }

    attempt += 1;
    const suffix = `-${attempt}`;
    const baseLength = Math.max(1, MAX_SLUG_LENGTH - suffix.length);
    candidate = `${base.slice(0, baseLength)}${suffix}`;
  }
}

module.exports = {
  slugify,
  ensureUniqueSlug,
};
