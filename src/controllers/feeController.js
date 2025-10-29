const feeService = require("../services/feeService");
const studentService = require("../services/studentService");
const { serializeState, createScriptTag } = require("../utils/viewHelpers");

const currencyFormatter = new Intl.NumberFormat("en-US", {
  style: "currency",
  currency: "USD",
  minimumFractionDigits: 2,
});

function wantsJson(req) {
  const accept = req.get("accept") || "";
  return req.xhr || accept.includes("application/json");
}

function formatCurrency(amount) {
  return currencyFormatter.format(Number(amount || 0));
}

function normalizeLineItemsInput(raw) {
  if (!raw) {
    return [];
  }
  if (typeof raw === "string") {
    try {
      const parsed = JSON.parse(raw);
      return Array.isArray(parsed) ? parsed : [parsed];
    } catch (error) {
      return [];
    }
  }
  if (Array.isArray(raw)) {
    return raw;
  }
  if (typeof raw === "object") {
    return [raw];
  }
  return [];
}

function parseLineItems(body) {
  const items = normalizeLineItemsInput(body.lineItems);
  if (!items.length && body.amount !== undefined) {
    items.push(body);
  }

  const errors = [];
  const normalized = items
    .map((item, index) => {
      const amountValue =
        typeof item.amount === "string" ? item.amount.trim() : item.amount;
      if (amountValue === undefined || amountValue === null || amountValue === "") {
        errors.push(`Line ${index + 1}: Amount is required.`);
        return null;
      }
      const amountNumber = Number.parseFloat(amountValue);
      if (Number.isNaN(amountNumber)) {
        errors.push(`Line ${index + 1}: Amount must be a number.`);
        return null;
      }

      const entryDate = item.entryDate ? new Date(item.entryDate) : new Date();
      if (Number.isNaN(entryDate.getTime())) {
        errors.push(`Line ${index + 1}: Entry date is invalid.`);
        return null;
      }

      return {
        categoryId: item.categoryId ? Number.parseInt(item.categoryId, 10) : null,
        amount: amountNumber.toFixed(2),
        entryDate: entryDate.toISOString(),
        description:
          typeof item.description === "string"
            ? item.description.trim()
            : null,
        paymentMethod:
          typeof item.paymentMethod === "string" && item.paymentMethod
            ? item.paymentMethod
            : null,
      };
    })
    .filter(Boolean);

  if (!normalized.length) {
    errors.push("Add at least one line item before submitting.");
  }

  return { errors, lineItems: normalized };
}

function respond(req, res, payload, redirectUrl) {
  if (wantsJson(req)) {
    if (payload.error) {
      return res.status(400).json(payload);
    }
    return res.json(payload);
  }
  if (payload.error && typeof req.flash === "function") {
    req.flash("danger", payload.error);
  } else if (payload.message && typeof req.flash === "function") {
    req.flash("success", payload.message);
  }
  return res.redirect(redirectUrl);
}

exports.index = async (req, res, next) => {
  try {
    const [categories, recentEntries] = await Promise.all([
      feeService.listCategories({ includeInactive: true }),
      feeService.getRecentLedgerEntries(10),
    ]);

    const indexState = serializeState({
      categories,
      recentEntries,
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/fees/index", {
      title: "Fees",
      categories,
      recentEntries,
      scripts: createScriptTag("fees.js"),
      indexState,
      formatCurrency,
    });
  } catch (error) {
    next(error);
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

function parseLedgerFilters(query) {
  const filters = {};
  if (query.startDate) {
    const start = new Date(query.startDate);
    if (!Number.isNaN(start.getTime())) {
      filters.startDate = start;
    }
  }
  if (query.endDate) {
    const end = new Date(query.endDate);
    if (!Number.isNaN(end.getTime())) {
      filters.endDate = end;
    }
  }
  if (query.categoryId) {
    const categoryId = Number.parseInt(query.categoryId, 10);
    if (!Number.isNaN(categoryId)) {
      filters.categoryId = categoryId;
    }
  }
  return filters;
}

exports.ledger = async (req, res, next) => {
  const studentId = Number.parseInt(req.params.studentId, 10);
  if (Number.isNaN(studentId)) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Student not found.");
    }
    return res.redirect("/fees");
  }

  try {
    const filters = parseLedgerFilters(req.query);
    const [ledger, categories] = await Promise.all([
      feeService.getStudentLedger(studentId, filters),
      feeService.listCategories({ includeInactive: false }),
    ]);

    if (!ledger) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Student not found.");
      }
      return res.redirect("/fees");
    }

    const ledgerState = serializeState({
      ledger,
      categories,
      filters: {
        startDate: req.query.startDate || "",
        endDate: req.query.endDate || "",
        categoryId: req.query.categoryId || "",
      },
      endpoints: {
        createCharge: `/fees/ledger/${studentId}/charges`,
        createPayment: `/fees/ledger/${studentId}/payments`,
        createAdjustment: `/fees/ledger/${studentId}/adjustments`,
      },
      csrfToken: res.locals.csrfToken || null,
    });

    if (wantsJson(req)) {
      return res.json({
        ledger,
        categories,
        filters: {
          startDate: req.query.startDate || "",
          endDate: req.query.endDate || "",
          categoryId: req.query.categoryId || "",
        },
      });
    }

    res.render("pages/fees/ledger", {
      title: "Student Ledger",
      ledger,
      categories,
      filters: {
        startDate: req.query.startDate || "",
        endDate: req.query.endDate || "",
        categoryId: req.query.categoryId || "",
      },
      scripts: createScriptTag("fees.js"),
      ledgerState,
      formatCurrency,
    });
  } catch (error) {
    next(error);
  }
};

exports.createCharge = async (req, res, next) => {
  const studentId = Number.parseInt(req.params.studentId, 10);
  if (Number.isNaN(studentId)) {
    return respond(req, res, { error: "Student not found." }, "/fees");
  }

  const { errors, lineItems } = parseLineItems(req.body || {});
  if (errors.length) {
    return respond(
      req,
      res,
      { error: errors.join(" ") },
      `/fees/ledger/${studentId}`
    );
  }

  try {
    const entries = await feeService.createCharges(
      studentId,
      lineItems,
      req.user ? req.user.id : null
    );
    return respond(
      req,
      res,
      {
        message: "Charge(s) recorded successfully.",
        entries,
      },
      `/fees/ledger/${studentId}`
    );
  } catch (error) {
    next(error);
  }
};

exports.createPayment = async (req, res, next) => {
  const studentId = Number.parseInt(req.params.studentId, 10);
  if (Number.isNaN(studentId)) {
    return respond(req, res, { error: "Student not found." }, "/fees");
  }

  const { errors, lineItems } = parseLineItems(req.body || {});
  if (errors.length) {
    return respond(
      req,
      res,
      { error: errors.join(" ") },
      `/fees/ledger/${studentId}`
    );
  }

  try {
    const result = await feeService.createPayments(
      studentId,
      lineItems,
      req.user ? req.user.id : null,
      { paymentMethod: req.body.paymentMethod }
    );
    return respond(
      req,
      res,
      {
        message: "Payment recorded successfully.",
        receiptNumber: result.receiptNumber,
        entries: result.entries,
      },
      `/fees/ledger/${studentId}`
    );
  } catch (error) {
    next(error);
  }
};

exports.createAdjustment = async (req, res, next) => {
  const studentId = Number.parseInt(req.params.studentId, 10);
  if (Number.isNaN(studentId)) {
    return respond(req, res, { error: "Student not found." }, "/fees");
  }

  const { errors, lineItems } = parseLineItems(req.body || {});
  if (errors.length) {
    return respond(
      req,
      res,
      { error: errors.join(" ") },
      `/fees/ledger/${studentId}`
    );
  }

  try {
    const entries = await feeService.createAdjustments(
      studentId,
      lineItems,
      req.user ? req.user.id : null
    );
    return respond(
      req,
      res,
      {
        message: "Adjustment recorded successfully.",
        entries,
      },
      `/fees/ledger/${studentId}`
    );
  } catch (error) {
    next(error);
  }
};

exports.receipt = async (req, res, next) => {
  const receiptNumber = req.params.receiptNumber;
  if (!receiptNumber) {
    if (typeof req.flash === "function") {
      req.flash("warning", "Receipt not found.");
    }
    return res.redirect("/fees");
  }

  try {
    const receipt = await feeService.getReceipt(receiptNumber);
    if (!receipt) {
      if (typeof req.flash === "function") {
        req.flash("warning", "Receipt not found.");
      }
      return res.redirect("/fees");
    }

    const receiptState = serializeState({
      receipt,
      csrfToken: res.locals.csrfToken || null,
    });

    res.render("pages/fees/receipt", {
      title: `Receipt ${receiptNumber}`,
      receipt,
      formatCurrency,
      scripts: createScriptTag("fees.js"),
      receiptState,
    });
  } catch (error) {
    next(error);
  }
};
