const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const teacherController = require("../controllers/teacherController");

const router = Router();
const manageTeachersGuard = ensureAnyRole(["Admin", "Staff"]);

router.use(ensureAuth, manageTeachersGuard);

router.get("/api/search", teacherController.apiSearch);

router.get("/", teacherController.index);
router.get("/create", teacherController.createForm);
router.post("/create", teacherController.create);
router.get("/:id/edit", teacherController.editForm);
router.post("/:id/edit", teacherController.update);
router.post("/:id/delete", teacherController.destroy);
router.get("/:id", teacherController.show);

module.exports = router;
