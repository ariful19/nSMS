const academicService = require("../services/academicService");
const { serializeState, createScriptTag } = require("../utils/viewHelpers");

function wantsJson(req) {
  if (!req) {
    return false;
  }
  const accepts = req.accepts(["json", "html"]);
  return accepts === "json" || req.headers.accept === "application/json";
}

function normalizeString(value) {
  if (typeof value !== "string") {
    return "";
  }
  return value.trim();
}

function parseOptionalDate(value, label, errors) {
  const trimmed = normalizeString(value);
  if (!trimmed) {
    return null;
  }
  const date = new Date(trimmed);
  if (Number.isNaN(date.getTime())) {
    if (errors) {
      errors.push(`${label} must be a valid date.`);
    }
    return null;
  }
  return date.toISOString();
}

function parseClassroomPayload(body) {
  const errors = [];
  const values = {
    name: normalizeString(body.name),
    code: normalizeString(body.code),
    description: normalizeString(body.description),
    gradeLevelId: body.gradeLevelId || "",
    homeroomTeacherId: body.homeroomTeacherId || "",
    startDate: body.startDate || "",
    endDate: body.endDate || "",
  };

  if (!values.name) {
    errors.push("Class name is required.");
  }

  const startDate = parseOptionalDate(values.startDate, "Start date", errors);
  const endDate = parseOptionalDate(values.endDate, "End date", errors);

  return {
    errors,
    values,
    payload: {
      name: values.name,
      code: values.code || null,
      description: values.description || null,
      gradeLevelId: values.gradeLevelId,
      homeroomTeacherId: values.homeroomTeacherId,
      startDate,
      endDate,
    },
  };
}

function parseSubjectPayload(body) {
  const errors = [];
  const values = {
    name: normalizeString(body.name),
    code: normalizeString(body.code),
    description: normalizeString(body.description),
  };

  if (!values.name) {
    errors.push("Subject name is required.");
  }

  return {
    errors,
    values,
    payload: {
      name: values.name,
      code: values.code || null,
      description: values.description || null,
    },
  };
}

async function respondWithManagementState(res, status = 200) {
  const management = await academicService.getClassManagementData();
  return res.status(status).json({ management });
}

async function respondWithClassDetail(res, classroomId, status = 200) {
  const detail = await academicService.getClassDetail(classroomId);
  if (!detail) {
    return res.status(404).json({ message: "Class not found" });
  }
  return res.status(status).json({ detail });
}

exports.index = async (req, res, next) => {
  try {
    const management = await academicService.getClassManagementData();
    if (wantsJson(req)) {
      return res.json({ management });
    }
    const state = serializeState({
      ...management,
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/classes/index", {
      title: "Classes and subjects",
      classes: management.classes,
      subjects: management.subjects,
      lookups: management.lookups,
      scripts: createScriptTag("academic.js"),
      managementState: state,
    });
  } catch (error) {
    next(error);
  }
};

exports.createClass = async (req, res, next) => {
  const { errors, values, payload } = parseClassroomPayload(req.body || {});

  if (errors.length > 0) {
    if (wantsJson(req)) {
      return res.status(422).json({ errors });
    }
    try {
      const management = await academicService.getClassManagementData();
      return res.status(422).render("pages/classes/index", {
        title: "Classes and subjects",
        classes: management.classes,
        subjects: management.subjects,
        lookups: management.lookups,
        classFormErrors: errors,
        classFormValues: values,
        scripts: createScriptTag("academic.js"),
        managementState: serializeState({
          ...management,
          csrfToken: res.locals.csrfToken || null,
        }),
      });
    } catch (error) {
      return next(error);
    }
  }

  try {
    const classroom = await academicService.createClassroom(payload);
    if (wantsJson(req)) {
      return respondWithManagementState(res, 201);
    }
    if (typeof req.flash === "function") {
      req.flash("success", `Class “${payload.name}” created.`);
    }
    return res.redirect(`/classes/${classroom.id}`);
  } catch (error) {
    return next(error);
  }
};

exports.updateClass = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(classroomId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid class id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Class not found.");
    }
    return res.redirect("/classes");
  }

  const { errors, values, payload } = parseClassroomPayload(req.body || {});
  if (errors.length > 0) {
    if (wantsJson(req)) {
      return res.status(422).json({ errors });
    }
    try {
      const detail = await academicService.getClassDetail(classroomId);
      if (!detail) {
        if (typeof req.flash === "function") {
          req.flash("warning", "Class not found.");
        }
        return res.redirect("/classes");
      }
      return res.status(422).render("pages/classes/detail", {
        title: detail.classroom.name,
        detail,
        classFormErrors: errors,
        classFormValues: values,
        scripts: createScriptTag("academic.js"),
        detailState: serializeState({
          ...detail,
          csrfToken: res.locals.csrfToken || null,
        }),
      });
    } catch (error) {
      return next(error);
    }
  }

  try {
    await academicService.updateClassroom(classroomId, payload);
    if (wantsJson(req)) {
      return respondWithClassDetail(res, classroomId, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Class updated.");
    }
    return res.redirect(`/classes/${classroomId}`);
  } catch (error) {
    return next(error);
  }
};

exports.archiveClass = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(classroomId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid class id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Class not found.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.setClassroomArchived(classroomId, true);
    if (wantsJson(req)) {
      return respondWithManagementState(res, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("info", "Class archived.");
    }
    return res.redirect("/classes");
  } catch (error) {
    return next(error);
  }
};

exports.restoreClass = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(classroomId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid class id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Class not found.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.setClassroomArchived(classroomId, false);
    if (wantsJson(req)) {
      return respondWithManagementState(res, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Class restored.");
    }
    return res.redirect("/classes");
  } catch (error) {
    return next(error);
  }
};

exports.createSubject = async (req, res, next) => {
  const { errors, payload, values } = parseSubjectPayload(req.body || {});
  if (errors.length > 0) {
    if (wantsJson(req)) {
      return res.status(422).json({ errors });
    }
    try {
      const management = await academicService.getClassManagementData();
      return res.status(422).render("pages/classes/index", {
        title: "Classes and subjects",
        classes: management.classes,
        subjects: management.subjects,
        lookups: management.lookups,
        subjectFormErrors: errors,
        subjectFormValues: values,
        scripts: createScriptTag("academic.js"),
        managementState: serializeState({
          ...management,
          csrfToken: res.locals.csrfToken || null,
        }),
      });
    } catch (error) {
      return next(error);
    }
  }

  try {
    await academicService.createSubject(payload);
    if (wantsJson(req)) {
      return respondWithManagementState(res, 201);
    }
    if (typeof req.flash === "function") {
      req.flash("success", `Subject “${payload.name}” created.`);
    }
    return res.redirect("/classes");
  } catch (error) {
    return next(error);
  }
};

exports.updateSubject = async (req, res, next) => {
  const subjectId = Number.parseInt(req.params.subjectId, 10);
  if (Number.isNaN(subjectId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid subject id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Subject not found.");
    }
    return res.redirect("/classes");
  }

  const { errors, payload } = parseSubjectPayload(req.body || {});
  if (errors.length > 0) {
    if (wantsJson(req)) {
      return res.status(422).json({ errors });
    }
    try {
      const management = await academicService.getClassManagementData();
      return res.status(422).render("pages/classes/index", {
        title: "Classes and subjects",
        classes: management.classes,
        subjects: management.subjects,
        lookups: management.lookups,
        subjectFormErrors: errors,
        subjectFormValues: { ...payload, id: subjectId },
        scripts: createScriptTag("academic.js"),
        managementState: serializeState({
          ...management,
          csrfToken: res.locals.csrfToken || null,
        }),
      });
    } catch (error) {
      return next(error);
    }
  }

  try {
    await academicService.updateSubject(subjectId, payload);
    if (wantsJson(req)) {
      return respondWithManagementState(res, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Subject updated.");
    }
    return res.redirect("/classes");
  } catch (error) {
    return next(error);
  }
};

exports.archiveSubject = async (req, res, next) => {
  const subjectId = Number.parseInt(req.params.subjectId, 10);
  if (Number.isNaN(subjectId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid subject id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Subject not found.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.setSubjectArchived(subjectId, true);
    if (wantsJson(req)) {
      return respondWithManagementState(res, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("info", "Subject archived.");
    }
    return res.redirect("/classes");
  } catch (error) {
    return next(error);
  }
};

exports.restoreSubject = async (req, res, next) => {
  const subjectId = Number.parseInt(req.params.subjectId, 10);
  if (Number.isNaN(subjectId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid subject id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Subject not found.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.setSubjectArchived(subjectId, false);
    if (wantsJson(req)) {
      return respondWithManagementState(res, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Subject restored.");
    }
    return res.redirect("/classes");
  } catch (error) {
    return next(error);
  }
};

exports.show = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(classroomId)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Class not found.");
    }
    return res.redirect("/classes");
  }

  try {
    const detail = await academicService.getClassDetail(classroomId);
    if (!detail) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Class not found.");
      }
      return res.redirect("/classes");
    }

    if (wantsJson(req)) {
      return res.json({ detail });
    }

    const state = serializeState({
      detail,
      csrfToken: res.locals.csrfToken || null,
    });

    return res.render("pages/classes/detail", {
      title: detail.classroom.name,
      detail,
      scripts: createScriptTag("academic.js"),
      detailState: state,
    });
  } catch (error) {
    return next(error);
  }
};

exports.assignSubject = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(classroomId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid class id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Class not found.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.assignSubjectToClassroom(classroomId, req.body || {});
    if (wantsJson(req)) {
      return respondWithClassDetail(res, classroomId, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Subject assignment saved.");
    }
    return res.redirect(`/classes/${classroomId}`);
  } catch (error) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: error.message });
    }
    if (typeof req.flash === "function") {
      req.flash("danger", error.message);
    }
    return res.redirect(`/classes/${classroomId}`);
  }
};

exports.removeSubject = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  const classSubjectId = Number.parseInt(req.params.classSubjectId, 10);
  if (Number.isNaN(classroomId) || Number.isNaN(classSubjectId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid identifiers" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Unable to update subject.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.removeSubjectFromClassroom(classSubjectId);
    if (wantsJson(req)) {
      return respondWithClassDetail(res, classroomId, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("info", "Subject removed from class.");
    }
    return res.redirect(`/classes/${classroomId}`);
  } catch (error) {
    return next(error);
  }
};

exports.searchStudents = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(classroomId)) {
    return res.status(400).json({ message: "Invalid class id" });
  }

  try {
    const results = await academicService.listAvailableStudents(classroomId, {
      search: req.query.search,
    });
    return res.json({ students: results });
  } catch (error) {
    return next(error);
  }
};

exports.addEnrollment = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  if (Number.isNaN(classroomId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid class id" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Class not found.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.addEnrollment(classroomId, req.body || {});
    if (wantsJson(req)) {
      return respondWithClassDetail(res, classroomId, 201);
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Student enrolled in class.");
    }
    return res.redirect(`/classes/${classroomId}`);
  } catch (error) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: error.message });
    }
    if (typeof req.flash === "function") {
      req.flash("danger", error.message);
    }
    return res.redirect(`/classes/${classroomId}`);
  }
};

exports.removeEnrollment = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  const enrollmentId = Number.parseInt(req.params.enrollmentId, 10);
  if (Number.isNaN(classroomId) || Number.isNaN(enrollmentId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid identifiers" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Unable to update enrollment.");
    }
    return res.redirect("/classes");
  }

  try {
    await academicService.removeEnrollment(enrollmentId);
    if (wantsJson(req)) {
      return respondWithClassDetail(res, classroomId, 200);
    }
    if (typeof req.flash === "function") {
      req.flash("info", "Student removed from class.");
    }
    return res.redirect(`/classes/${classroomId}`);
  } catch (error) {
    return next(error);
  }
};

function parseAssessmentPayload(body) {
  const errors = [];
  const values = {
    title: normalizeString(body.title),
    assessmentTypeId: body.assessmentTypeId || "",
    dueDate: body.dueDate || "",
    maxScore: body.maxScore || "",
    description: normalizeString(body.description),
  };

  if (!values.title) {
    errors.push("Assessment title is required.");
  }
  if (!values.assessmentTypeId) {
    errors.push("Assessment type is required.");
  }

  const dueDate = parseOptionalDate(values.dueDate, "Due date", errors);

  return {
    errors,
    values,
    payload: {
      title: values.title,
      assessmentTypeId: values.assessmentTypeId,
      dueDate,
      maxScore: values.maxScore,
      description: values.description,
    },
  };
}

exports.gradeEntry = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  const classSubjectId = Number.parseInt(req.params.classSubjectId, 10);
  if (Number.isNaN(classroomId) || Number.isNaN(classSubjectId)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Grade entry screen not found.");
    }
    return res.redirect("/classes");
  }

  try {
    const context = await academicService.getGradeEntryContext(
      classroomId,
      classSubjectId,
    );
    if (!context) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Grade entry screen not found.");
      }
      return res.redirect(`/classes/${classroomId}`);
    }

    if (wantsJson(req)) {
      return res.json({ context });
    }

    const state = serializeState({
      ...context,
      csrfToken: res.locals.csrfToken || null,
    });

    return res.render("pages/classes/grade-entry", {
      title: `${context.classSubject.classroom.name} • ${context.classSubject.subject.name}`,
      context,
      scripts: createScriptTag("academic.js"),
      gradeEntryState: state,
    });
  } catch (error) {
    return next(error);
  }
};

exports.createAssessment = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  const classSubjectId = Number.parseInt(req.params.classSubjectId, 10);
  if (Number.isNaN(classroomId) || Number.isNaN(classSubjectId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid identifiers" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Unable to create assessment.");
    }
    return res.redirect("/classes");
  }

  const { errors, payload } = parseAssessmentPayload(req.body || {});
  if (errors.length > 0) {
    if (wantsJson(req)) {
      return res.status(422).json({ errors });
    }
    if (typeof req.flash === "function") {
      errors.forEach((message) => req.flash("danger", message));
    }
    return res.redirect(`/classes/${classroomId}/subjects/${classSubjectId}/grades`);
  }

  try {
    await academicService.createAssessment(classSubjectId, payload);
    if (wantsJson(req)) {
      const context = await academicService.getGradeEntryContext(
        classroomId,
        classSubjectId,
      );
      return res.status(201).json({ context });
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Assessment created.");
    }
    return res.redirect(`/classes/${classroomId}/subjects/${classSubjectId}/grades`);
  } catch (error) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: error.message });
    }
    if (typeof req.flash === "function") {
      req.flash("danger", error.message);
    }
    return res.redirect(`/classes/${classroomId}/subjects/${classSubjectId}/grades`);
  }
};

exports.saveGrades = async (req, res, next) => {
  const classroomId = Number.parseInt(req.params.id, 10);
  const classSubjectId = Number.parseInt(req.params.classSubjectId, 10);
  if (Number.isNaN(classroomId) || Number.isNaN(classSubjectId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Invalid identifiers" });
    }
    if (typeof req.flash === "function") {
      req.flash("warning", "Unable to save grades.");
    }
    return res.redirect("/classes");
  }

  const assessmentId = Number.parseInt(req.body.assessmentId, 10);
  if (Number.isNaN(assessmentId)) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: "Assessment is required" });
    }
    if (typeof req.flash === "function") {
      req.flash("danger", "Assessment is required.");
    }
    return res.redirect(`/classes/${classroomId}/subjects/${classSubjectId}/grades`);
  }

  const entries = Array.isArray(req.body.entries)
    ? req.body.entries
    : Array.isArray(req.body["entries[]"])
      ? req.body["entries[]"]
      : [];

  const normalizedEntries = Array.isArray(entries)
    ? entries
    : Object.values(entries);

  try {
    await academicService.saveGradeEntries({
      classSubjectId,
      assessmentId,
      entries: normalizedEntries,
      gradedById: req.session && req.session.userId ? req.session.userId : null,
    });

    if (wantsJson(req)) {
      const context = await academicService.getGradeEntryContext(
        classroomId,
        classSubjectId,
      );
      return res.json({ context });
    }
    if (typeof req.flash === "function") {
      req.flash("success", "Grades saved.");
    }
    return res.redirect(`/classes/${classroomId}/subjects/${classSubjectId}/grades`);
  } catch (error) {
    if (wantsJson(req)) {
      return res.status(400).json({ message: error.message });
    }
    if (typeof req.flash === "function") {
      req.flash("danger", error.message);
    }
    return res.redirect(`/classes/${classroomId}/subjects/${classSubjectId}/grades`);
  }
};
