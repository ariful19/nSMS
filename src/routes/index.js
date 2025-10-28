const { Router } = require("express");
const homeController = require("../controllers/homeController");
const dashboardController = require("../controllers/dashboardController");
const ensureAuth = require("../middleware/ensureAuth");
const authRoutes = require("./authRoutes");

const router = Router();

router.get("/", homeController.getHome);
router.get("/dashboard", ensureAuth, dashboardController.index);
router.use("/auth", authRoutes);

module.exports = router;
