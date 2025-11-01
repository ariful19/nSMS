const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const eventController = require("../controllers/eventController");

const router = Router();
const readEventsGuard = ensureAnyRole(["Admin", "Staff", "Teacher", "Student"]);
const manageEventsGuard = ensureAnyRole(["Admin", "Staff"]);

router.use(ensureAuth);

router.get("/", readEventsGuard, eventController.index);
router.get("/create", manageEventsGuard, eventController.createForm);
router.post("/create", manageEventsGuard, eventController.create);
router.get("/:id/edit", manageEventsGuard, eventController.editForm);
router.post("/:id/edit", manageEventsGuard, eventController.update);
router.post("/:id/delete", manageEventsGuard, eventController.destroy);
router.post("/:id/rsvp", readEventsGuard, eventController.rsvp);
router.post("/:id/rsvp/cancel", readEventsGuard, eventController.cancelRsvp);
router.get("/:id", readEventsGuard, eventController.show);

module.exports = router;
