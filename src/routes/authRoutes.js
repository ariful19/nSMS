const { Router } = require("express");
const authController = require("../controllers/authController");
const ensureGuest = require("../middleware/ensureGuest");
const ensureAuth = require("../middleware/ensureAuth");

const router = Router();

router.get("/login", ensureGuest, authController.showLogin);
router.post("/login", ensureGuest, authController.handleLogin);
router.post("/logout", ensureAuth, authController.handleLogout);

module.exports = router;
