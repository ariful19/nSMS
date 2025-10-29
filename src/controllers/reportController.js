const academicService = require("../services/academicService");
const { serializeState, createScriptTag } = require("../utils/viewHelpers");

function parseFilterInt(value) {
  if (value === undefined || value === null || value === "") {
    return undefined;
  }
  const parsed = Number.parseInt(value, 10);
  return Number.isNaN(parsed) ? undefined : parsed;
}

exports.gradeReportsPage = async (req, res, next) => {
  try {
    const report = await academicService.getGradeReport({});
    const state = serializeState({
      report,
      filters: {
        classId: null,
        studentId: null,
      },
      csrfToken: res.locals.csrfToken || null,
    });

    return res.render("pages/reports/grades", {
      title: "Grade reports",
      report,
      scripts: createScriptTag("academic.js"),
      reportState: state,
    });
  } catch (error) {
    return next(error);
  }
};

exports.gradeReportsData = async (req, res, next) => {
  try {
    const classId = parseFilterInt(req.query.classId);
    const studentId = parseFilterInt(req.query.studentId);
    const report = await academicService.getGradeReport({ classId, studentId });
    return res.json({
      report,
      filters: {
        classId: classId || null,
        studentId: studentId || null,
      },
    });
  } catch (error) {
    return next(error);
  }
};
