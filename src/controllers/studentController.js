const studentService = require("../services/studentService");
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

function buildStudentPayload(body) {
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

  const student = {
    studentNumber: normalizeString(body.studentNumber),
    enrollmentStatusId: parseOptionalInt(body.enrollmentStatusId),
    gradeLevelId: parseOptionalInt(body.gradeLevelId),
    admissionDate: parseOptionalDate(body.admissionDate, "Admission date", errors),
    graduationDate: parseOptionalDate(body.graduationDate, "Graduation date", errors),
    notes: normalizeString(body.notes),
  };

  if (!student.enrollmentStatusId) {
    errors.push("Enrollment status is required.");
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
    studentNumber: body.studentNumber || "",
    enrollmentStatusId: body.enrollmentStatusId || "",
    gradeLevelId: body.gradeLevelId || "",
    admissionDate: body.admissionDate || "",
    graduationDate: body.graduationDate || "",
    notes: body.notes || "",
  };

  return { errors, payload: { person, student }, formValues };
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
    const [students, lookups] = await Promise.all([
      studentService.listStudents({ search: search || undefined }),
      studentService.getStudentLookups(),
    ]);

    const indexState = serializeState({
      students,
      search,
      lookups: {
        statuses: lookups.statuses,
        gradeLevels: lookups.gradeLevels,
      },
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/students/index", {
      title: "Students",
      students,
      search,
      lookups,
      scripts: createScriptTag("students.js"),
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
      req.flash("warning", "Student not found.");
    }
    return res.redirect("/students");
  }

  try {
    const student = await studentService.getStudentById(id);

    if (!student) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Student not found.");
      }
      return res.redirect("/students");
    }

    const detailState = serializeState({
      student,
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/students/detail", {
      title: `${student.person.firstName} ${student.person.lastName}`,
      student,
      scripts: createScriptTag("students.js"),
      detailState,
    });
  } catch (error) {
    next(error);
  }
};

exports.createForm = async (req, res, next) => {
  try {
    const lookups = await studentService.getStudentLookups();

    const formState = serializeState({
      lookups,
      values: {},
      errors: [],
      isEditMode: false,
      studentId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/students/create",
    });

    res.render("pages/students/form", {
      title: "Add student",
      lookups,
      formValues: {},
      errors: [],
      isEditMode: false,
      studentId: null,
      scripts: createScriptTag("students.js"),
      formState,
    });
  } catch (error) {
    next(error);
  }
};

exports.create = async (req, res, next) => {
  let lookups;
  try {
    lookups = await studentService.getStudentLookups();
  } catch (error) {
    return next(error);
  }

  const { errors, payload, formValues } = buildStudentPayload(req.body);

  if (errors.length > 0) {
    pushInlineAlert(res, "danger", "Please correct the errors below.");
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: false,
      studentId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/students/create",
    });

    return res.status(422).render("pages/students/form", {
      title: "Add student",
      lookups,
      formValues,
      errors,
      isEditMode: false,
      studentId: null,
      scripts: createScriptTag("students.js"),
      formState,
    });
  }

  try {
    const student = await studentService.createStudent(payload);
    if (typeof req.flash === "function") {
      req.flash("success", "Student created successfully.");
    }
    return res.redirect(`/students/${student.id}`);
  } catch (error) {
    pushInlineAlert(res, "danger", "Unable to save the student. Please review the details and try again.");

    const combinedErrors = [
      ...errors,
      "A database error occurred while saving the student.",
    ];
    const formState = serializeState({
      lookups,
      values: formValues,
      errors: combinedErrors,
      isEditMode: false,
      studentId: null,
      csrfToken: res.locals.csrfToken || null,
      formAction: "/students/create",
    });

    return res.status(500).render("pages/students/form", {
      title: "Add student",
      lookups,
      formValues,
      errors: combinedErrors,
      isEditMode: false,
      studentId: null,
      scripts: createScriptTag("students.js"),
      formState,
    });
  }
};

exports.editForm = async (req, res, next) => {
  const id = parseId(req.params.id);

  if (!id) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Student not found.");
    }
    return res.redirect("/students");
  }

  try {
    const [student, lookups] = await Promise.all([
      studentService.getStudentById(id),
      studentService.getStudentLookups(),
    ]);

    if (!student) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Student not found.");
      }
      return res.redirect("/students");
    }

    const formValues = {
      firstName: student.person.firstName || "",
      middleName: student.person.middleName || "",
      lastName: student.person.lastName || "",
      preferredName: student.person.preferredName || "",
      dateOfBirth: student.person.dateOfBirth
        ? student.person.dateOfBirth.toISOString().slice(0, 10)
        : "",
      genderId: student.person.genderId || "",
      primaryEmail: student.person.primaryEmail || "",
      mobilePhone: student.person.mobilePhone || "",
      studentNumber: student.studentNumber || "",
      enrollmentStatusId: student.enrollmentStatusId || "",
      gradeLevelId: student.gradeLevelId || "",
      admissionDate: student.admissionDate
        ? student.admissionDate.toISOString().slice(0, 10)
        : "",
      graduationDate: student.graduationDate
        ? student.graduationDate.toISOString().slice(0, 10)
        : "",
      notes: student.notes || "",
    };

    const formState = serializeState({
      lookups,
      values: formValues,
      errors: [],
      isEditMode: true,
      studentId: student.id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/students/${student.id}/edit`,
    });

    res.render("pages/students/form", {
      title: "Edit student",
      lookups,
      formValues,
      errors: [],
      isEditMode: true,
      studentId: student.id,
      scripts: createScriptTag("students.js"),
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
      req.flash("warning", "Student not found.");
    }
    return res.redirect("/students");
  }

  let lookups;
  try {
    lookups = await studentService.getStudentLookups();
  } catch (error) {
    return next(error);
  }

  const { errors, payload, formValues } = buildStudentPayload(req.body);

  if (errors.length > 0) {
    pushInlineAlert(res, "danger", "Please correct the errors below.");
    const formState = serializeState({
      lookups,
      values: formValues,
      errors,
      isEditMode: true,
      studentId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/students/${id}/edit`,
    });

    return res.status(422).render("pages/students/form", {
      title: "Edit student",
      lookups,
      formValues,
      errors,
      isEditMode: true,
      studentId: id,
      scripts: createScriptTag("students.js"),
      formState,
    });
  }

  try {
    const student = await studentService.updateStudent(id, payload);

    if (!student) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Student not found.");
      }
      return res.redirect("/students");
    }

    if (typeof req.flash === "function") {
      req.flash("success", "Student updated successfully.");
    }
    return res.redirect(`/students/${student.id}`);
  } catch (error) {
    pushInlineAlert(res, "danger", "Unable to update the student. Please review the details and try again.");
    const combinedErrors = [
      ...errors,
      "A database error occurred while updating the student.",
    ];
    const formState = serializeState({
      lookups,
      values: formValues,
      errors: combinedErrors,
      isEditMode: true,
      studentId: id,
      csrfToken: res.locals.csrfToken || null,
      formAction: `/students/${id}/edit`,
    });

    return res.status(500).render("pages/students/form", {
      title: "Edit student",
      lookups,
      formValues,
      errors: combinedErrors,
      isEditMode: true,
      studentId: id,
      scripts: createScriptTag("students.js"),
      formState,
    });
  }
};

exports.destroy = async (req, res, next) => {
  const id = parseId(req.params.id);

  if (!id) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Student not found.");
    }
    return res.redirect("/students");
  }

  try {
    const removed = await studentService.deleteStudent(id);

    if (!removed) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Student not found.");
      }
    } else if (typeof req.flash === "function") {
      req.flash("success", "Student deleted successfully.");
    }

    return res.redirect("/students");
  } catch (error) {
    if (typeof req.flash === "function") {
      req.flash("danger", "Unable to delete the student. Please try again.");
    }
    return res.redirect("/students");
  }
};

exports.apiSearch = async (req, res, next) => {
  const term = typeof req.query.q === "string" ? req.query.q : "";

  try {
    const results = await studentService.searchStudents(term);
    res.json({ results });
  } catch (error) {
    next(error);
  }
};
