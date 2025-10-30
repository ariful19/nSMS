const { Router } = require("express");

const ensureAuth = require("../middleware/ensureAuth");
const ensureAnyRole = require("../middleware/ensureAnyRole");
const libraryController = require("../controllers/libraryController");

const router = Router();
const manageLibraryGuard = ensureAnyRole(["Admin", "Staff"]);

router.use(ensureAuth, manageLibraryGuard);

router.get("/api/books", libraryController.searchBooks);
router.get("/api/students", libraryController.studentSearch);
router.get("/api/loans/recent", libraryController.recentLoans);
router.get("/", libraryController.index);
router.get("/books/:bookId", libraryController.bookDetail);
router.post("/books", libraryController.createBook);
router.post("/books/:bookId", libraryController.updateBook);
router.post("/books/:bookId/copies", libraryController.createCopy);
router.post("/loans", libraryController.issueLoan);
router.post("/loans/:loanId/return", libraryController.returnLoan);

module.exports = router;
