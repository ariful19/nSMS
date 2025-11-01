const noticeService = require("../services/noticeService");
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

function buildNoticePayload(body) {
  const errors = [];

  const title = normalizeString(body.title);
  if (!title) {
    errors.push("Title is required.");
  }

  const content = normalizeString(body.content);
  if (!content) {
    errors.push("Content is required.");
  }

  const summary = normalizeString(body.summary);
  const status = normalizeString(body.status).toUpperCase();
  const publishAt = parseOptionalDateTime(body.publishAt, "Publish date", errors);
  const expiresAt = parseOptionalDateTime(body.expiresAt, "Expiry date", errors);
  const audienceRoleIds = parseAudienceRoleIds(body.audienceRoleIds);
  const isPinned = body.isPinned === "true" || body.isPinned === "on";

  const payload = {
    title,
    summary: summary || null,
    content,
    status,
    publishAt,
    expiresAt,
    isPinned,
    audienceRoleIds,
  };

  const formValues = {
    title: body.title || "",
    summary: body.summary || "",
    content: body.content || "",
    status: status || "",
    publishAt: body.publishAt || "",
    expiresAt: body.expiresAt || "",
    isPinned,
    audienceRoleIds: audienceRoleIds.map(String),
  };

  return { errors, payload, formValues };
}

function canManage(res) {
  if (typeof res.locals.hasRole !== "function") {
    return false;
  }
  return res.locals.hasRole("Admin") || res.locals.hasRole("Staff");
}

exports.index = async (req, res, next) => {
  const search = normalizeString(req.query.search || "");
  const status = normalizeString(req.query.status || "");
  const includeExpired = canManage(res) && req.query.includeExpired === "true";
  const audienceRoleIds = parseAudienceRoleIds(req.query.audienceRoleIds);

  try {
    const [notices, lookups] = await Promise.all([
      noticeService.listNotices({
        search: search || undefined,
        status: status || undefined,
        includeExpired,
        audienceRoleIds,
      }),
      noticeService.getNoticeLookups(),
    ]);

    const filters = {
      search,
      status,
      includeExpired,
      audienceRoleIds,
    };

    const indexState = serializeState({
      notices,
      lookups,
      filters,
      csrfToken: res.locals.csrfToken || null,
      canManage: canManage(res),
    });

    res.render("pages/notices/index", {
      title: "Notices",
      notices,
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
      req.flash("warning", "Notice not found.");
    }
    return res.redirect("/notices");
  }

  try {
    const notice = await noticeService.getNoticeById(id);
    if (!notice) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Notice not found.");
      }
      return res.redirect("/notices");
    }

    const detailState = serializeState({
      notice,
      canManage: canManage(res),
      csrfToken: res.locals.csrfToken || null,
    });

    return res.render("pages/notices/detail", {
      title: notice.title,
      notice,
      canManage: canManage(res),
      scripts: createScriptTag("communications.js"),
      detailState,
    });
  } catch (error) {
    return next(error);
  }
};

exports.createForm = async (req, res, next) => {
  try {
    const lookups = await noticeService.getNoticeLookups();
    const canManageNotice = canManage(res);

    if (!canManageNotice) {
      return res.status(403).render("pages/error", {
        title: "Forbidden",
        message: "You do not have permission to manage notices.",
      });
    }

    const formState = serializeState({
      lookups,
      values: {},
      errors: [],
      isEditMode: false,
      noticeId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/notices/create",
    });

    return res.render("pages/notices/form", {
      title: "Create notice",
      lookups,
      formValues: {},
      errors: [],
      isEditMode: false,
      noticeId: null,
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
      message: "You do not have permission to manage notices.",
    });
  }

  let lookups;
  try {
    lookups = await noticeService.getNoticeLookups();
  } catch (error) {
    return next(error);
  }

  const { errors, payload, formValues } = buildNoticePayload(req.body || {});
  payload.createdById = req.session?.userId || null;
  payload.updatedById = req.session?.userId || null;

  if (errors.length > 0) {
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: false,
      noticeId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/notices/create",
    });

    return res.status(400).render("pages/notices/form", {
      title: "Create notice",
      lookups,
      formValues,
      errors,
      isEditMode: false,
      noticeId: null,
      scripts: createScriptTag("communications.js"),
      formState,
    });
  }

  try {
    const notice = await noticeService.createNotice(payload);
    if (typeof req.flash === "function") {
      req.flash("success", "Notice created successfully.");
    }
    return res.redirect(`/notices/${notice.id}`);
  } catch (error) {
    return next(error);
  }
};

exports.editForm = async (req, res, next) => {
  if (!canManage(res)) {
    return res.status(403).render("pages/error", {
      title: "Forbidden",
      message: "You do not have permission to manage notices.",
    });
  }

  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Notice not found.");
    }
    return res.redirect("/notices");
  }

  try {
    const [notice, lookups] = await Promise.all([
      noticeService.getNoticeById(id),
      noticeService.getNoticeLookups(),
    ]);

    if (!notice) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Notice not found.");
      }
      return res.redirect("/notices");
    }

    const formValues = {
      title: notice.title,
      summary: notice.summary || "",
      content: notice.content,
      status: notice.status,
      publishAt: notice.publishAt ? new Date(notice.publishAt).toISOString().slice(0, 16) : "",
      expiresAt: notice.expiresAt ? new Date(notice.expiresAt).toISOString().slice(0, 16) : "",
      isPinned: notice.isPinned,
      audienceRoleIds: notice.audiences.map((audience) => String(audience.roleId)),
    };

    const formState = serializeState({
      lookups,
      values: formValues,
      errors: [],
      isEditMode: true,
      noticeId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/notices/${id}/edit`,
    });

    return res.render("pages/notices/form", {
      title: `Edit notice • ${notice.title}`,
      lookups,
      formValues,
      errors: [],
      isEditMode: true,
      noticeId: id,
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
      message: "You do not have permission to manage notices.",
    });
  }

  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Notice not found.");
    }
    return res.redirect("/notices");
  }

  let lookups;
  try {
    lookups = await noticeService.getNoticeLookups();
  } catch (error) {
    return next(error);
  }

  const { errors, payload, formValues } = buildNoticePayload(req.body || {});
  payload.updatedById = req.session?.userId || null;

  if (errors.length > 0) {
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: true,
      noticeId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/notices/${id}/edit`,
    });

    return res.status(400).render("pages/notices/form", {
      title: "Edit notice",
      lookups,
      formValues,
      errors,
      isEditMode: true,
      noticeId: id,
      scripts: createScriptTag("communications.js"),
      formState,
    });
  }

  try {
    const updated = await noticeService.updateNotice(id, payload);
    if (!updated) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Notice not found.");
      }
      return res.redirect("/notices");
    }

    if (typeof req.flash === "function") {
      req.flash("success", "Notice updated successfully.");
    }
    return res.redirect(`/notices/${updated.id}`);
  } catch (error) {
    return next(error);
  }
};

exports.destroy = async (req, res, next) => {
  if (!canManage(res)) {
    return res.status(403).render("pages/error", {
      title: "Forbidden",
      message: "You do not have permission to manage notices.",
    });
  }

  const id = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(id)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Notice not found.");
    }
    return res.redirect("/notices");
  }

  try {
    const deleted = await noticeService.deleteNotice(id);
    if (deleted && typeof req.flash === "function") {
      req.flash("success", "Notice deleted successfully.");
    }
    return res.redirect("/notices");
  } catch (error) {
    return next(error);
  }
};
