const bcrypt = require("bcryptjs");
const { PrismaClient } = require("@prisma/client");

const prisma = new PrismaClient();

async function main() {
  const saltRounds = Number(process.env.BCRYPT_COST || 10);
  const adminPassword = process.env.SEED_ADMIN_PASSWORD || "ChangeMe123!";
  const adminEmail = "admin@example.com";
  const adminUsername = "admin";

  const roles = await Promise.all(
    [
      { name: "Admin", description: "Full administrative access" },
      { name: "Teacher", description: "Classroom educator access" },
      { name: "Staff", description: "General staff permissions" },
    ].map(async (role) =>
      prisma.role.upsert({
        where: { name: role.name },
        update: {},
        create: role,
      })
    )
  );

  const adminRole = roles.find((role) => role.name === "Admin");

  const passwordHash = await bcrypt.hash(adminPassword, saltRounds);

  const admin = await prisma.user.upsert({
    where: { email: adminEmail },
    update: {
      username: adminUsername,
      passwordHash,
      isActive: true,
    },
    create: {
      email: adminEmail,
      username: adminUsername,
      passwordHash,
      isActive: true,
      roles: {
        create: {
          role: {
            connect: { id: adminRole.id },
          },
        },
      },
    },
    include: {
      roles: true,
    },
  });

  const existingAdminRole = await prisma.userRole.findUnique({
    where: {
      userId_roleId: {
        userId: admin.id,
        roleId: adminRole.id,
      },
    },
  });

  if (!existingAdminRole) {
    await prisma.userRole.create({
      data: {
        userId: admin.id,
        roleId: adminRole.id,
      },
    });
  }

  // Optional placeholder staff user for testing future phases
  const staffEmail = "staff@example.com";
  const staffRole = roles.find((role) => role.name === "Staff");
  const staffPasswordHash = await bcrypt.hash("StaffPass123!", saltRounds);

  await prisma.user.upsert({
    where: { email: staffEmail },
    update: {
      passwordHash: staffPasswordHash,
      isActive: true,
    },
    create: {
      email: staffEmail,
      username: "staff",
      passwordHash: staffPasswordHash,
      isActive: true,
      roles: {
        create: {
          role: {
            connect: { id: staffRole.id },
          },
        },
      },
    },
  });
}

main()
  .then(async () => {
    console.log("Database seeded with default roles and users.");
    await prisma.$disconnect();
  })
  .catch(async (error) => {
    console.error("Failed to seed database", error);
    await prisma.$disconnect();
    process.exit(1);
  });
