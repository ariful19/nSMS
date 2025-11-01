const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const noticeController = require("../controllers/noticeController");

const router = Router();
const readNoticesGuard = ensureAnyRole(["Admin", "Staff", "Teacher", "Student"]);
const manageNoticesGuard = ensureAnyRole(["Admin", "Staff"]);

router.use(ensureAuth);

router.get("/", readNoticesGuard, noticeController.index);
router.get("/create", manageNoticesGuard, noticeController.createForm);
router.post("/create", manageNoticesGuard, noticeController.create);
router.get("/:id/edit", manageNoticesGuard, noticeController.editForm);
router.post("/:id/edit", manageNoticesGuard, noticeController.update);
router.post("/:id/delete", manageNoticesGuard, noticeController.destroy);
router.get("/:id", readNoticesGuard, noticeController.show);

module.exports = router;
