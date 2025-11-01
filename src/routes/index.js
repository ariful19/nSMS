const { Router } = require("express");
const homeController = require("../controllers/homeController");
const dashboardController = require("../controllers/dashboardController");
const ensureAuth = require("../middleware/ensureAuth");
const authRoutes = require("./authRoutes");
const studentRoutes = require("./studentRoutes");
const teacherRoutes = require("./teacherRoutes");
const classRoutes = require("./classRoutes");
const reportRoutes = require("./reportRoutes");
const feeRoutes = require("./feeRoutes");
const libraryRoutes = require("./libraryRoutes");
const noticeRoutes = require("./noticeRoutes");
const eventRoutes = require("./eventRoutes");

const router = Router();

router.get("/", homeController.getHome);
router.get("/dashboard", ensureAuth, dashboardController.index);
router.use("/auth", authRoutes);
router.use("/students", studentRoutes);
router.use("/teachers", teacherRoutes);
router.use("/classes", classRoutes);
router.use("/reports", reportRoutes);
router.use("/fees", feeRoutes);
router.use("/library", libraryRoutes);
router.use("/notices", noticeRoutes);
router.use("/events", eventRoutes);

module.exports = router;
