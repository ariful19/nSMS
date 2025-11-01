const eventService = require("../services/eventService");
const { serializeState, createScriptTag } = require("../utils/viewHelpers");

function normalizeString(value) {
  if (typeof value !== "string") {
    return "";
  }
  return value.trim();
}

function parseOptionalDateTime(value, label, errors) {
  if (!value) {
    return null;
  }
  const parsed = new Date(value);
  if (Number.isNaN(parsed.getTime())) {
    if (errors) {
      errors.push(`${label} must be a valid date and time.`);
    }
    return null;
  }
  return parsed;
}

function parseRequiredDateTime(value, label, errors) {
  const parsed = parseOptionalDateTime(value, label, errors);
  if (!parsed) {
    errors.push(`${label} is required.`);
  }
  return parsed;
}

function parseAudienceRoleIds(input) {
  if (!input) {
    return [];
  }
  const values = Array.isArray(input) ? input : [input];
  const ids = values
    .map((value) => {
      const parsed = Number.parseInt(value, 10);
      return Number.isNaN(parsed) ? null : parsed;
    })
    .filter((value, index, array) => value !== null && array.indexOf(value) === index);
  return ids;
}

function canManage(res) {
  if (typeof res.locals.hasRole !== "function") {
    return false;
  }
  return res.locals.hasRole("Admin") || res.locals.hasRole("Staff");
}

function buildEventPayload(body, errors) {
  const title = normalizeString(body.title);
  if (!title) {
    errors.push("Title is required.");
  }

  const status = normalizeString(body.status).toUpperCase();
  const visibility = normalizeString(body.visibility).toUpperCase();
  const summary = normalizeString(body.summary);
  const description = normalizeString(body.description);
  const location = normalizeString(body.location);
  const isAllDay = body.isAllDay === "true" || body.isAllDay === "on";

  const startAt = parseRequiredDateTime(body.startAt, "Start date", errors);
  const endAt = parseOptionalDateTime(body.endAt, "End date", errors);
  const publishAt = parseOptionalDateTime(body.publishAt, "Publish date", errors);
  const registrationDeadline = parseOptionalDateTime(
    body.registrationDeadline,
    "Registration deadline",
    errors
  );

  if (startAt && endAt && endAt < startAt) {
    errors.push("End date must be after the start date.");
  }

  if (registrationDeadline && startAt && registrationDeadline > startAt) {
    errors.push("Registration deadline must be on or before the event start.");
  }

  const audienceRoleIds = parseAudienceRoleIds(body.audienceRoleIds);

  const payload = {
    title,
    status,
    visibility,
    summary: summary || null,
    description: description || null,
    location: location || null,
    isAllDay,
    startAt,
    endAt,
    publishAt,
    registrationDeadline,
    audienceRoleIds,
  };

  const formValues = {
    title: body.title || "",
    status,
    visibility,
    summary: body.summary || "",
    description: body.description || "",
    location: body.location || "",
    isAllDay,
    startAt: body.startAt || "",
    endAt: body.endAt || "",
    publishAt: body.publishAt || "",
    registrationDeadline: body.registrationDeadline || "",
    audienceRoleIds: audienceRoleIds.map(String),
  };

  return { payload, formValues };
}

function formatDateTimeForInput(value) {
  if (!value) {
    return "";
  }
  const date = value instanceof Date ? value : new Date(value);
  if (Number.isNaN(date.getTime())) {
    return "";
  }
  return date.toISOString().slice(0, 16);
}

exports.index = async (req, res, next) => {
  const search = normalizeString(req.query.search || "");
  const status = normalizeString(req.query.status || "");
  const visibility = normalizeString(req.query.visibility || "");
  const includePast = canManage(res) && req.query.includePast === "true";
  const audienceRoleIds = parseAudienceRoleIds(req.query.audienceRoleIds);
  const startAfter = parseOptionalDateTime(req.query.startAfter, "Start after");
  const startBefore = parseOptionalDateTime(req.query.startBefore, "Start before");

  try {
    const [events, lookups] = await Promise.all([
      eventService.listEvents({
        search: search || undefined,
        status: status || undefined,
        visibility: visibility || undefined,
        includePast,
        audienceRoleIds,
        startAfter,
        startBefore,
      }),
      eventService.getEventLookups(),
    ]);

    const filters = {
      search,
      status,
      visibility,
      includePast,
      audienceRoleIds,
      startAfter: req.query.startAfter || "",
      startBefore: req.query.startBefore || "",
    };

    const indexState = serializeState({
      events,
      lookups,
      filters,
      canManage: canManage(res),
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/events/index", {
      title: "Events",
      events,
      lookups,
      filters,
      canManage: canManage(res),
      scripts: createScriptTag("communications.js"),
      indexState,
    });
  } catch (error) {
    next(error);
  }
};

exports.show = async (req, res, next) => {
  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Event not found.");
    }
    return res.redirect("/events");
  }

  try {
    const event = await eventService.getEventById(id);
    if (!event) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Event not found.");
      }
      return res.redirect("/events");
    }

    const lookups = await eventService.getEventLookups();

    const currentUserId = req.session?.userId || null;
    const registration = currentUserId
      ? event.registrations.find((entry) => entry.userId === currentUserId)
      : null;

    const detailState = serializeState({
      event,
      lookups,
      registration,
      canManage: canManage(res),
      csrfToken: res.locals.csrfToken || null,
    });

    return res.render("pages/events/detail", {
      title: event.title,
      event,
      lookups,
      registration,
      canManage: canManage(res),
      scripts: createScriptTag("communications.js"),
      detailState,
    });
  } catch (error) {
    return next(error);
  }
};

exports.createForm = async (req, res, next) => {
  if (!canManage(res)) {
    return res.status(403).render("pages/error", {
      title: "Forbidden",
      message: "You do not have permission to manage events.",
    });
  }

  try {
    const lookups = await eventService.getEventLookups();
    const formState = serializeState({
      lookups,
      values: {},
      errors: [],
      isEditMode: false,
      eventId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/events/create",
    });

    return res.render("pages/events/form", {
      title: "Create event",
      lookups,
      formValues: {},
      errors: [],
      isEditMode: false,
      eventId: null,
      scripts: createScriptTag("communications.js"),
      formState,
    });
  } catch (error) {
    return next(error);
  }
};

exports.create = async (req, res, next) => {
  if (!canManage(res)) {
    return res.status(403).render("pages/error", {
      title: "Forbidden",
      message: "You do not have permission to manage events.",
    });
  }

  let lookups;
  try {
    lookups = await eventService.getEventLookups();
  } catch (error) {
    return next(error);
  }

  const errors = [];
  const { payload, formValues } = buildEventPayload(req.body || {}, errors);
  payload.createdById = req.session?.userId || null;
  payload.updatedById = req.session?.userId || null;

  if (errors.length > 0) {
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: false,
      eventId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/events/create",
    });

    return res.status(400).render("pages/events/form", {
      title: "Create event",
      lookups,
      formValues,
      errors,
      isEditMode: false,
      eventId: null,
      scripts: createScriptTag("communications.js"),
      formState,
    });
  }

  try {
    const event = await eventService.createEvent(payload);
    if (typeof req.flash === "function") {
      req.flash("success", "Event created successfully.");
    }
    return res.redirect(`/events/${event.id}`);
  } catch (error) {
    return next(error);
  }
};

exports.editForm = async (req, res, next) => {
  if (!canManage(res)) {
    return res.status(403).render("pages/error", {
      title: "Forbidden",
      message: "You do not have permission to manage events.",
    });
  }

  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Event not found.");
    }
    return res.redirect("/events");
  }

  try {
    const [event, lookups] = await Promise.all([
      eventService.getEventById(id),
      eventService.getEventLookups(),
    ]);

    if (!event) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Event not found.");
      }
      return res.redirect("/events");
    }

    const formValues = {
      title: event.title,
      status: event.status,
      visibility: event.visibility,
      summary: event.summary || "",
      description: event.description || "",
      location: event.location || "",
      isAllDay: event.isAllDay,
      startAt: formatDateTimeForInput(event.startAt),
      endAt: formatDateTimeForInput(event.endAt),
      publishAt: formatDateTimeForInput(event.publishAt),
      registrationDeadline: formatDateTimeForInput(event.registrationDeadline),
      audienceRoleIds: event.audiences.map((audience) => String(audience.roleId)),
    };

    const formState = serializeState({
      lookups,
      values: formValues,
      errors: [],
      isEditMode: true,
      eventId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/events/${id}/edit`,
    });

    return res.render("pages/events/form", {
      title: `Edit event • ${event.title}`,
      lookups,
      formValues,
      errors: [],
      isEditMode: true,
      eventId: id,
      scripts: createScriptTag("communications.js"),
      formState,
    });
  } catch (error) {
    return next(error);
  }
};

exports.update = async (req, res, next) => {
  if (!canManage(res)) {
    return res.status(403).render("pages/error", {
      title: "Forbidden",
      message: "You do not have permission to manage events.",
    });
  }

  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Event not found.");
    }
    return res.redirect("/events");
  }

  let lookups;
  try {
    lookups = await eventService.getEventLookups();
  } catch (error) {
    return next(error);
  }

  const errors = [];
  const { payload, formValues } = buildEventPayload(req.body || {}, errors);
  payload.updatedById = req.session?.userId || null;

  if (errors.length > 0) {
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: true,
      eventId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/events/${id}/edit`,
    });

    return res.status(400).render("pages/events/form", {
      title: "Edit event",
      lookups,
      formValues,
      errors,
      isEditMode: true,
      eventId: id,
      scripts: createScriptTag("communications.js"),
      formState,
    });
  }

  try {
    const updated = await eventService.updateEvent(id, payload);
    if (!updated) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Event not found.");
      }
      return res.redirect("/events");
    }

    if (typeof req.flash === "function") {
      req.flash("success", "Event updated successfully.");
    }
    return res.redirect(`/events/${updated.id}`);
  } catch (error) {
    return next(error);
  }
};

exports.destroy = async (req, res, next) => {
  if (!canManage(res)) {
    return res.status(403).render("pages/error", {
      title: "Forbidden",
      message: "You do not have permission to manage events.",
    });
  }

  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Event not found.");
    }
    return res.redirect("/events");
  }

  try {
    const deleted = await eventService.deleteEvent(id);
    if (deleted && typeof req.flash === "function") {
      req.flash("success", "Event deleted successfully.");
    }
    return res.redirect("/events");
  } catch (error) {
    return next(error);
  }
};

exports.rsvp = async (req, res, next) => {
  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Event not found.");
    }
    return res.redirect("/events");
  }

  const userId = req.session?.userId;
  if (!userId) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Please sign in to RSVP.");
    }
    return res.redirect("/auth/login");
  }

  try {
    const event = await eventService.getEventById(id);
    if (!event) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Event not found.");
      }
      return res.redirect("/events");
    }

    await eventService.setRegistration(id, userId, req.body?.status, req.body?.notes);
    if (typeof req.flash === "function") {
      req.flash("success", "Your response has been saved.");
    }
    return res.redirect(`/events/${id}`);
  } catch (error) {
    return next(error);
  }
};

exports.cancelRsvp = async (req, res, next) => {
  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Event not found.");
    }
    return res.redirect("/events");
  }

  const userId = req.session?.userId;
  if (!userId) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Please sign in to manage your RSVP.");
    }
    return res.redirect("/auth/login");
  }

  try {
    await eventService.deleteRegistration(id, userId);
    if (typeof req.flash === "function") {
      req.flash("success", "Your RSVP has been removed.");
    }
    return res.redirect(`/events/${id}`);
  } catch (error) {
    return next(error);
  }
};
