const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const feeController = require("../controllers/feeController");

const router = Router();
const manageFeesGuard = ensureAnyRole(["Admin", "Staff"]);

router.use(ensureAuth, manageFeesGuard);

router.get("/api/students", feeController.studentSearch);
router.get("/", feeController.index);
router.get("/ledger/:studentId", feeController.ledger);
router.post("/ledger/:studentId/charges", feeController.createCharge);
router.post("/ledger/:studentId/payments", feeController.createPayment);
router.post("/ledger/:studentId/adjustments", feeController.createAdjustment);
router.get("/receipt/:receiptNumber", feeController.receipt);

module.exports = router;
