const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const classController = require("../controllers/classController");

const router = Router();
const manageClassesGuard = ensureAnyRole(["Admin", "Staff"]);
const gradeAccessGuard = ensureAnyRole(["Admin", "Staff", "Teacher"]);

router.use(ensureAuth);

router.get("/", manageClassesGuard, classController.index);
router.post("/", manageClassesGuard, classController.createClass);

router.post("/subjects", manageClassesGuard, classController.createSubject);
router.post("/subjects/:subjectId/update", manageClassesGuard, classController.updateSubject);
router.post("/subjects/:subjectId/archive", manageClassesGuard, classController.archiveSubject);
router.post("/subjects/:subjectId/restore", manageClassesGuard, classController.restoreSubject);

router.get("/:id", gradeAccessGuard, classController.show);
router.post("/:id/update", manageClassesGuard, classController.updateClass);
router.post("/:id/archive", manageClassesGuard, classController.archiveClass);
router.post("/:id/restore", manageClassesGuard, classController.restoreClass);

router.post(
  "/:id/subjects",
  manageClassesGuard,
  classController.assignSubject,
);
router.post(
  "/:id/subjects/:classSubjectId/delete",
  manageClassesGuard,
  classController.removeSubject,
);

router.get(
  "/:id/enrollment/search",
  manageClassesGuard,
  classController.searchStudents,
);
router.post(
  "/:id/enrollments",
  manageClassesGuard,
  classController.addEnrollment,
);
router.post(
  "/:id/enrollments/:enrollmentId/delete",
  manageClassesGuard,
  classController.removeEnrollment,
);

router.get(
  "/:id/subjects/:classSubjectId/grades",
  gradeAccessGuard,
  classController.gradeEntry,
);
router.post(
  "/:id/subjects/:classSubjectId/assessments",
  gradeAccessGuard,
  classController.createAssessment,
);
router.post(
  "/:id/subjects/:classSubjectId/grades",
  gradeAccessGuard,
  classController.saveGrades,
);

module.exports = router;
