const teacherService = require("../services/teacherService");
const { serializeState, createScriptTag } = require("../utils/viewHelpers");

function normalizeString(value) {
  if (typeof value !== "string") {
    return null;
  }
  const trimmed = value.trim();
  return trimmed.length > 0 ? trimmed : null;
}

function parseOptionalInt(value) {
  if (value === undefined || value === null || value === "") {
    return null;
  }
  const parsed = Number.parseInt(value, 10);
  return Number.isNaN(parsed) ? null : parsed;
}

function parseOptionalDate(value, label, errors) {
  if (!value) {
    return null;
  }
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    errors.push(`${label} must be a valid date.`);
    return null;
  }
  return date;
}

function buildTeacherPayload(body) {
  const errors = [];

  const person = {
    firstName: normalizeString(body.firstName),
    middleName: normalizeString(body.middleName),
    lastName: normalizeString(body.lastName),
    preferredName: normalizeString(body.preferredName),
    dateOfBirth: null,
    genderId: parseOptionalInt(body.genderId),
    primaryEmail: normalizeString(body.primaryEmail),
    mobilePhone: normalizeString(body.mobilePhone),
  };

  if (!person.firstName) {
    errors.push("First name is required.");
  }

  if (!person.lastName) {
    errors.push("Last name is required.");
  }

  person.dateOfBirth = parseOptionalDate(body.dateOfBirth, "Date of birth", errors);

  const teacher = {
    employeeNumber: normalizeString(body.employeeNumber),
    staffTypeId: parseOptionalInt(body.staffTypeId),
    employmentStatusId: parseOptionalInt(body.employmentStatusId),
    hireDate: parseOptionalDate(body.hireDate, "Hire date", errors),
    contractEndDate: parseOptionalDate(body.contractEndDate, "Contract end date", errors),
    primarySubject: normalizeString(body.primarySubject),
    notes: normalizeString(body.notes),
  };

  if (!teacher.staffTypeId) {
    errors.push("Staff type is required.");
  }

  if (!teacher.employmentStatusId) {
    errors.push("Employment status is required.");
  }

  const formValues = {
    firstName: body.firstName || "",
    middleName: body.middleName || "",
    lastName: body.lastName || "",
    preferredName: body.preferredName || "",
    dateOfBirth: body.dateOfBirth || "",
    genderId: body.genderId || "",
    primaryEmail: body.primaryEmail || "",
    mobilePhone: body.mobilePhone || "",
    employeeNumber: body.employeeNumber || "",
    staffTypeId: body.staffTypeId || "",
    employmentStatusId: body.employmentStatusId || "",
    hireDate: body.hireDate || "",
    contractEndDate: body.contractEndDate || "",
    primarySubject: body.primarySubject || "",
    notes: body.notes || "",
  };

  return { errors, payload: { person, teacher }, formValues };
}

function pushInlineAlert(res, type, message) {
  if (!Array.isArray(res.locals.flashMessages)) {
    res.locals.flashMessages = [];
  }
  res.locals.flashMessages.push({ type, message });
}

function parseId(param) {
  const parsed = Number.parseInt(param, 10);
  return Number.isNaN(parsed) ? null : parsed;
}

exports.index = async (req, res, next) => {
  const search = typeof req.query.search === "string" ? req.query.search.trim() : "";

  try {
    const [teachers, lookups] = await Promise.all([
      teacherService.listTeachers({ search: search || undefined }),
      teacherService.getTeacherLookups(),
    ]);

    const indexState = serializeState({
      teachers,
      search,
      lookups: {
        staffTypes: lookups.staffTypes,
        employmentStatuses: lookups.employmentStatuses,
      },
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/teachers/index", {
      title: "Teachers & Staff",
      teachers,
      search,
      lookups,
      scripts: createScriptTag("teachers.js"),
      indexState,
    });
  } catch (error) {
    next(error);
  }
};

exports.show = async (req, res, next) => {
  const id = parseId(req.params.id);

  if (!id) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Teacher not found.");
    }
    return res.redirect("/teachers");
  }

  try {
    const teacher = await teacherService.getTeacherById(id);

    if (!teacher) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Teacher not found.");
      }
      return res.redirect("/teachers");
    }

    const detailState = serializeState({
      teacher,
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/teachers/detail", {
      title: `${teacher.person.firstName} ${teacher.person.lastName}`,
      teacher,
      scripts: createScriptTag("teachers.js"),
      detailState,
    });
  } catch (error) {
    next(error);
  }
};

exports.createForm = async (req, res, next) => {
  try {
    const lookups = await teacherService.getTeacherLookups();

    const formState = serializeState({
      lookups,
      values: {},
      errors: [],
      isEditMode: false,
      teacherId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/teachers/create",
    });

    res.render("pages/teachers/form", {
      title: "Add teacher or staff member",
      lookups,
      formValues: {},
      errors: [],
      isEditMode: false,
      teacherId: null,
      scripts: createScriptTag("teachers.js"),
      formState,
    });
  } catch (error) {
    next(error);
  }
};

exports.create = async (req, res, next) => {
  let lookups;
  try {
    lookups = await teacherService.getTeacherLookups();
  } catch (error) {
    return next(error);
  }

  const { errors, payload, formValues } = buildTeacherPayload(req.body);

  if (errors.length > 0) {
    pushInlineAlert(res, "danger", "Please correct the errors below.");
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: false,
      teacherId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/teachers/create",
    });

    return res.status(422).render("pages/teachers/form", {
      title: "Add teacher or staff member",
      lookups,
      formValues,
      errors,
      isEditMode: false,
      teacherId: null,
      scripts: createScriptTag("teachers.js"),
      formState,
    });
  }

  try {
    const teacher = await teacherService.createTeacher(payload);
    if (typeof req.flash === "function") {
      req.flash("success", "Teacher saved successfully.");
    }
    return res.redirect(`/teachers/${teacher.id}`);
  } catch (error) {
    pushInlineAlert(res, "danger", "Unable to save the teacher. Please review the details and try again.");
    const combinedErrors = [
      ...errors,
      "A database error occurred while saving the teacher.",
    ];
    const formState = serializeState({
      lookups,
      values: formValues,
      errors: combinedErrors,
      isEditMode: false,
      teacherId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/teachers/create",
    });

    return res.status(500).render("pages/teachers/form", {
      title: "Add teacher or staff member",
      lookups,
      formValues,
      errors: combinedErrors,
      isEditMode: false,
      teacherId: null,
      scripts: createScriptTag("teachers.js"),
      formState,
    });
  }
};

exports.editForm = async (req, res, next) => {
  const id = parseId(req.params.id);

  if (!id) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Teacher not found.");
    }
    return res.redirect("/teachers");
  }

  try {
    const [teacher, lookups] = await Promise.all([
      teacherService.getTeacherById(id),
      teacherService.getTeacherLookups(),
    ]);

    if (!teacher) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Teacher not found.");
      }
      return res.redirect("/teachers");
    }

    const formValues = {
      firstName: teacher.person.firstName || "",
      middleName: teacher.person.middleName || "",
      lastName: teacher.person.lastName || "",
      preferredName: teacher.person.preferredName || "",
      dateOfBirth: teacher.person.dateOfBirth
        ? teacher.person.dateOfBirth.toISOString().slice(0, 10)
        : "",
      genderId: teacher.person.genderId || "",
      primaryEmail: teacher.person.primaryEmail || "",
      mobilePhone: teacher.person.mobilePhone || "",
      employeeNumber: teacher.employeeNumber || "",
      staffTypeId: teacher.staffTypeId || "",
      employmentStatusId: teacher.employmentStatusId || "",
      hireDate: teacher.hireDate ? teacher.hireDate.toISOString().slice(0, 10) : "",
      contractEndDate: teacher.contractEndDate
        ? teacher.contractEndDate.toISOString().slice(0, 10)
        : "",
      primarySubject: teacher.primarySubject || "",
      notes: teacher.notes || "",
    };

    const formState = serializeState({
      lookups,
      values: formValues,
      errors: [],
      isEditMode: true,
      teacherId: teacher.id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/teachers/${teacher.id}/edit`,
    });

    res.render("pages/teachers/form", {
      title: "Edit teacher or staff member",
      lookups,
      formValues,
      errors: [],
      isEditMode: true,
      teacherId: teacher.id,
      scripts: createScriptTag("teachers.js"),
      formState,
    });
  } catch (error) {
    next(error);
  }
};

exports.update = async (req, res, next) => {
  const id = parseId(req.params.id);

  if (!id) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Teacher not found.");
    }
    return res.redirect("/teachers");
  }

  let lookups;
  try {
    lookups = await teacherService.getTeacherLookups();
  } catch (error) {
    return next(error);
  }

  const { errors, payload, formValues } = buildTeacherPayload(req.body);

  if (errors.length > 0) {
    pushInlineAlert(res, "danger", "Please correct the errors below.");
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: true,
      teacherId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/teachers/${id}/edit`,
    });

    return res.status(422).render("pages/teachers/form", {
      title: "Edit teacher or staff member",
      lookups,
      formValues,
      errors,
      isEditMode: true,
      teacherId: id,
      scripts: createScriptTag("teachers.js"),
      formState,
    });
  }

  try {
    const teacher = await teacherService.updateTeacher(id, payload);

    if (!teacher) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Teacher not found.");
      }
      return res.redirect("/teachers");
    }

    if (typeof req.flash === "function") {
      req.flash("success", "Teacher updated successfully.");
    }
    return res.redirect(`/teachers/${teacher.id}`);
  } catch (error) {
    pushInlineAlert(res, "danger", "Unable to update the teacher. Please review the details and try again.");
    const combinedErrors = [
      ...errors,
      "A database error occurred while updating the teacher.",
    ];
    const formState = serializeState({
      lookups,
      values: formValues,
      errors: combinedErrors,
      isEditMode: true,
      teacherId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/teachers/${id}/edit`,
    });

    return res.status(500).render("pages/teachers/form", {
      title: "Edit teacher or staff member",
      lookups,
      formValues,
      errors: combinedErrors,
      isEditMode: true,
      teacherId: id,
      scripts: createScriptTag("teachers.js"),
      formState,
    });
  }
};

exports.destroy = async (req, res, next) => {
  const id = parseId(req.params.id);

  if (!id) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Teacher not found.");
    }
    return res.redirect("/teachers");
  }

  try {
    const removed = await teacherService.deleteTeacher(id);

    if (!removed) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Teacher not found.");
      }
    } else if (typeof req.flash === "function") {
      req.flash("success", "Teacher deleted successfully.");
    }

    return res.redirect("/teachers");
  } catch (error) {
    if (typeof req.flash === "function") {
      req.flash("danger", "Unable to delete the teacher. Please try again.");
    }
    return res.redirect("/teachers");
  }
};

exports.apiSearch = async (req, res, next) => {
  const term = typeof req.query.q === "string" ? req.query.q : "";

  try {
    const results = await teacherService.searchTeachers(term);
    res.json({ results });
  } catch (error) {
    next(error);
  }
};
