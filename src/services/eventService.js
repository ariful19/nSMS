const { EventStatus, EventVisibility, RsvpStatus } = require("@prisma/client");
const { prisma } = require("../db/client");
const { ensureUniqueSlug } = require("../utils/slug");

const defaultEventInclude = {
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

const detailEventInclude = {
  ...defaultEventInclude,
  registrations: {
    include: {
      user: {
        select: {
          id: true,
          username: true,
          email: true,
        },
      },
    },
    orderBy: [{ respondedAt: "desc" }],
  },
};

const eventOrderBy = [
  { startAt: "asc" },
  { title: "asc" },
];

function normalizeStatus(status) {
  if (!status) {
    return undefined;
  }
  const upper = status.toString().toUpperCase();
  return EventStatus[upper] ? upper : undefined;
}

function normalizeVisibility(visibility) {
  if (!visibility) {
    return undefined;
  }
  const upper = visibility.toString().toUpperCase();
  return EventVisibility[upper] ? upper : undefined;
}

function normalizeRsvp(status) {
  if (!status) {
    return undefined;
  }
  const upper = status.toString().toUpperCase();
  return RsvpStatus[upper] ? upper : undefined;
}

function buildEventWhere({
  search,
  status,
  visibility,
  includePast,
  audienceRoleIds,
  startAfter,
  startBefore,
} = {}) {
  const conditions = [];
  const normalizedStatus = normalizeStatus(status);
  const normalizedVisibility = normalizeVisibility(visibility);

  if (normalizedStatus) {
    conditions.push({ status: normalizedStatus });
  }

  if (normalizedVisibility) {
    conditions.push({ visibility: normalizedVisibility });
  }

  if (search) {
    conditions.push({
      OR: [
        { title: { contains: search, mode: "insensitive" } },
        { summary: { contains: search, mode: "insensitive" } },
        { description: { contains: search, mode: "insensitive" } },
        { location: { contains: search, mode: "insensitive" } },
      ],
    });
  }

  if (!includePast) {
    const now = new Date();
    conditions.push({
      OR: [
        { endAt: null, startAt: { gte: now } },
        { endAt: { gte: now } },
      ],
    });
  }

  if (startAfter instanceof Date && !Number.isNaN(startAfter.getTime())) {
    conditions.push({ startAt: { gte: startAfter } });
  }

  if (startBefore instanceof Date && !Number.isNaN(startBefore.getTime())) {
    conditions.push({ startAt: { lte: startBefore } });
  }

  if (Array.isArray(audienceRoleIds) && audienceRoleIds.length > 0) {
    conditions.push({ audiences: { some: { roleId: { in: audienceRoleIds } } } });
  }

  if (conditions.length === 0) {
    return undefined;
  }

  return { AND: conditions };
}

async function listEvents({
  search,
  status,
  visibility,
  includePast = false,
  audienceRoleIds,
  startAfter,
  startBefore,
  limit,
} = {}) {
  const where = buildEventWhere({
    search,
    status,
    visibility,
    includePast,
    audienceRoleIds,
    startAfter,
    startBefore,
  });

  return prisma.event.findMany({
    where,
    include: defaultEventInclude,
    orderBy: eventOrderBy,
    take: typeof limit === "number" ? limit : undefined,
  });
}

async function getEventById(id) {
  if (!id) {
    return null;
  }

  return prisma.event.findUnique({
    where: { id },
    include: detailEventInclude,
  });
}

async function createEvent(payload) {
  return prisma.$transaction(async (tx) => {
    const slug = await ensureUniqueSlug(tx.event, payload.slug || payload.title, { prefix: "event" });

    const event = await tx.event.create({
      data: {
        slug,
        title: payload.title,
        summary: payload.summary || null,
        description: payload.description || null,
        location: payload.location || null,
        status: normalizeStatus(payload.status) || EventStatus.DRAFT,
        visibility: normalizeVisibility(payload.visibility) || EventVisibility.INTERNAL,
        startAt: payload.startAt,
        endAt: payload.endAt || null,
        publishAt: payload.publishAt || null,
        registrationDeadline: payload.registrationDeadline || null,
        isAllDay: Boolean(payload.isAllDay),
        createdById: payload.createdById || null,
        updatedById: payload.updatedById || payload.createdById || null,
        audiences: Array.isArray(payload.audienceRoleIds) && payload.audienceRoleIds.length > 0
          ? {
              create: payload.audienceRoleIds.map((roleId) => ({ roleId })),
            }
          : undefined,
      },
      include: detailEventInclude,
    });

    return event;
  });
}

async function updateEvent(id, payload) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.event.findUnique({ where: { id } });
    if (!existing) {
      return null;
    }

    const slugBase = payload.slug || payload.title || existing.title;
    const slug = await ensureUniqueSlug(tx.event, slugBase, { excludeId: id, prefix: "event" });

    const event = await tx.event.update({
      where: { id },
      data: {
        slug,
        title: payload.title,
        summary: payload.summary || null,
        description: payload.description || null,
        location: payload.location || null,
        status: normalizeStatus(payload.status) || EventStatus.DRAFT,
        visibility: normalizeVisibility(payload.visibility) || EventVisibility.INTERNAL,
        startAt: payload.startAt,
        endAt: payload.endAt || null,
        publishAt: payload.publishAt || null,
        registrationDeadline: payload.registrationDeadline || null,
        isAllDay: Boolean(payload.isAllDay),
        updatedById: payload.updatedById || payload.createdById || null,
      },
      include: detailEventInclude,
    });

    if (Array.isArray(payload.audienceRoleIds)) {
      await tx.eventAudienceRole.deleteMany({ where: { eventId: id } });
      if (payload.audienceRoleIds.length > 0) {
        await tx.eventAudienceRole.createMany({
          data: payload.audienceRoleIds.map((roleId) => ({ eventId: id, roleId })),
          skipDuplicates: true,
        });
      }
    }

    return event;
  });
}

async function deleteEvent(id) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.event.findUnique({ where: { id } });
    if (!existing) {
      return false;
    }

    await tx.eventAudienceRole.deleteMany({ where: { eventId: id } });
    await tx.eventRegistration.deleteMany({ where: { eventId: id } });
    await tx.event.delete({ where: { id } });
    return true;
  });
}

async function setRegistration(eventId, userId, status, notes) {
  const normalizedStatus = normalizeRsvp(status) || RsvpStatus.GOING;
  const respondedAt = new Date();

  const registration = await prisma.eventRegistration.upsert({
    where: {
      eventId_userId: {
        eventId,
        userId,
      },
    },
    update: {
      status: normalizedStatus,
      respondedAt,
      notes: notes || null,
    },
    create: {
      eventId,
      userId,
      status: normalizedStatus,
      respondedAt,
      notes: notes || null,
    },
    include: {
      user: {
        select: {
          id: true,
          username: true,
          email: true,
        },
      },
    },
  });

  return registration;
}

async function deleteRegistration(eventId, userId) {
  await prisma.eventRegistration.deleteMany({ where: { eventId, userId } });
}

async function getEventLookups() {
  const roles = await prisma.role.findMany({ orderBy: [{ name: "asc" }] });
  const statuses = Object.keys(EventStatus);
  const visibilities = Object.keys(EventVisibility);
  const rsvpStatuses = Object.keys(RsvpStatus);
  return { roles, statuses, visibilities, rsvpStatuses };
}

async function getUpcomingEvents(limit = 5) {
  const now = new Date();
  return prisma.event.findMany({
    where: {
      status: { in: [EventStatus.PUBLISHED, EventStatus.SCHEDULED] },
      OR: [
        { endAt: null, startAt: { gte: now } },
        { endAt: { gte: now } },
      ],
    },
    include: defaultEventInclude,
    orderBy: eventOrderBy,
    take: limit,
  });
}

module.exports = {
  listEvents,
  getEventById,
  createEvent,
  updateEvent,
  deleteEvent,
  setRegistration,
  deleteRegistration,
  getEventLookups,
  getUpcomingEvents,
  normalizeStatus,
  normalizeVisibility,
  normalizeRsvp,
};
