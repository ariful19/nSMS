const { prisma } = require("../db/client");

const defaultBookInclude = {
  category: true,
  copies: {
    orderBy: [{ barcode: "asc" }, { id: "asc" }],
  },
};

function parseIntOrNull(value) {
  if (value === undefined || value === null || value === "") {
    return null;
  }
  const parsed = Number.parseInt(value, 10);
  return Number.isNaN(parsed) ? null : parsed;
}

function toDate(value) {
  if (!value) {
    return null;
  }
  const date = value instanceof Date ? value : new Date(value);
  return Number.isNaN(date.getTime()) ? null : date;
}

function normalizeTerm(term) {
  if (!term) {
    return "";
  }
  return term.trim();
}

function summarizeCopies(copies = []) {
  const summary = {
    total: copies.length,
    available: 0,
    loaned: 0,
    maintenance: 0,
    lost: 0,
  };

  for (const copy of copies) {
    if (copy.status === "AVAILABLE") {
      summary.available += 1;
    } else if (copy.status === "LOANED") {
      summary.loaned += 1;
    } else if (copy.status === "MAINTENANCE") {
      summary.maintenance += 1;
    } else if (copy.status === "LOST") {
      summary.lost += 1;
    }
  }

  return summary;
}

function serializeCategory(category) {
  if (!category) {
    return null;
  }
  return {
    id: category.id,
    name: category.name,
    code: category.code,
  };
}

function serializeBook(book) {
  const summary = summarizeCopies(book.copies || []);
  return {
    id: book.id,
    title: book.title,
    isbn: book.isbn,
    author: book.author,
    publisher: book.publisher,
    publishedYear: book.publishedYear,
    summary: book.summary,
    isArchived: book.isArchived,
    category: serializeCategory(book.category),
    copies: book.copies || [],
    copySummary: summary,
  };
}

function formatStudent(student) {
  if (!student) {
    return null;
  }
  const nameParts = [];
  if (student.person) {
    if (student.person.firstName) {
      nameParts.push(student.person.firstName);
    }
    if (student.person.lastName) {
      nameParts.push(student.person.lastName);
    }
  }
  return {
    id: student.id,
    studentNumber: student.studentNumber,
    name: nameParts.join(" ").trim() || student.studentNumber,
  };
}

function serializeLoan(loan) {
  return {
    id: loan.id,
    copyId: loan.copyId,
    studentId: loan.studentId,
    issuedById: loan.issuedById,
    receivedById: loan.receivedById,
    issuedAt: loan.issuedAt,
    dueAt: loan.dueAt,
    returnedAt: loan.returnedAt,
    notes: loan.notes,
    copy: loan.copy
      ? {
          id: loan.copy.id,
          barcode: loan.copy.barcode,
          bookId: loan.copy.bookId,
        }
      : null,
    book: loan.copy && loan.copy.book
      ? {
          id: loan.copy.book.id,
          title: loan.copy.book.title,
        }
      : null,
    student: loan.student ? formatStudent(loan.student) : null,
  };
}

async function listCategories({ includeInactive = false } = {}) {
  return prisma.libraryCategory.findMany({
    where: includeInactive ? undefined : { isActive: true },
    orderBy: [{ sortOrder: "asc" }, { name: "asc" }],
  });
}

async function searchBooks(term, { limit = 20, categoryId, includeArchived = false } = {}) {
  const normalizedTerm = normalizeTerm(term);
  const where = {
    isArchived: includeArchived ? undefined : false,
  };

  if (normalizedTerm) {
    where.OR = [
      { title: { contains: normalizedTerm, mode: "insensitive" } },
      { author: { contains: normalizedTerm, mode: "insensitive" } },
      { isbn: { contains: normalizedTerm, mode: "insensitive" } },
    ];
  }

  if (categoryId) {
    where.categoryId = categoryId;
  }

  const books = await prisma.libraryBook.findMany({
    where,
    include: {
      category: true,
      copies: {
        select: {
          id: true,
          status: true,
        },
      },
    },
    orderBy: [{ title: "asc" }, { id: "asc" }],
    take: limit,
  });

  return books.map((book) => {
    const summary = summarizeCopies(book.copies);
    return {
      id: book.id,
      title: book.title,
      author: book.author,
      isbn: book.isbn,
      category: serializeCategory(book.category),
      copySummary: summary,
    };
  });
}

async function getBookById(bookId) {
  const [book, activeLoans, recentLoans] = await Promise.all([
    prisma.libraryBook.findUnique({
      where: { id: bookId },
      include: defaultBookInclude,
    }),
    prisma.libraryLoan.findMany({
      where: { copy: { bookId }, returnedAt: null },
      include: {
        copy: {
          select: {
            id: true,
            barcode: true,
          },
        },
        student: {
          include: {
            person: true,
          },
        },
      },
      orderBy: [{ issuedAt: "desc" }, { id: "desc" }],
    }),
    prisma.libraryLoan.findMany({
      where: { copy: { bookId } },
      include: {
        copy: {
          select: {
            id: true,
            barcode: true,
            book: {
              select: {
                id: true,
                title: true,
              },
            },
          },
        },
        student: {
          include: {
            person: true,
          },
        },
      },
      orderBy: [{ issuedAt: "desc" }, { id: "desc" }],
      take: 15,
    }),
  ]);

  if (!book) {
    return null;
  }

  return {
    book: serializeBook(book),
    activeLoans: activeLoans.map((loan) => ({
      ...serializeLoan(loan),
      copy: loan.copy
        ? {
            id: loan.copy.id,
            barcode: loan.copy.barcode,
          }
        : null,
    })),
    recentLoans: recentLoans.map((loan) => serializeLoan(loan)),
  };
}

async function createBook(data) {
  return prisma.libraryBook.create({
    data: {
      title: data.title,
      isbn: data.isbn || null,
      author: data.author || null,
      publisher: data.publisher || null,
      publishedYear: parseIntOrNull(data.publishedYear),
      summary: data.summary || null,
      categoryId: parseIntOrNull(data.categoryId),
      isArchived: data.isArchived === true,
    },
  });
}

async function updateBook(bookId, data) {
  return prisma.libraryBook.update({
    where: { id: bookId },
    data: {
      title: data.title,
      isbn: data.isbn || null,
      author: data.author || null,
      publisher: data.publisher || null,
      publishedYear: parseIntOrNull(data.publishedYear),
      summary: data.summary || null,
      categoryId: parseIntOrNull(data.categoryId),
      isArchived: data.isArchived === true,
    },
  });
}

async function createCopy(bookId, data) {
  return prisma.libraryCopy.create({
    data: {
      bookId,
      barcode: data.barcode || null,
      status: data.status || "AVAILABLE",
      acquiredAt: toDate(data.acquiredAt),
      location: data.location || null,
      isArchived: data.isArchived === true,
    },
  });
}

async function listCopiesForBook(bookId) {
  const copies = await prisma.libraryCopy.findMany({
    where: { bookId },
    orderBy: [{ barcode: "asc" }, { id: "asc" }],
  });
  return copies;
}

async function getRecentLoans(limit = 10) {
  const loans = await prisma.libraryLoan.findMany({
    include: {
      copy: {
        include: {
          book: {
            select: {
              id: true,
              title: true,
            },
          },
        },
      },
      student: {
        include: {
          person: true,
        },
      },
    },
    orderBy: [{ issuedAt: "desc" }, { id: "desc" }],
    take: limit,
  });

  return loans.map((loan) => serializeLoan(loan));
}

async function issueLoan(copyId, studentId, { issuedById, dueAt, issuedAt, notes } = {}) {
  return prisma.$transaction(async (tx) => {
    const copy = await tx.libraryCopy.findUnique({ where: { id: copyId } });
    if (!copy) {
      throw new Error("Copy not found.");
    }
    if (copy.status !== "AVAILABLE") {
      throw new Error("Copy is not available for loan.");
    }

    const createdLoan = await tx.libraryLoan.create({
      data: {
        copyId,
        studentId,
        issuedById: issuedById || null,
        issuedAt: toDate(issuedAt) || new Date(),
        dueAt: toDate(dueAt),
        notes: notes || null,
      },
    });

    await tx.libraryCopy.update({
      where: { id: copyId },
      data: { status: "LOANED" },
    });

    return createdLoan;
  });
}

async function returnLoan(loanId, { receivedById, returnedAt } = {}) {
  return prisma.$transaction(async (tx) => {
    const loan = await tx.libraryLoan.findUnique({ where: { id: loanId } });
    if (!loan) {
      throw new Error("Loan not found.");
    }
    if (loan.returnedAt) {
      return loan;
    }

    const updatedLoan = await tx.libraryLoan.update({
      where: { id: loanId },
      data: {
        receivedById: receivedById || null,
        returnedAt: toDate(returnedAt) || new Date(),
      },
    });

    await tx.libraryCopy.update({
      where: { id: loan.copyId },
      data: { status: "AVAILABLE" },
    });

    return updatedLoan;
  });
}

module.exports = {
  listCategories,
  searchBooks,
  getBookById,
  createBook,
  updateBook,
  createCopy,
  listCopiesForBook,
  getRecentLoans,
  issueLoan,
  returnLoan,
};
