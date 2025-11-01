const { NoticeStatus } = require("@prisma/client");
const { prisma } = require("../db/client");
const { ensureUniqueSlug } = require("../utils/slug");

const defaultNoticeInclude = {
  audiences: {
    include: {
      role: true,
    },
  },
  createdBy: {
    select: {
      id: true,
      username: true,
      email: true,
    },
  },
  updatedBy: {
    select: {
      id: true,
      username: true,
      email: true,
    },
  },
};

const noticeOrderBy = [
  { isPinned: "desc" },
  { publishAt: "desc" },
  { createdAt: "desc" },
];

function normalizeStatus(status) {
  if (!status) {
    return undefined;
  }
  const upper = status.toString().toUpperCase();
  return NoticeStatus[upper] ? upper : undefined;
}

function buildNoticeWhere({ search, status, includeExpired, audienceRoleIds } = {}) {
  const conditions = [];
  const normalizedStatus = normalizeStatus(status);
  if (normalizedStatus) {
    conditions.push({ status: normalizedStatus });
  }

  if (search) {
    conditions.push({
      OR: [
        { title: { contains: search, mode: "insensitive" } },
        { summary: { contains: search, mode: "insensitive" } },
        { content: { contains: search, mode: "insensitive" } },
      ],
    });
  }

  if (!includeExpired) {
    const now = new Date();
    conditions.push({ OR: [{ expiresAt: null }, { expiresAt: { gte: now } }] });
  }

  if (Array.isArray(audienceRoleIds) && audienceRoleIds.length > 0) {
    conditions.push({ audiences: { some: { roleId: { in: audienceRoleIds } } } });
  }

  if (conditions.length === 0) {
    return undefined;
  }

  return { AND: conditions };
}

async function listNotices({ search, status, includeExpired = false, audienceRoleIds, limit } = {}) {
  const where = buildNoticeWhere({ search, status, includeExpired, audienceRoleIds });

  return prisma.notice.findMany({
    where,
    include: defaultNoticeInclude,
    orderBy: noticeOrderBy,
    take: typeof limit === "number" ? limit : undefined,
  });
}

async function getNoticeById(id) {
  if (!id) {
    return null;
  }

  return prisma.notice.findUnique({
    where: { id },
    include: {
      ...defaultNoticeInclude,
      audiences: {
        include: { role: true },
        orderBy: [{ role: { name: "asc" } }],
      },
    },
  });
}

async function createNotice(payload) {
  return prisma.$transaction(async (tx) => {
    const slug = await ensureUniqueSlug(tx.notice, payload.slug || payload.title, { prefix: "notice" });

    const notice = await tx.notice.create({
      data: {
        slug,
        title: payload.title,
        summary: payload.summary || null,
        content: payload.content,
        status: normalizeStatus(payload.status) || NoticeStatus.DRAFT,
        publishAt: payload.publishAt || null,
        expiresAt: payload.expiresAt || null,
        isPinned: Boolean(payload.isPinned),
        createdById: payload.createdById || null,
        updatedById: payload.updatedById || payload.createdById || null,
        audiences: Array.isArray(payload.audienceRoleIds) && payload.audienceRoleIds.length > 0
          ? {
              create: payload.audienceRoleIds.map((roleId) => ({ roleId })),
            }
          : undefined,
      },
      include: defaultNoticeInclude,
    });

    return notice;
  });
}

async function updateNotice(id, payload) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.notice.findUnique({ where: { id } });
    if (!existing) {
      return null;
    }

    const slugBase = payload.slug || payload.title || existing.title;
    const slug = await ensureUniqueSlug(tx.notice, slugBase, { excludeId: id, prefix: "notice" });

    const notice = await tx.notice.update({
      where: { id },
      data: {
        slug,
        title: payload.title,
        summary: payload.summary || null,
        content: payload.content,
        status: normalizeStatus(payload.status) || NoticeStatus.DRAFT,
        publishAt: payload.publishAt || null,
        expiresAt: payload.expiresAt || null,
        isPinned: Boolean(payload.isPinned),
        updatedById: payload.updatedById || payload.createdById || null,
      },
      include: defaultNoticeInclude,
    });

    if (Array.isArray(payload.audienceRoleIds)) {
      await tx.noticeAudienceRole.deleteMany({ where: { noticeId: id } });
      if (payload.audienceRoleIds.length > 0) {
        await tx.noticeAudienceRole.createMany({
          data: payload.audienceRoleIds.map((roleId) => ({ noticeId: id, roleId })),
          skipDuplicates: true,
        });
      }
    }

    return notice;
  });
}

async function deleteNotice(id) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.notice.findUnique({ where: { id } });
    if (!existing) {
      return false;
    }

    await tx.noticeAudienceRole.deleteMany({ where: { noticeId: id } });
    await tx.notice.delete({ where: { id } });
    return true;
  });
}

async function getNoticeLookups() {
  const roles = await prisma.role.findMany({ orderBy: [{ name: "asc" }] });
  const statuses = Object.keys(NoticeStatus);
  return { roles, statuses };
}

async function getRecentPublishedNotices(limit = 5) {
  const now = new Date();
  return prisma.notice.findMany({
    where: {
      status: NoticeStatus.PUBLISHED,
      AND: [
        { OR: [{ publishAt: null }, { publishAt: { lte: now } }] },
        { OR: [{ expiresAt: null }, { expiresAt: { gte: now } }] },
      ],
    },
    include: defaultNoticeInclude,
    orderBy: noticeOrderBy,
    take: limit,
  });
}

module.exports = {
  listNotices,
  getNoticeById,
  createNotice,
  updateNotice,
  deleteNotice,
  getNoticeLookups,
  getRecentPublishedNotices,
  normalizeStatus,
};
