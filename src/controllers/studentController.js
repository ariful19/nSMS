const studentService = require("../services/studentService");

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
    const students = await studentService.listStudents({ search: search || undefined });

    res.render("pages/students/index", {
      title: "Students",
      students,
      search,
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

    res.render("pages/students/detail", {
      title: `${student.person.firstName} ${student.person.lastName}`,
      student,
    });
  } catch (error) {
    next(error);
  }
};

exports.createForm = async (req, res, next) => {
  try {
    const lookups = await studentService.getStudentLookups();

    res.render("pages/students/form", {
      title: "Add student",
      lookups,
      formValues: {},
      errors: [],
      isEditMode: false,
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
    return res.status(422).render("pages/students/form", {
      title: "Add student",
      lookups,
      formValues,
      errors,
      isEditMode: false,
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

    return res.status(500).render("pages/students/form", {
      title: "Add student",
      lookups,
      formValues,
      errors: [
        ...errors,
        "A database error occurred while saving the student.",
      ],
      isEditMode: false,
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

    res.render("pages/students/form", {
      title: "Edit student",
      lookups,
      formValues,
      errors: [],
      isEditMode: true,
      studentId: student.id,
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
    return res.status(422).render("pages/students/form", {
      title: "Edit student",
      lookups,
      formValues,
      errors,
      isEditMode: true,
      studentId: id,
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
    return res.status(500).render("pages/students/form", {
      title: "Edit student",
      lookups,
      formValues,
      errors: [
        ...errors,
        "A database error occurred while updating the student.",
      ],
      isEditMode: true,
      studentId: id,
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
