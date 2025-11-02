jest.mock("../src/db/client", () => ({
  prisma: {
    feeCategory: {
      findMany: jest.fn(),
      findUnique: jest.fn(),
    },
    feeLedgerEntry: {
      findMany: jest.fn(),
      create: jest.fn(),
    },
    student: {
      findUnique: jest.fn(),
    },
  },
}));

const { prisma } = require("../src/db/client");
const feeService = require("../src/services/feeService");

describe("Fee Service", () => {
  afterEach(() => {
    jest.clearAllMocks();
  });

  describe("listCategories", () => {
    test("retrieves all active fee categories", async () => {
      const mockCategories = [
        { id: 1, name: "Tuition", code: "TUI", sortOrder: 0, isActive: true },
        { id: 2, name: "Library", code: "LIB", sortOrder: 1, isActive: true },
      ];

      prisma.feeCategory.findMany.mockResolvedValue(mockCategories);

      const result = await feeService.listCategories();

      expect(result).toEqual(mockCategories);
      expect(prisma.feeCategory.findMany).toHaveBeenCalledWith(
        expect.objectContaining({
          where: { isActive: true },
        })
      );
    });

    test("returns empty array when no categories exist", async () => {
      prisma.feeCategory.findMany.mockResolvedValue([]);

      const result = await feeService.listCategories();

      expect(result).toEqual([]);
    });
  });

  describe("getCategoryById", () => {
    test("retrieves category by ID", async () => {
      const mockCategory = {
        id: 1,
        name: "Tuition",
        code: "TUI",
        defaultAmount: "5000.00",
        isActive: true,
      };

      prisma.feeCategory.findUnique.mockResolvedValue(mockCategory);

      const result = await feeService.getCategoryById(1);

      expect(result).toEqual(mockCategory);
      expect(prisma.feeCategory.findUnique).toHaveBeenCalledWith({
        where: { id: 1 },
      });
    });

    test("returns null when category not found", async () => {
      prisma.feeCategory.findUnique.mockResolvedValue(null);

      const result = await feeService.getCategoryById(999);

      expect(result).toBeNull();
    });
  });

  describe("getStudentLedger", () => {
    test("retrieves ledger entries for student", async () => {
      const mockStudent = {
        id: 1,
        studentNumber: "STU-001",
        person: {
          firstName: "John",
          lastName: "Doe",
        },
      };

      const mockEntries = [
        {
          id: 1,
          studentId: 1,
          categoryId: 1,
          entryType: "CHARGE",
          amount: "1000.00",
          entryDate: new Date("2024-01-01"),
          category: { id: 1, name: "Tuition", code: "TUI" },
        },
        {
          id: 2,
          studentId: 1,
          categoryId: null,
          entryType: "PAYMENT",
          amount: "500.00",
          paymentMethod: "CASH",
          entryDate: new Date("2024-01-15"),
          category: null,
        },
      ];

      prisma.student.findUnique.mockResolvedValue(mockStudent);
      prisma.feeLedgerEntry.findMany.mockResolvedValue(mockEntries);

      const result = await feeService.getStudentLedger(1);

      expect(result.student).toBeDefined();
      expect(result.entries).toHaveLength(2);
      expect(result.balance).toBe(500); // 1000 - 500
      expect(prisma.student.findUnique).toHaveBeenCalledWith(
        expect.objectContaining({
          where: { id: 1 },
        })
      );
    });

    test("handles student with no entries", async () => {
      const mockStudent = {
        id: 1,
        studentNumber: "STU-001",
        person: {
          firstName: "John",
          lastName: "Doe",
        },
      };

      prisma.student.findUnique.mockResolvedValue(mockStudent);
      prisma.feeLedgerEntry.findMany.mockResolvedValue([]);

      const result = await feeService.getStudentLedger(1);

      expect(result.entries).toEqual([]);
      expect(result.balance).toBe(0);
    });

    test("returns null when student not found", async () => {
      prisma.student.findUnique.mockResolvedValue(null);

      const result = await feeService.getStudentLedger(999);

      expect(result).toBeNull();
    });
  });

  describe("createCharges", () => {
    test("accepts valid charge payload", async () => {
      const mockTransaction = jest.fn().mockResolvedValue([
        {
          id: 1,
          studentId: 1,
          categoryId: 1,
          entryType: "CHARGE",
          amount: "1000.00",
        },
      ]);

      prisma.$transaction = mockTransaction;

      const result = await feeService.createCharges(
        1, // studentId
        [
          {
            categoryId: 1,
            amount: 1000,
            description: "Tuition fee",
          },
        ],
        1 // recordedById
      );

      expect(result).toBeDefined();
      expect(mockTransaction).toHaveBeenCalled();
    });
  });
});
