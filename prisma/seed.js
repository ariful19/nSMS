const bcrypt = require("bcryptjs");
const { PrismaClient } = require("@prisma/client");

const prisma = new PrismaClient();

function mapByName(records = []) {
  return records.reduce((acc, record) => {
    acc[record.name] = record;
    return acc;
  }, {});
}

async function ensureUserRole(userId, roleId) {
  await prisma.userRole.upsert({
    where: {
      userId_roleId: {
        userId,
        roleId,
      },
    },
    update: {},
    create: {
      userId,
      roleId,
    },
  });
}

async function upsertLookup(model, items) {
  for (const item of items) {
    await model.upsert({
      where: { name: item.name },
      update: item,
      create: item,
    });
  }
}

async function seedLookups() {
  await upsertLookup(prisma.gender, [
    { name: "Female", sortOrder: 1 },
    { name: "Male", sortOrder: 2 },
    { name: "Non-binary", sortOrder: 3 },
  ]);

  await upsertLookup(prisma.studentStatus, [
    { name: "Active", sortOrder: 1 },
    { name: "Inactive", sortOrder: 2 },
    { name: "Graduated", sortOrder: 3 },
  ]);

  await upsertLookup(prisma.gradeLevel, [
    { name: "Grade 9", sortOrder: 9 },
    { name: "Grade 10", sortOrder: 10 },
    { name: "Grade 11", sortOrder: 11 },
    { name: "Grade 12", sortOrder: 12 },
  ]);

  await upsertLookup(prisma.staffType, [
    { name: "Teacher", sortOrder: 1 },
    { name: "Staff", sortOrder: 2 },
  ]);

  await upsertLookup(prisma.employmentStatus, [
    { name: "Active", sortOrder: 1 },
    { name: "On Leave", sortOrder: 2 },
    { name: "Former", sortOrder: 3 },
  ]);

  const [genders, studentStatuses, gradeLevels, staffTypes, employmentStatuses] =
    await Promise.all([
      prisma.gender.findMany(),
      prisma.studentStatus.findMany(),
      prisma.gradeLevel.findMany(),
      prisma.staffType.findMany(),
      prisma.employmentStatus.findMany(),
    ]);

  return {
    genders: mapByName(genders),
    studentStatuses: mapByName(studentStatuses),
    gradeLevels: mapByName(gradeLevels),
    staffTypes: mapByName(staffTypes),
    employmentStatuses: mapByName(employmentStatuses),
  };
}

async function seedRolesAndUsers(saltRounds) {
  const adminPassword = process.env.SEED_ADMIN_PASSWORD || "ChangeMe123!";
  const teacherPassword = process.env.SEED_TEACHER_PASSWORD || "TeacherPass123!";
  const studentPassword = process.env.SEED_STUDENT_PASSWORD || "StudentPass123!";
  const staffPassword = process.env.SEED_STAFF_PASSWORD || "StaffPass123!";

  const roleDefinitions = [
    { name: "Admin", description: "Full administrative access" },
    { name: "Teacher", description: "Classroom educator access" },
    { name: "Staff", description: "General staff permissions" },
    { name: "Student", description: "Student self-service portal" },
  ];

  const roles = await Promise.all(
    roleDefinitions.map((role) =>
      prisma.role.upsert({
        where: { name: role.name },
        update: {},
        create: role,
      })
    )
  );

  const roleByName = mapByName(roles);

  const admin = await prisma.user.upsert({
    where: { email: "admin@example.com" },
    update: {
      username: "admin",
      passwordHash: await bcrypt.hash(adminPassword, saltRounds),
      isActive: true,
    },
    create: {
      email: "admin@example.com",
      username: "admin",
      passwordHash: await bcrypt.hash(adminPassword, saltRounds),
      isActive: true,
      roles: {
        create: {
          role: { connect: { id: roleByName.Admin.id } },
        },
      },
    },
  });
  await ensureUserRole(admin.id, roleByName.Admin.id);

  const teacher = await prisma.user.upsert({
    where: { email: "teacher@example.com" },
    update: {
      username: "teacher",
      passwordHash: await bcrypt.hash(teacherPassword, saltRounds),
      isActive: true,
    },
    create: {
      email: "teacher@example.com",
      username: "teacher",
      passwordHash: await bcrypt.hash(teacherPassword, saltRounds),
      isActive: true,
      roles: {
        create: {
          role: { connect: { id: roleByName.Teacher.id } },
        },
      },
    },
  });
  await ensureUserRole(teacher.id, roleByName.Teacher.id);

  const staff = await prisma.user.upsert({
    where: { email: "staff@example.com" },
    update: {
      username: "staff",
      passwordHash: await bcrypt.hash(staffPassword, saltRounds),
      isActive: true,
    },
    create: {
      email: "staff@example.com",
      username: "staff",
      passwordHash: await bcrypt.hash(staffPassword, saltRounds),
      isActive: true,
      roles: {
        create: {
          role: { connect: { id: roleByName.Staff.id } },
        },
      },
    },
  });
  await ensureUserRole(staff.id, roleByName.Staff.id);

  const student = await prisma.user.upsert({
    where: { email: "student@example.com" },
    update: {
      username: "student",
      passwordHash: await bcrypt.hash(studentPassword, saltRounds),
      isActive: true,
    },
    create: {
      email: "student@example.com",
      username: "student",
      passwordHash: await bcrypt.hash(studentPassword, saltRounds),
      isActive: true,
      roles: {
        create: {
          role: { connect: { id: roleByName.Student.id } },
        },
      },
    },
  });
  await ensureUserRole(student.id, roleByName.Student.id);

  return {
    admin,
    teacher,
    staff,
    student,
    roleByName,
  };
}

async function seedProfiles({
  genders,
  studentStatuses,
  gradeLevels,
  staffTypes,
  employmentStatuses,
}, users) {
  const studentPerson = await prisma.person.upsert({
    where: { externalId: "PER-0001" },
    update: {
      firstName: "Sara",
      lastName: "Nguyen",
      gender: genders.Female ? { connect: { id: genders.Female.id } } : undefined,
      primaryEmail: "student@example.com",
      mobilePhone: "+1-555-0101",
      addressLine1: "123 Academy Way",
      city: "Queens",
      state: "NY",
      postalCode: "11101",
      country: "USA",
    },
    create: {
      externalId: "PER-0001",
      firstName: "Sara",
      lastName: "Nguyen",
      dateOfBirth: new Date("2010-04-12T00:00:00Z"),
      gender: genders.Female ? { connect: { id: genders.Female.id } } : undefined,
      primaryEmail: "student@example.com",
      mobilePhone: "+1-555-0101",
      addressLine1: "123 Academy Way",
      city: "Queens",
      state: "NY",
      postalCode: "11101",
      country: "USA",
    },
  });

  await prisma.student.upsert({
    where: { personId: studentPerson.id },
    update: {
      user: { connect: { id: users.student.id } },
      enrollmentStatus: { connect: { id: studentStatuses.Active.id } },
      gradeLevel: gradeLevels["Grade 9"]
        ? { connect: { id: gradeLevels["Grade 9"].id } }
        : undefined,
      studentNumber: "STU-0001",
      admissionDate: new Date("2024-09-02T00:00:00Z"),
      notes: "Sample student record for development seeds.",
    },
    create: {
      person: { connect: { id: studentPerson.id } },
      user: { connect: { id: users.student.id } },
      enrollmentStatus: { connect: { id: studentStatuses.Active.id } },
      gradeLevel: gradeLevels["Grade 9"]
        ? { connect: { id: gradeLevels["Grade 9"].id } }
        : undefined,
      studentNumber: "STU-0001",
      admissionDate: new Date("2024-09-02T00:00:00Z"),
      notes: "Sample student record for development seeds.",
    },
  });

  const teacherPerson = await prisma.person.upsert({
    where: { externalId: "PER-1001" },
    update: {
      firstName: "Daniel",
      lastName: "Okafor",
      gender: genders.Male ? { connect: { id: genders.Male.id } } : undefined,
      primaryEmail: "teacher@example.com",
      mobilePhone: "+1-555-0125",
      addressLine1: "44 Faculty Plaza",
      city: "Queens",
      state: "NY",
      postalCode: "11368",
      country: "USA",
    },
    create: {
      externalId: "PER-1001",
      firstName: "Daniel",
      lastName: "Okafor",
      dateOfBirth: new Date("1986-02-18T00:00:00Z"),
      gender: genders.Male ? { connect: { id: genders.Male.id } } : undefined,
      primaryEmail: "teacher@example.com",
      mobilePhone: "+1-555-0125",
      addressLine1: "44 Faculty Plaza",
      city: "Queens",
      state: "NY",
      postalCode: "11368",
      country: "USA",
    },
  });

  await prisma.teacherStaff.upsert({
    where: { personId: teacherPerson.id },
    update: {
      user: { connect: { id: users.teacher.id } },
      staffType: { connect: { id: staffTypes.Teacher.id } },
      employmentStatus: { connect: { id: employmentStatuses.Active.id } },
      employeeNumber: "EMP-0456",
      hireDate: new Date("2015-08-15T00:00:00Z"),
      primarySubject: "Mathematics",
      notes: "Lead math teacher used for sample data.",
    },
    create: {
      person: { connect: { id: teacherPerson.id } },
      user: { connect: { id: users.teacher.id } },
      staffType: { connect: { id: staffTypes.Teacher.id } },
      employmentStatus: { connect: { id: employmentStatuses.Active.id } },
      employeeNumber: "EMP-0456",
      hireDate: new Date("2015-08-15T00:00:00Z"),
      primarySubject: "Mathematics",
      notes: "Lead math teacher used for sample data.",
    },
  });
}

async function main() {
  const saltRounds = Number(process.env.BCRYPT_COST || 10);

  const lookupData = await seedLookups();
  const users = await seedRolesAndUsers(saltRounds);
  await seedProfiles(lookupData, users);
}

main()
  .then(async () => {
    console.log("Database seeded with default lookups, users, and sample records.");
    await prisma.$disconnect();
  })
  .catch(async (error) => {
    console.error("Failed to seed database", error);
    await prisma.$disconnect();
    process.exit(1);
  });
