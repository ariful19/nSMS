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

const genderSeeds = [
  { name: "Female", sortOrder: 1 },
  { name: "Male", sortOrder: 2 },
  { name: "Non-binary", sortOrder: 3 },
  { name: "Prefer not to say", sortOrder: 4 },
];

const studentStatusSeeds = [
  { name: "Active", sortOrder: 1, description: "Enrolled and attending classes." },
  { name: "Inactive", sortOrder: 2, description: "Temporarily inactive but expected to return." },
  { name: "Graduated", sortOrder: 3, description: "Completed their program of study." },
  { name: "Prospective", sortOrder: 4, description: "Accepted but not yet started." },
];

const gradeLevelSeeds = [
  { name: "Grade 6", sortOrder: 6, description: "Middle school" },
  { name: "Grade 7", sortOrder: 7, description: "Middle school" },
  { name: "Grade 8", sortOrder: 8, description: "Middle school" },
  { name: "Grade 9", sortOrder: 9, description: "Freshman" },
  { name: "Grade 10", sortOrder: 10, description: "Sophomore" },
  { name: "Grade 11", sortOrder: 11, description: "Junior" },
  { name: "Grade 12", sortOrder: 12, description: "Senior" },
];

const staffTypeSeeds = [
  { name: "Teacher", sortOrder: 1, description: "Classroom instructors" },
  { name: "Counselor", sortOrder: 2, description: "Student support staff" },
  { name: "Administrator", sortOrder: 3, description: "School administration" },
  { name: "Support Staff", sortOrder: 4, description: "Non-instructional staff" },
];

const employmentStatusSeeds = [
  { name: "Active", sortOrder: 1, description: "Currently employed" },
  { name: "On Leave", sortOrder: 2, description: "Temporarily away" },
  { name: "Former", sortOrder: 3, description: "No longer with the school" },
  { name: "Contractor", sortOrder: 4, description: "Contract-based staff" },
];

function parseDate(value) {
  if (!value) {
    return null;
  }

  const date = new Date(`${value}T00:00:00Z`);
  return Number.isNaN(date.getTime()) ? null : date;
}

async function seedLookups() {
  await upsertLookup(prisma.gender, genderSeeds);
  await upsertLookup(prisma.studentStatus, studentStatusSeeds);
  await upsertLookup(prisma.gradeLevel, gradeLevelSeeds);
  await upsertLookup(prisma.staffType, staffTypeSeeds);
  await upsertLookup(prisma.employmentStatus, employmentStatusSeeds);

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

const studentSamples = [
  {
    person: {
      externalId: "PER-0001",
      firstName: "Sara",
      lastName: "Nguyen",
      gender: "Female",
      dateOfBirth: "2010-04-12",
      primaryEmail: "student@example.com",
      mobilePhone: "+1-555-0101",
      addressLine1: "123 Academy Way",
      city: "Queens",
      state: "NY",
      postalCode: "11101",
      country: "USA",
    },
    student: {
      studentNumber: "STU-0001",
      enrollmentStatus: "Active",
      gradeLevel: "Grade 9",
      admissionDate: "2024-09-02",
      notes: "Sample student record for development seeds.",
    },
    userKey: "student",
  },
  {
    person: {
      externalId: "PER-0002",
      firstName: "Luis",
      lastName: "Martinez",
      gender: "Male",
      dateOfBirth: "2009-11-30",
      primaryEmail: "luis.martinez@example.com",
      mobilePhone: "+1-555-0107",
      addressLine1: "77 Cypress Ave",
      city: "Queens",
      state: "NY",
      postalCode: "11385",
      country: "USA",
    },
    student: {
      studentNumber: "STU-0002",
      enrollmentStatus: "Active",
      gradeLevel: "Grade 11",
      admissionDate: "2022-09-06",
      notes: "Varsity soccer co-captain.",
    },
  },
  {
    person: {
      externalId: "PER-0003",
      firstName: "Mira",
      lastName: "Patel",
      gender: "Female",
      dateOfBirth: "2008-06-21",
      primaryEmail: "mira.patel@example.com",
      mobilePhone: "+1-555-0109",
      addressLine1: "905 Skillman Ave",
      city: "Queens",
      state: "NY",
      postalCode: "11101",
      country: "USA",
    },
    student: {
      studentNumber: "STU-0003",
      enrollmentStatus: "Graduated",
      gradeLevel: "Grade 12",
      admissionDate: "2020-09-08",
      graduationDate: "2024-06-15",
      notes: "Recent alum used for regression testing.",
    },
  },
];

const teacherSamples = [
  {
    person: {
      externalId: "PER-1001",
      firstName: "Daniel",
      lastName: "Okafor",
      gender: "Male",
      dateOfBirth: "1986-02-18",
      primaryEmail: "teacher@example.com",
      mobilePhone: "+1-555-0125",
      addressLine1: "44 Faculty Plaza",
      city: "Queens",
      state: "NY",
      postalCode: "11368",
      country: "USA",
    },
    teacher: {
      employeeNumber: "EMP-0456",
      staffType: "Teacher",
      employmentStatus: "Active",
      hireDate: "2015-08-15",
      primarySubject: "Mathematics",
      notes: "Lead math teacher used for sample data.",
    },
    userKey: "teacher",
  },
  {
    person: {
      externalId: "PER-1002",
      firstName: "Amina",
      lastName: "Chowdhury",
      gender: "Female",
      dateOfBirth: "1989-07-02",
      primaryEmail: "staff@example.com",
      mobilePhone: "+1-555-0128",
      addressLine1: "12 Community Way",
      city: "Queens",
      state: "NY",
      postalCode: "11109",
      country: "USA",
    },
    teacher: {
      employeeNumber: "EMP-0721",
      staffType: "Counselor",
      employmentStatus: "Active",
      hireDate: "2018-01-03",
      primarySubject: "Student Support",
      notes: "Guidance counselor covering grades 9-12.",
    },
    userKey: "staff",
  },
  {
    person: {
      externalId: "PER-1003",
      firstName: "Noah",
      lastName: "Garcia",
      gender: "Non-binary",
      dateOfBirth: "1992-10-11",
      primaryEmail: "noah.garcia@example.com",
      mobilePhone: "+1-555-0134",
      addressLine1: "500 Shore Front Pkwy",
      city: "Queens",
      state: "NY",
      postalCode: "11694",
      country: "USA",
    },
    teacher: {
      employeeNumber: "EMP-0833",
      staffType: "Teacher",
      employmentStatus: "On Leave",
      hireDate: "2019-09-01",
      contractEndDate: "2025-06-30",
      primarySubject: "Earth Science",
      notes: "On sabbatical during spring term.",
    },
  },
];

function resolveLookup(lookupMap, value, type) {
  if (!value) {
    return null;
  }

  const record = lookupMap[value];
  if (!record) {
    throw new Error(`Missing ${type} lookup for value: ${value}`);
  }
  return record;
}

async function seedStudents(lookupData, users) {
  for (const sample of studentSamples) {
    const gender = resolveLookup(lookupData.genders, sample.person.gender, "gender");
    const status = resolveLookup(
      lookupData.studentStatuses,
      sample.student.enrollmentStatus,
      "student status"
    );
    const gradeLevel = sample.student.gradeLevel
      ? resolveLookup(lookupData.gradeLevels, sample.student.gradeLevel, "grade level")
      : null;

    const person = await prisma.person.upsert({
      where: { externalId: sample.person.externalId },
      update: {
        firstName: sample.person.firstName,
        lastName: sample.person.lastName,
        middleName: sample.person.middleName || null,
        preferredName: sample.person.preferredName || null,
        dateOfBirth: parseDate(sample.person.dateOfBirth),
        gender: gender ? { connect: { id: gender.id } } : undefined,
        primaryEmail: sample.person.primaryEmail,
        mobilePhone: sample.person.mobilePhone,
        addressLine1: sample.person.addressLine1,
        addressLine2: sample.person.addressLine2 || null,
        city: sample.person.city,
        state: sample.person.state,
        postalCode: sample.person.postalCode,
        country: sample.person.country,
      },
      create: {
        externalId: sample.person.externalId,
        firstName: sample.person.firstName,
        lastName: sample.person.lastName,
        middleName: sample.person.middleName || null,
        preferredName: sample.person.preferredName || null,
        dateOfBirth: parseDate(sample.person.dateOfBirth),
        gender: gender ? { connect: { id: gender.id } } : undefined,
        primaryEmail: sample.person.primaryEmail,
        mobilePhone: sample.person.mobilePhone,
        addressLine1: sample.person.addressLine1,
        addressLine2: sample.person.addressLine2 || null,
        city: sample.person.city,
        state: sample.person.state,
        postalCode: sample.person.postalCode,
        country: sample.person.country,
      },
    });

    const createData = {
      person: { connect: { id: person.id } },
      enrollmentStatus: { connect: { id: status.id } },
      studentNumber: sample.student.studentNumber,
      admissionDate: parseDate(sample.student.admissionDate),
      graduationDate: parseDate(sample.student.graduationDate),
      notes: sample.student.notes || null,
    };

    const updateData = {
      enrollmentStatus: { connect: { id: status.id } },
      studentNumber: sample.student.studentNumber,
      admissionDate: parseDate(sample.student.admissionDate),
      graduationDate: parseDate(sample.student.graduationDate),
      notes: sample.student.notes || null,
    };

    if (gradeLevel) {
      createData.gradeLevel = { connect: { id: gradeLevel.id } };
      updateData.gradeLevel = { connect: { id: gradeLevel.id } };
    } else {
      updateData.gradeLevel = { disconnect: true };
    }

    if (sample.userKey && users[sample.userKey]) {
      createData.user = { connect: { id: users[sample.userKey].id } };
      updateData.user = { connect: { id: users[sample.userKey].id } };
    } else {
      updateData.user = { disconnect: true };
    }

    await prisma.student.upsert({
      where: { personId: person.id },
      create: createData,
      update: updateData,
    });
  }
}

async function seedTeachers(lookupData, users) {
  for (const sample of teacherSamples) {
    const gender = resolveLookup(lookupData.genders, sample.person.gender, "gender");
    const staffType = resolveLookup(
      lookupData.staffTypes,
      sample.teacher.staffType,
      "staff type"
    );
    const employmentStatus = resolveLookup(
      lookupData.employmentStatuses,
      sample.teacher.employmentStatus,
      "employment status"
    );

    const person = await prisma.person.upsert({
      where: { externalId: sample.person.externalId },
      update: {
        firstName: sample.person.firstName,
        lastName: sample.person.lastName,
        middleName: sample.person.middleName || null,
        preferredName: sample.person.preferredName || null,
        dateOfBirth: parseDate(sample.person.dateOfBirth),
        gender: gender ? { connect: { id: gender.id } } : undefined,
        primaryEmail: sample.person.primaryEmail,
        mobilePhone: sample.person.mobilePhone,
        addressLine1: sample.person.addressLine1,
        addressLine2: sample.person.addressLine2 || null,
        city: sample.person.city,
        state: sample.person.state,
        postalCode: sample.person.postalCode,
        country: sample.person.country,
      },
      create: {
        externalId: sample.person.externalId,
        firstName: sample.person.firstName,
        lastName: sample.person.lastName,
        middleName: sample.person.middleName || null,
        preferredName: sample.person.preferredName || null,
        dateOfBirth: parseDate(sample.person.dateOfBirth),
        gender: gender ? { connect: { id: gender.id } } : undefined,
        primaryEmail: sample.person.primaryEmail,
        mobilePhone: sample.person.mobilePhone,
        addressLine1: sample.person.addressLine1,
        addressLine2: sample.person.addressLine2 || null,
        city: sample.person.city,
        state: sample.person.state,
        postalCode: sample.person.postalCode,
        country: sample.person.country,
      },
    });

    const createData = {
      person: { connect: { id: person.id } },
      staffType: { connect: { id: staffType.id } },
      employmentStatus: { connect: { id: employmentStatus.id } },
      employeeNumber: sample.teacher.employeeNumber,
      hireDate: parseDate(sample.teacher.hireDate),
      contractEndDate: parseDate(sample.teacher.contractEndDate),
      primarySubject: sample.teacher.primarySubject || null,
      notes: sample.teacher.notes || null,
    };

    const updateData = {
      staffType: { connect: { id: staffType.id } },
      employmentStatus: { connect: { id: employmentStatus.id } },
      employeeNumber: sample.teacher.employeeNumber,
      hireDate: parseDate(sample.teacher.hireDate),
      contractEndDate: parseDate(sample.teacher.contractEndDate),
      primarySubject: sample.teacher.primarySubject || null,
      notes: sample.teacher.notes || null,
    };

    if (sample.userKey && users[sample.userKey]) {
      createData.user = { connect: { id: users[sample.userKey].id } };
      updateData.user = { connect: { id: users[sample.userKey].id } };
    } else {
      updateData.user = { disconnect: true };
    }

    await prisma.teacherStaff.upsert({
      where: { personId: person.id },
      create: createData,
      update: updateData,
    });
  }
}

async function main() {
  const saltRounds = Number(process.env.BCRYPT_COST || 10);

  const lookupData = await seedLookups();
  const users = await seedRolesAndUsers(saltRounds);
  await seedStudents(lookupData, users);
  await seedTeachers(lookupData, users);
}

main()
  .then(async () => {
    console.log(
      "Database seeded with default lookups, demo users, and representative student/teacher records."
    );
    await prisma.$disconnect();
  })
  .catch(async (error) => {
    console.error("Failed to seed database", error);
    await prisma.$disconnect();
    process.exit(1);
  });
