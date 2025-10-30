const libraryService = require("../services/libraryService");
const studentService = require("../services/studentService");
const { serializeState, createScriptTag } = require("../utils/viewHelpers");

function wantsJson(req) {
  const accept = req.get("accept") || "";
  return req.xhr || accept.includes("application/json");
}

function parseId(value) {
  const parsed = Number.parseInt(value, 10);
  return Number.isNaN(parsed) ? null : parsed;
}

function respond(req, res, payload, redirectUrl) {
  if (wantsJson(req)) {
    if (payload.error) {
      return res.status(payload.statusCode || 400).json(payload);
    }
    const status = payload.statusCode || 200;
    return res.status(status).json(payload);
  }

  if (payload.error && typeof req.flash === "function") {
    req.flash("danger", payload.error);
  } else if (payload.message && typeof req.flash === "function") {
    req.flash("success", payload.message);
  }

  return res.redirect(redirectUrl);
}

function validateBookPayload(body) {
  const errors = [];
  const title = typeof body.title === "string" ? body.title.trim() : "";
  const isbn = typeof body.isbn === "string" ? body.isbn.trim() : "";
  const author = typeof body.author === "string" ? body.author.trim() : "";
  const publisher = typeof body.publisher === "string" ? body.publisher.trim() : "";
  const summary = typeof body.summary === "string" ? body.summary.trim() : "";
  const publishedYear = typeof body.publishedYear === "string" ? body.publishedYear.trim() : body.publishedYear;

  if (!title) {
    errors.push("Title is required.");
  }

  if (publishedYear) {
    const parsedYear = Number.parseInt(publishedYear, 10);
    if (Number.isNaN(parsedYear)) {
      errors.push("Published year must be a number.");
    }
  }

  return {
    errors,
    payload: {
      title,
      isbn,
      author,
      publisher,
      summary,
      publishedYear,
      categoryId: body.categoryId,
      isArchived: body.isArchived === "true" || body.isArchived === true,
    },
  };
}

function validateCopyPayload(body) {
  const barcode = typeof body.barcode === "string" ? body.barcode.trim() : "";
  const status = typeof body.status === "string" && body.status ? body.status : "AVAILABLE";
  const location = typeof body.location === "string" ? body.location.trim() : "";
  return {
    errors: [],
    payload: {
      barcode,
      status,
      location,
      acquiredAt: body.acquiredAt,
      isArchived: body.isArchived === "true" || body.isArchived === true,
    },
  };
}

function validateLoanPayload(body) {
  const copyId = parseId(body.copyId);
  const studentId = parseId(body.studentId);
  const dueAt = body.dueAt;
  const issuedAt = body.issuedAt;
  const notes = typeof body.notes === "string" ? body.notes.trim() : "";

  const errors = [];
  if (!copyId) {
    errors.push("Copy is required.");
  }
  if (!studentId) {
    errors.push("Student is required.");
  }

  return {
    errors,
    payload: {
      copyId,
      studentId,
      dueAt,
      issuedAt,
      notes,
    },
  };
}

exports.index = async (req, res, next) => {
  try {
    const [categories, books, recentLoans] = await Promise.all([
      libraryService.listCategories({ includeInactive: false }),
      libraryService.searchBooks("", { limit: 25 }),
      libraryService.getRecentLoans(10),
    ]);

    const indexState = serializeState({
      categories,
      books,
      recentLoans,
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/library/index", {
      title: "Library",
      categories,
      books,
      recentLoans,
      scripts: createScriptTag("library.js"),
      indexState,
    });
  } catch (error) {
    next(error);
  }
};

exports.searchBooks = async (req, res, next) => {
  try {
    const term = typeof req.query.term === "string" ? req.query.term : "";
    const categoryId = parseId(req.query.categoryId);
    const books = await libraryService.searchBooks(term, { categoryId });
    res.json({ results: books });
  } catch (error) {
    next(error);
  }
};

exports.recentLoans = async (req, res, next) => {
  try {
    const limit = parseId(req.query.limit) || 10;
    const loans = await libraryService.getRecentLoans(limit);
    res.json({ loans });
  } catch (error) {
    next(error);
  }
};

exports.bookDetail = async (req, res, next) => {
  const bookId = parseId(req.params.bookId);
  if (!bookId) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Book not found.");
    }
    return res.redirect("/library");
  }

  try {
    const [detail, categories] = await Promise.all([
      libraryService.getBookById(bookId),
      libraryService.listCategories({ includeInactive: false }),
    ]);

    if (!detail) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Book not found.");
      }
      return res.redirect("/library");
    }

    const bookState = serializeState({
      ...detail,
      categories,
      csrfToken: res.locals.csrfToken || null,
    });

    if (wantsJson(req)) {
      return res.json({ ...detail, categories });
    }

    return res.render("pages/library/book", {
      title: `${detail.book.title} | Library`,
      book: detail.book,
      activeLoans: detail.activeLoans,
      recentLoans: detail.recentLoans,
      categories,
      scripts: createScriptTag("library.js"),
      bookState,
    });
  } catch (error) {
    return next(error);
  }
};

exports.createBook = async (req, res, next) => {
  const { errors, payload } = validateBookPayload(req.body || {});
  if (errors.length) {
    return respond(
      req,
      res,
      { error: errors.join(" "), errors, statusCode: 400 },
      "/library"
    );
  }

  try {
    const book = await libraryService.createBook(payload);
    return respond(
      req,
      res,
      { message: "Book created successfully.", book, statusCode: 201 },
      `/library/books/${book.id}`
    );
  } catch (error) {
    return next(error);
  }
};

exports.updateBook = async (req, res, next) => {
  const bookId = parseId(req.params.bookId);
  if (!bookId) {
    return respond(
      req,
      res,
      { error: "Book not found.", statusCode: 404 },
      "/library"
    );
  }

  const { errors, payload } = validateBookPayload(req.body || {});
  if (errors.length) {
    return respond(
      req,
      res,
      { error: errors.join(" "), errors, statusCode: 400 },
      `/library/books/${bookId}`
    );
  }

  try {
    const book = await libraryService.updateBook(bookId, payload);
    return respond(
      req,
      res,
      { message: "Book updated successfully.", book },
      `/library/books/${bookId}`
    );
  } catch (error) {
    return next(error);
  }
};

exports.createCopy = async (req, res, next) => {
  const bookId = parseId(req.params.bookId);
  if (!bookId) {
    return respond(
      req,
      res,
      { error: "Book not found.", statusCode: 404 },
      "/library"
    );
  }

  const { payload } = validateCopyPayload(req.body || {});

  try {
    const copy = await libraryService.createCopy(bookId, payload);
    return respond(
      req,
      res,
      { message: "Copy added successfully.", copy },
      `/library/books/${bookId}`
    );
  } catch (error) {
    return next(error);
  }
};

exports.issueLoan = async (req, res, next) => {
  const { errors, payload } = validateLoanPayload(req.body || {});
  const bookId = parseId(req.body?.bookId);
  if (errors.length) {
    return respond(
      req,
      res,
      { error: errors.join(" "), errors, statusCode: 400 },
      bookId ? `/library/books/${bookId}` : "/library"
    );
  }

  try {
    const loan = await libraryService.issueLoan(payload.copyId, payload.studentId, {
      issuedById: req.session?.userId || null,
      dueAt: payload.dueAt,
      issuedAt: payload.issuedAt,
      notes: payload.notes,
    });
    return respond(
      req,
      res,
      { message: "Loan recorded successfully.", loan, statusCode: 201 },
      bookId ? `/library/books/${bookId}` : "/library"
    );
  } catch (error) {
    return respond(
      req,
      res,
      { error: error.message, statusCode: 400 },
      bookId ? `/library/books/${bookId}` : "/library"
    );
  }
};

exports.returnLoan = async (req, res, next) => {
  const loanId = parseId(req.params.loanId);
  if (!loanId) {
    return respond(
      req,
      res,
      { error: "Loan not found.", statusCode: 404 },
      "/library"
    );
  }

  try {
    const loan = await libraryService.returnLoan(loanId, {
      receivedById: req.session?.userId || null,
      returnedAt: req.body?.returnedAt,
    });
    return respond(
      req,
      res,
      { message: "Loan closed successfully.", loan },
      req.get("referer") || "/library"
    );
  } catch (error) {
    return respond(
      req,
      res,
      { error: error.message, statusCode: 400 },
      req.get("referer") || "/library"
    );
  }
};

exports.studentSearch = async (req, res, next) => {
  try {
    const term = typeof req.query.term === "string" ? req.query.term : "";
    const results = await studentService.searchStudents(term, 10);
    res.json({ results });
  } catch (error) {
    next(error);
  }
};
