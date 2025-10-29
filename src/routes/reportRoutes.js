const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const reportController = require("../controllers/reportController");

const router = Router();
const reportGuard = ensureAnyRole(["Admin", "Staff", "Teacher"]);

router.use(ensureAuth, reportGuard);

router.get("/grades", reportController.gradeReportsPage);
router.get("/grades/data", reportController.gradeReportsData);

module.exports = router;
