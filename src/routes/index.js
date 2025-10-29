const { Router } = require("express");
const homeController = require("../controllers/homeController");
const dashboardController = require("../controllers/dashboardController");
const ensureAuth = require("../middleware/ensureAuth");
const authRoutes = require("./authRoutes");
const studentRoutes = require("./studentRoutes");
const teacherRoutes = require("./teacherRoutes");
const classRoutes = require("./classRoutes");
const reportRoutes = require("./reportRoutes");

const router = Router();

router.get("/", homeController.getHome);
router.get("/dashboard", ensureAuth, dashboardController.index);
router.use("/auth", authRoutes);
router.use("/students", studentRoutes);
router.use("/teachers", teacherRoutes);
router.use("/classes", classRoutes);
router.use("/reports", reportRoutes);

module.exports = router;
