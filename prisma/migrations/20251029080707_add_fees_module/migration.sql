-- CreateTable
CREATE TABLE "FeeCategory" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "name" TEXT NOT NULL,
    "code" TEXT,
    "defaultAmount" DECIMAL,
    "sortOrder" INTEGER NOT NULL DEFAULT 0,
    "isActive" BOOLEAN NOT NULL DEFAULT true,
    "createdAt" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" DATETIME NOT NULL
);

-- CreateTable
CREATE TABLE "FeeLedgerEntry" (
    "id" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    "studentId" INTEGER NOT NULL,
    "categoryId" INTEGER,
    "recordedById" INTEGER,
    "entryType" TEXT NOT NULL,
    "paymentMethod" TEXT,
    "amount" DECIMAL NOT NULL,
    "entryDate" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "description" TEXT,
    "receiptNumber" TEXT,
    "createdAt" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "updatedAt" DATETIME NOT NULL,
    CONSTRAINT "FeeLedgerEntry_studentId_fkey" FOREIGN KEY ("studentId") REFERENCES "Student" ("id") ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT "FeeLedgerEntry_categoryId_fkey" FOREIGN KEY ("categoryId") REFERENCES "FeeCategory" ("id") ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT "FeeLedgerEntry_recordedById_fkey" FOREIGN KEY ("recordedById") REFERENCES "User" ("id") ON DELETE SET NULL ON UPDATE CASCADE
);

-- CreateIndex
CREATE UNIQUE INDEX "FeeCategory_code_key" ON "FeeCategory"("code");

-- CreateIndex
CREATE UNIQUE INDEX "FeeCategory_name_key" ON "FeeCategory"("name");

-- CreateIndex
CREATE UNIQUE INDEX "FeeLedgerEntry_receiptNumber_key" ON "FeeLedgerEntry"("receiptNumber");

-- CreateIndex
CREATE INDEX "FeeLedgerEntry_studentId_entryDate_idx" ON "FeeLedgerEntry"("studentId", "entryDate");

-- CreateIndex
CREATE INDEX "FeeLedgerEntry_categoryId_idx" ON "FeeLedgerEntry"("categoryId");

-- CreateIndex
CREATE INDEX "FeeLedgerEntry_recordedById_idx" ON "FeeLedgerEntry"("recordedById");
