const { Prisma } = require("@prisma/client");
const { prisma } = require("../db/client");

const defaultCategoryOrder = [
  { sortOrder: "asc" },
  { name: "asc" },
];

const ledgerInclude = {
  category: true,
  recordedBy: {
    select: {
      id: true,
      username: true,
      email: true,
    },
  },
};

function decimalToNumber(value) {
  if (value === null || value === undefined) {
    return 0;
  }
  if (typeof value === "number") {
    return value;
  }
  return Number.parseFloat(value);
}

function signedAmount(entry) {
  const amount = decimalToNumber(entry.amount);
  if (entry.entryType === "PAYMENT") {
    return amount * -1;
  }
  return amount;
}

function serializeLedgerEntry(entry, runningBalance) {
  return {
    id: entry.id,
    studentId: entry.studentId,
    categoryId: entry.categoryId,
    recordedById: entry.recordedById,
    entryType: entry.entryType,
    paymentMethod: entry.paymentMethod,
    amount: decimalToNumber(entry.amount),
    signedAmount: signedAmount(entry),
    entryDate: entry.entryDate,
    description: entry.description,
    receiptNumber: entry.receiptNumber,
    createdAt: entry.createdAt,
    updatedAt: entry.updatedAt,
    runningBalance: runningBalance,
    category: entry.category
      ? {
          id: entry.category.id,
          name: entry.category.name,
          code: entry.category.code,
        }
      : null,
    recordedBy: entry.recordedBy
      ? {
          id: entry.recordedBy.id,
          username: entry.recordedBy.username,
          email: entry.recordedBy.email,
        }
      : null,
  };
}

async function listCategories({ includeInactive = false } = {}) {
  return prisma.feeCategory.findMany({
    where: includeInactive ? undefined : { isActive: true },
    orderBy: defaultCategoryOrder,
  });
}

async function getCategoryById(id) {
  return prisma.feeCategory.findUnique({ where: { id } });
}

async function createCategory(data) {
  return prisma.feeCategory.create({
    data: {
      name: data.name,
      code: data.code || null,
      defaultAmount: data.defaultAmount
        ? new Prisma.Decimal(data.defaultAmount)
        : null,
      sortOrder: data.sortOrder ?? 0,
      isActive: data.isActive !== false,
    },
  });
}

async function updateCategory(id, data) {
  return prisma.feeCategory.update({
    where: { id },
    data: {
      name: data.name,
      code: data.code || null,
      defaultAmount: data.defaultAmount
        ? new Prisma.Decimal(data.defaultAmount)
        : null,
      sortOrder: data.sortOrder ?? 0,
      isActive:
        typeof data.isActive === "boolean" ? data.isActive : undefined,
    },
  });
}

async function archiveCategory(id) {
  return prisma.feeCategory.update({
    where: { id },
    data: { isActive: false },
  });
}

async function getRecentLedgerEntries(limit = 10) {
  const entries = await prisma.feeLedgerEntry.findMany({
    include: {
      ...ledgerInclude,
      student: {
        include: {
          person: true,
        },
      },
    },
    orderBy: [{ entryDate: "desc" }, { createdAt: "desc" }],
    take: limit,
  });

  return entries.map((entry) => ({
    ...serializeLedgerEntry(entry, null),
    student: entry.student
      ? {
          id: entry.student.id,
          studentNumber: entry.student.studentNumber,
          name: entry.student.person
            ? `${entry.student.person.firstName} ${entry.student.person.lastName}`.trim()
            : null,
        }
      : null,
  }));
}

function applyLedgerFilters(filters = {}) {
  const where = {};
  if (filters.startDate) {
    where.entryDate = { gte: filters.startDate };
  }
  if (filters.endDate) {
    where.entryDate = where.entryDate
      ? { ...where.entryDate, lte: filters.endDate }
      : { lte: filters.endDate };
  }
  if (filters.categoryId) {
    where.categoryId = filters.categoryId;
  }
  return where;
}

async function getStudentLedger(studentId, filters = {}) {
  const student = await prisma.student.findUnique({
    where: { id: studentId },
    include: {
      person: true,
    },
  });

  if (!student) {
    return null;
  }

  const where = { studentId, ...applyLedgerFilters(filters) };

  const entries = await prisma.feeLedgerEntry.findMany({
    where,
    include: ledgerInclude,
    orderBy: [{ entryDate: "asc" }, { createdAt: "asc" }, { id: "asc" }],
  });

  let running = 0;
  let totalCharges = 0;
  let totalPayments = 0;
  let totalAdjustments = 0;

  const ledgerEntries = entries.map((entry) => {
    const signed = signedAmount(entry);
    running += signed;
    if (entry.entryType === "CHARGE") {
      totalCharges += decimalToNumber(entry.amount);
    } else if (entry.entryType === "PAYMENT") {
      totalPayments += decimalToNumber(entry.amount);
    } else {
      totalAdjustments += decimalToNumber(entry.amount);
    }
    return serializeLedgerEntry(entry, running);
  });

  return {
    student: {
      id: student.id,
      studentNumber: student.studentNumber,
      person: student.person,
    },
    entries: ledgerEntries,
    totals: {
      charges: totalCharges,
      payments: totalPayments,
      adjustments: totalAdjustments,
    },
    balance: running,
  };
}

function normalizeLineItem(lineItem) {
  if (!lineItem) {
    return null;
  }
  const amount = lineItem.amount;
  if (amount === undefined || amount === null) {
    return null;
  }
  const normalized = {
    categoryId: lineItem.categoryId ? Number(lineItem.categoryId) : null,
    amount: new Prisma.Decimal(lineItem.amount),
    entryDate: lineItem.entryDate ? new Date(lineItem.entryDate) : new Date(),
    description: lineItem.description ? lineItem.description.trim() : null,
    paymentMethod: lineItem.paymentMethod || null,
  };
  return normalized;
}

async function createCharges(studentId, lineItems, recordedById) {
  const normalizedItems = Array.isArray(lineItems)
    ? lineItems.map(normalizeLineItem).filter(Boolean)
    : [];

  if (normalizedItems.length === 0) {
    return [];
  }

  const created = await prisma.$transaction(async (tx) => {
    const results = [];
    for (const item of normalizedItems) {
      const entry = await tx.feeLedgerEntry.create({
        data: {
          studentId,
          categoryId: item.categoryId,
          recordedById,
          entryType: "CHARGE",
          paymentMethod: null,
          amount: item.amount,
          entryDate: item.entryDate,
          description: item.description,
        },
        include: ledgerInclude,
      });
      results.push(entry);
    }
    return results;
  });

  let running = 0;
  return created.map((entry) => {
    running += signedAmount(entry);
    return serializeLedgerEntry(entry, running);
  });
}

async function createAdjustments(studentId, lineItems, recordedById) {
  const normalizedItems = Array.isArray(lineItems)
    ? lineItems.map(normalizeLineItem).filter(Boolean)
    : [];

  if (normalizedItems.length === 0) {
    return [];
  }

  const created = await prisma.$transaction(async (tx) => {
    const results = [];
    for (const item of normalizedItems) {
      const entry = await tx.feeLedgerEntry.create({
        data: {
          studentId,
          categoryId: item.categoryId,
          recordedById,
          entryType: "ADJUSTMENT",
          paymentMethod: null,
          amount: item.amount,
          entryDate: item.entryDate,
          description: item.description,
        },
        include: ledgerInclude,
      });
      results.push(entry);
    }
    return results;
  });

  let running = 0;
  return created.map((entry) => {
    running += signedAmount(entry);
    return serializeLedgerEntry(entry, running);
  });
}

async function generateReceiptNumber() {
  const timestamp = new Date();
  const base = `RCPT-${timestamp.getUTCFullYear()}${String(
    timestamp.getUTCMonth() + 1
  ).padStart(2, "0")}${String(timestamp.getUTCDate()).padStart(2, "0")}-${timestamp
    .getTime()
    .toString()
    .slice(-6)}`;

  let attempt = 0;
  let candidate = base;
  // eslint-disable-next-line no-constant-condition
  while (true) {
    const existing = await prisma.feeLedgerEntry.findUnique({
      where: { receiptNumber: candidate },
    });
    if (!existing) {
      return candidate;
    }
    attempt += 1;
    candidate = `${base}-${String(attempt).padStart(2, "0")}`;
  }
}

async function createPayments(studentId, lineItems, recordedById, options = {}) {
  const normalizedItems = Array.isArray(lineItems)
    ? lineItems.map(normalizeLineItem).filter(Boolean)
    : [];

  if (normalizedItems.length === 0) {
    return { entries: [], receiptNumber: null };
  }

  const baseReceiptNumber =
    options.receiptNumber || (await generateReceiptNumber());

  const created = await prisma.$transaction(async (tx) => {
    const results = [];
    for (let index = 0; index < normalizedItems.length; index += 1) {
      const item = normalizedItems[index];
      const receiptNumber =
        normalizedItems.length === 1
          ? baseReceiptNumber
          : `${baseReceiptNumber}-${String(index + 1).padStart(2, "0")}`;
      const entry = await tx.feeLedgerEntry.create({
        data: {
          studentId,
          categoryId: item.categoryId,
          recordedById,
          entryType: "PAYMENT",
          paymentMethod: item.paymentMethod || options.paymentMethod || null,
          amount: item.amount,
          entryDate: item.entryDate,
          description: item.description,
          receiptNumber,
        },
        include: ledgerInclude,
      });
      results.push(entry);
    }
    return results;
  });

  let running = 0;
  return {
    receiptNumber: baseReceiptNumber,
    entries: created.map((entry) => {
      running += signedAmount(entry);
      return serializeLedgerEntry(entry, running);
    }),
  };
}

async function getReceipt(receiptNumber) {
  const entries = await prisma.feeLedgerEntry.findMany({
    where: {
      OR: [
        { receiptNumber },
        { receiptNumber: { startsWith: `${receiptNumber}-` } },
      ],
    },
    include: {
      ...ledgerInclude,
      student: {
        include: {
          person: true,
        },
      },
    },
    orderBy: [{ receiptNumber: "asc" }],
  });

  if (!entries.length) {
    return null;
  }

  const student = entries[0].student;
  let total = 0;
  const lineItems = entries.map((entry) => {
    const amount = decimalToNumber(entry.amount);
    total += amount;
    return {
      id: entry.id,
      amount,
      description: entry.description,
      category: entry.category
        ? {
            id: entry.category.id,
            name: entry.category.name,
          }
        : null,
      paymentMethod: entry.paymentMethod,
      entryDate: entry.entryDate,
      receiptNumber: entry.receiptNumber,
    };
  });

  return {
    receiptNumber,
    student: student
      ? {
          id: student.id,
          studentNumber: student.studentNumber,
          name: student.person
            ? `${student.person.firstName} ${student.person.lastName}`.trim()
            : null,
          person: student.person,
        }
      : null,
    total,
    lineItems,
  };
}

module.exports = {
  listCategories,
  getCategoryById,
  createCategory,
  updateCategory,
  archiveCategory,
  getRecentLedgerEntries,
  getStudentLedger,
  createCharges,
  createAdjustments,
  createPayments,
  getReceipt,
  generateReceiptNumber,
};
