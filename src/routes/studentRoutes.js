const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const studentController = require("../controllers/studentController");

const router = Router();
const manageStudentsGuard = ensureAnyRole(["Admin", "Staff"]);

router.use(ensureAuth, manageStudentsGuard);

router.get("/api/search", studentController.apiSearch);

router.get("/", studentController.index);
router.get("/create", studentController.createForm);
router.post("/create", studentController.create);
router.get("/:id/edit", studentController.editForm);
router.post("/:id/edit", studentController.update);
router.post("/:id/delete", studentController.destroy);
router.get("/:id", studentController.show);

module.exports = router;
