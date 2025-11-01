const bcrypt = require("bcryptjs");
const { PrismaClient, Prisma } = require("@prisma/client");

const prisma = new PrismaClient();

function mapByName(records = []) {
  return records.reduce((acc, record) => {
    acc[record.name] = record;
    return acc;
  }, {});
}

function mapByField(records = [], field) {
  return records.reduce((acc, record) => {
    if (record[field]) {
      acc[record[field]] = record;
    }
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

const feeCategorySeeds = [
  { name: "Tuition", code: "TUITION", defaultAmount: new Prisma.Decimal("350.00"), sortOrder: 1 },
  { name: "Laboratory", code: "LAB", defaultAmount: new Prisma.Decimal("45.00"), sortOrder: 2 },
  { name: "Transportation", code: "TRANSPORT", defaultAmount: new Prisma.Decimal("60.00"), sortOrder: 3 },
  { name: "Library", code: "LIBRARY", defaultAmount: new Prisma.Decimal("15.00"), sortOrder: 4 },
];

const libraryCategorySeeds = [
  { name: "Fiction", code: "FIC", sortOrder: 1, description: "Novels and stories." },
  { name: "Non-fiction", code: "NF", sortOrder: 2, description: "Biographies and informational titles." },
  { name: "Reference", code: "REF", sortOrder: 3, description: "Encyclopedias, atlases, and handbooks." },
];

const subjectSeeds = [
  { name: "Mathematics", code: "MATH-101", description: "Core algebra and geometry." },
  { name: "Earth Science", code: "SCI-201", description: "Introductory geology and meteorology." },
  { name: "English Language Arts", code: "ENG-110", description: "Reading comprehension and writing." },
  { name: "World History", code: "HIS-205", description: "Survey of global civilizations." },
];

const assessmentTypeSeeds = [
  { name: "Exam", weight: 0.4, description: "Unit and quarterly exams." },
  { name: "Quiz", weight: 0.1, description: "Short comprehension checks." },
  { name: "Project", weight: 0.3, description: "Extended project-based work." },
  { name: "Homework", weight: 0.2, description: "Practice assignments." },
];

function parseDate(value) {
  if (!value) {
    return null;
  }

  const date = new Date(`${value}T00:00:00Z`);
  return Number.isNaN(date.getTime()) ? null : date;
}

function parseDateTime(value) {
  if (!value) {
    return null;
  }

  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? null : date;
}

function slugify(value) {
  if (!value) {
    return "";
  }
  return value
    .toString()
    .trim()
    .toLowerCase()
    .replace(/[^a-z0-9]+/g, "-")
    .replace(/^-+|-+$/g, "")
    .substring(0, 60);
}

async function seedLookups() {
  await upsertLookup(prisma.gender, genderSeeds);
  await upsertLookup(prisma.studentStatus, studentStatusSeeds);
  await upsertLookup(prisma.gradeLevel, gradeLevelSeeds);
  await upsertLookup(prisma.staffType, staffTypeSeeds);
  await upsertLookup(prisma.employmentStatus, employmentStatusSeeds);
  await upsertLookup(prisma.feeCategory, feeCategorySeeds);
  await upsertLookup(prisma.libraryCategory, libraryCategorySeeds);

  const [genders, studentStatuses, gradeLevels, staffTypes, employmentStatuses, libraryCategories] =
    await Promise.all([
      prisma.gender.findMany(),
      prisma.studentStatus.findMany(),
      prisma.gradeLevel.findMany(),
      prisma.staffType.findMany(),
      prisma.employmentStatus.findMany(),
      prisma.libraryCategory.findMany(),
    ]);

  return {
    genders: mapByName(genders),
    studentStatuses: mapByName(studentStatuses),
    gradeLevels: mapByName(gradeLevels),
    staffTypes: mapByName(staffTypes),
    employmentStatuses: mapByName(employmentStatuses),
    libraryCategories: mapByName(libraryCategories),
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

const noticeSamples = [
  {
    slug: "welcome-back-week",
    title: "Welcome Back Week Highlights",
    summary: "Key reminders and celebrations as we kick off the new term.",
    content:
      "<p>Our welcome back week is underway! Don't miss the student activities fair on Wednesday and the family breakfast on Friday morning.</p><p>Teachers should submit advisory updates by Thursday at noon.</p>",
    status: "PUBLISHED",
    publishAt: "2024-08-15T13:00:00Z",
    expiresAt: "2024-09-01T00:00:00Z",
    isPinned: true,
    audiences: ["Student", "Teacher", "Staff"],
    createdBy: "staff",
  },
  {
    slug: "technology-upgrades",
    title: "Technology Lab Upgrades",
    summary: "Scheduled maintenance and what to expect from the refreshed computer labs.",
    content:
      "<p>The main computer lab will be offline from September 12-13 for network upgrades. Please plan to use the library collaboration space if you need computers during that window.</p><p>Staff training on the new equipment is scheduled for September 16.</p>",
    status: "SCHEDULED",
    publishAt: "2024-09-05T14:30:00Z",
    expiresAt: "2024-09-20T00:00:00Z",
    isPinned: false,
    audiences: ["Teacher", "Staff"],
    createdBy: "admin",
  },
  {
    slug: "family-newsletter",
    title: "September Family Newsletter",
    summary: "Share this monthly roundup with guardians and caregivers.",
    content:
      "<p>The September newsletter includes highlights from athletics, classroom spotlights, and important reminders about back-to-school nights. Printable PDFs are available in English and Spanish.</p>",
    status: "PUBLISHED",
    publishAt: "2024-08-28T12:00:00Z",
    expiresAt: "2024-10-01T00:00:00Z",
    isPinned: false,
    audiences: ["Student", "Teacher", "Staff"],
    createdBy: "staff",
  },
];

const eventSamples = [
  {
    slug: "fall-open-house",
    title: "Fall Open House",
    summary: "An evening for prospective families to explore our campus and programs.",
    description:
      "<p>Faculty and student ambassadors will lead classroom tours. Light refreshments will be served in the cafeteria.</p>",
    location: "Main Campus",
    status: "PUBLISHED",
    visibility: "COMMUNITY",
    startAt: "2024-10-03T22:00:00Z",
    endAt: "2024-10-04T00:00:00Z",
    publishAt: "2024-08-20T13:00:00Z",
    registrationDeadline: "2024-09-30T23:59:59Z",
    isAllDay: false,
    audiences: ["Student", "Teacher", "Staff"],
    createdBy: "admin",
    registrations: [
      { userKey: "teacher", status: "GOING" },
      { userKey: "staff", status: "GOING" },
    ],
  },
  {
    slug: "staff-retreat",
    title: "Staff Wellness Retreat",
    summary: "A day of collaboration and wellness workshops for all staff members.",
    description:
      "<p>We'll gather offsite at Willow Creek Center for professional learning, mindfulness sessions, and team-building activities. Transportation departs from campus at 8:00 AM.</p>",
    location: "Willow Creek Center",
    status: "SCHEDULED",
    visibility: "INTERNAL",
    startAt: "2024-09-20T13:00:00Z",
    endAt: "2024-09-20T21:00:00Z",
    publishAt: "2024-08-25T12:00:00Z",
    registrationDeadline: "2024-09-10T23:59:59Z",
    isAllDay: false,
    audiences: ["Teacher", "Staff"],
    createdBy: "staff",
    registrations: [{ userKey: "teacher", status: "INTERESTED" }],
  },
  {
    slug: "homecoming-week",
    title: "Homecoming Spirit Week",
    summary: "Theme days, pep rally, and the big game to celebrate our community.",
    description:
      "<p>Check the daily dress themes and volunteer opportunities for parents and guardians. The pep rally will be Friday afternoon with the game that evening.</p>",
    location: "Gymnasium & Athletics Field",
    status: "PUBLISHED",
    visibility: "COMMUNITY",
    startAt: "2024-10-14T12:00:00Z",
    endAt: "2024-10-19T02:00:00Z",
    publishAt: "2024-09-10T12:00:00Z",
    registrationDeadline: null,
    isAllDay: false,
    audiences: ["Student", "Teacher", "Staff"],
    createdBy: "admin",
  },
];

const feeLedgerSamples = [
  {
    studentNumber: "STU-0001",
    entries: [
      {
        entryType: "CHARGE",
        categoryCode: "TUITION",
        amount: "350.00",
        entryDate: "2024-09-01",
        description: "Tuition - Fall 2024",
      },
      {
        entryType: "CHARGE",
        categoryCode: "LAB",
        amount: "45.00",
        entryDate: "2024-09-01",
        description: "Science lab materials fee",
      },
      {
        entryType: "PAYMENT",
        amount: "200.00",
        entryDate: "2024-09-15",
        description: "Partial tuition payment",
        paymentMethod: "CASH",
        receiptNumber: "RCPT-2024-0001",
      },
    ],
  },
  {
    studentNumber: "STU-0002",
    entries: [
      {
        entryType: "CHARGE",
        categoryCode: "TUITION",
        amount: "350.00",
        entryDate: "2024-09-01",
        description: "Tuition - Fall 2024",
      },
      {
        entryType: "CHARGE",
        categoryCode: "TRANSPORT",
        amount: "60.00",
        entryDate: "2024-09-03",
        description: "Monthly transport fee",
      },
      {
        entryType: "PAYMENT",
        amount: "150.00",
        entryDate: "2024-09-20",
        description: "Online banking payment",
        paymentMethod: "TRANSFER",
        receiptNumber: "RCPT-2024-0002",
      },
    ],
  },
  {
    studentNumber: "STU-0003",
    entries: [
      {
        entryType: "CHARGE",
        categoryCode: "LIBRARY",
        amount: "15.00",
        entryDate: "2024-05-01",
        description: "Library replacement card",
      },
      {
        entryType: "ADJUSTMENT",
        amount: "-15.00",
        entryDate: "2024-05-10",
        description: "Waived library replacement charge",
      },
    ],
  },
];

const libraryBookSamples = [
  {
    title: "The Great Gatsby",
    isbn: "9780743273565",
    author: "F. Scott Fitzgerald",
    publisher: "Scribner",
    publishedYear: 2004,
    category: "Fiction",
    summary: "Classic novel chronicling the Jazz Age and the pursuit of the American Dream.",
    copies: [
      { barcode: "GG-001", location: "FIC FIT", status: "AVAILABLE" },
      { barcode: "GG-002", location: "FIC FIT", status: "LOANED" },
    ],
    loans: [
      {
        barcode: "GG-002",
        studentNumber: "STU-0002",
        issuedAt: "2024-09-10",
        dueAt: "2024-09-24",
        issuedBy: "staff",
        notes: "Class reading assignment.",
      },
    ],
  },
  {
    title: "Hidden Figures",
    isbn: "9780062363602",
    author: "Margot Lee Shetterly",
    publisher: "William Morrow",
    publishedYear: 2016,
    category: "Non-fiction",
    summary: "Story of the Black women mathematicians who helped win the space race.",
    copies: [
      { barcode: "HF-001", location: "NF SHE", status: "AVAILABLE" },
    ],
    loans: [
      {
        barcode: "HF-001",
        studentNumber: "STU-0001",
        issuedAt: "2024-08-01",
        dueAt: "2024-08-15",
        returnedAt: "2024-08-12",
        issuedBy: "staff",
        receivedBy: "staff",
        notes: "Summer reading checkout.",
      },
    ],
  },
  {
    title: "Atlas of the World",
    isbn: "9781465453603",
    author: "National Geographic",
    publisher: "National Geographic",
    publishedYear: 2017,
    category: "Reference",
    summary: "Comprehensive atlas for geography projects and research.",
    copies: [
      { barcode: "AT-001", location: "REF ATLAS", status: "AVAILABLE" },
      { barcode: "AT-002", location: "REF ATLAS", status: "AVAILABLE" },
    ],
    loans: [],
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

async function seedCommunications(users) {
  const roleByName = users.roleByName || {};

  for (const sample of noticeSamples) {
    const slug = sample.slug || slugify(sample.title);
    const publishAt = parseDateTime(sample.publishAt) || null;
    const expiresAt = parseDateTime(sample.expiresAt) || null;
    const createdById = sample.createdBy && users[sample.createdBy] ? users[sample.createdBy].id : users.admin.id;
    const updatedById = sample.updatedBy && users[sample.updatedBy]
      ? users[sample.updatedBy].id
      : createdById;

    const notice = await prisma.notice.upsert({
      where: { slug },
      update: {
        title: sample.title,
        summary: sample.summary || null,
        content: sample.content,
        status: sample.status,
        publishAt,
        expiresAt,
        isPinned: Boolean(sample.isPinned),
        updatedById,
      },
      create: {
        slug,
        title: sample.title,
        summary: sample.summary || null,
        content: sample.content,
        status: sample.status,
        publishAt,
        expiresAt,
        isPinned: Boolean(sample.isPinned),
        createdById,
        updatedById,
      },
    });

    if (Array.isArray(sample.audiences)) {
      await prisma.noticeAudienceRole.deleteMany({ where: { noticeId: notice.id } });
      const roleIds = sample.audiences
        .map((roleName) => roleByName[roleName]?.id)
        .filter(Boolean);
      const uniqueRoleIds = [...new Set(roleIds)];
      if (uniqueRoleIds.length > 0) {
        await prisma.noticeAudienceRole.createMany({
          data: uniqueRoleIds.map((roleId) => ({ noticeId: notice.id, roleId })),
        });
      }
    }
  }

  for (const sample of eventSamples) {
    const slug = sample.slug || slugify(sample.title);
    const startAt = parseDateTime(sample.startAt) || new Date();
    const endAt = parseDateTime(sample.endAt);
    const publishAt = parseDateTime(sample.publishAt) || null;
    const registrationDeadline = parseDateTime(sample.registrationDeadline) || null;
    const createdById = sample.createdBy && users[sample.createdBy] ? users[sample.createdBy].id : users.admin.id;
    const updatedById = sample.updatedBy && users[sample.updatedBy]
      ? users[sample.updatedBy].id
      : createdById;

    const event = await prisma.event.upsert({
      where: { slug },
      update: {
        title: sample.title,
        summary: sample.summary || null,
        description: sample.description || null,
        location: sample.location || null,
        status: sample.status,
        visibility: sample.visibility || "INTERNAL",
        startAt,
        endAt,
        publishAt,
        registrationDeadline,
        isAllDay: Boolean(sample.isAllDay),
        updatedById,
      },
      create: {
        slug,
        title: sample.title,
        summary: sample.summary || null,
        description: sample.description || null,
        location: sample.location || null,
        status: sample.status,
        visibility: sample.visibility || "INTERNAL",
        startAt,
        endAt,
        publishAt,
        registrationDeadline,
        isAllDay: Boolean(sample.isAllDay),
        createdById,
        updatedById,
      },
    });

    if (Array.isArray(sample.audiences)) {
      await prisma.eventAudienceRole.deleteMany({ where: { eventId: event.id } });
      const roleIds = sample.audiences
        .map((roleName) => roleByName[roleName]?.id)
        .filter(Boolean);
      const uniqueRoleIds = [...new Set(roleIds)];
      if (uniqueRoleIds.length > 0) {
        await prisma.eventAudienceRole.createMany({
          data: uniqueRoleIds.map((roleId) => ({ eventId: event.id, roleId })),
        });
      }
    }

    if (Array.isArray(sample.registrations)) {
      const userIds = [];
      for (const registration of sample.registrations) {
        const userRecord = registration.userKey ? users[registration.userKey] : null;
        if (!userRecord) {
          continue;
        }
        userIds.push(userRecord.id);
        const respondedAt = parseDateTime(registration.respondedAt) || new Date();
        await prisma.eventRegistration.upsert({
          where: {
            eventId_userId: {
              eventId: event.id,
              userId: userRecord.id,
            },
          },
          update: {
            status: registration.status || "GOING",
            respondedAt,
            notes: registration.notes || null,
          },
          create: {
            eventId: event.id,
            userId: userRecord.id,
            status: registration.status || "GOING",
            respondedAt,
            notes: registration.notes || null,
          },
        });
      }

      if (userIds.length > 0) {
        await prisma.eventRegistration.deleteMany({
          where: {
            eventId: event.id,
            userId: { notIn: userIds },
          },
        });
      }
    }
  }
}

function calculateGradeMetrics(score, maxScore) {
  if (
    score === null ||
    score === undefined ||
    maxScore === null ||
    maxScore === undefined
  ) {
    return { percentage: null, letter: null };
  }

  const parsedScore = Number(score);
  const parsedMax = Number(maxScore);
  if (!Number.isFinite(parsedScore) || !Number.isFinite(parsedMax) || parsedMax <= 0) {
    return { percentage: null, letter: null };
  }

  const percentage = Number(((Math.max(0, parsedScore) / parsedMax) * 100).toFixed(2));
  let letter = null;
  if (percentage >= 90) {
    letter = "A";
  } else if (percentage >= 80) {
    letter = "B";
  } else if (percentage >= 70) {
    letter = "C";
  } else if (percentage >= 60) {
    letter = "D";
  } else {
    letter = "F";
  }

  return { percentage, letter };
}

async function seedSubjects() {
  await upsertLookup(prisma.subject, subjectSeeds);
  const subjects = await prisma.subject.findMany();
  return mapByName(subjects);
}

async function seedAssessmentTypes() {
  await upsertLookup(prisma.assessmentType, assessmentTypeSeeds);
  const types = await prisma.assessmentType.findMany();
  return mapByName(types);
}

const classSamples = [
  {
    name: "Grade 9 - Homeroom A",
    code: "G9-A",
    description: "Freshman core cohort.",
    gradeLevel: "Grade 9",
    homeroomTeacherEmployee: "EMP-0456",
    startDate: "2024-09-03",
    endDate: "2025-06-15",
    subjects: [
      { subject: "Mathematics", teacherEmployee: "EMP-0456", schedule: "Mon/Wed/Fri 09:00" },
      { subject: "English Language Arts", teacherEmployee: "EMP-0721", schedule: "Tue/Thu 09:00" },
    ],
    enrollments: [
      { studentNumber: "STU-0001", status: "Active", enrolledAt: "2024-09-02" },
      { studentNumber: "STU-0002", status: "Active", enrolledAt: "2022-09-06" },
    ],
    assessments: [
      {
        subject: "Mathematics",
        type: "Exam",
        title: "Quarter 1 Exam",
        dueDate: "2024-11-15",
        maxScore: 100,
        grades: [
          { studentNumber: "STU-0001", score: 92, teacherEmployee: "EMP-0456" },
          { studentNumber: "STU-0002", score: 85, teacherEmployee: "EMP-0456" },
        ],
      },
      {
        subject: "English Language Arts",
        type: "Project",
        title: "Literary Analysis Essay",
        dueDate: "2024-10-28",
        maxScore: 50,
        grades: [
          { studentNumber: "STU-0001", score: 44, teacherEmployee: "EMP-0721" },
          { studentNumber: "STU-0002", score: 40, teacherEmployee: "EMP-0721" },
        ],
      },
    ],
  },
  {
    name: "Grade 11 Science Cohort",
    code: "G11-SCI",
    description: "Upper school science rotation.",
    gradeLevel: "Grade 11",
    homeroomTeacherEmployee: "EMP-0833",
    startDate: "2024-09-03",
    endDate: "2025-06-15",
    subjects: [
      { subject: "Earth Science", teacherEmployee: "EMP-0833", schedule: "Mon/Wed 11:00" },
      { subject: "World History", teacherEmployee: "EMP-0721", schedule: "Tue/Thu 13:00" },
    ],
    enrollments: [
      { studentNumber: "STU-0002", status: "Active", enrolledAt: "2022-09-06" },
      {
        studentNumber: "STU-0003",
        status: "Graduated",
        enrolledAt: "2020-09-08",
        exitDate: "2024-06-15",
      },
    ],
    assessments: [
      {
        subject: "Earth Science",
        type: "Quiz",
        title: "Plate Tectonics Quiz",
        dueDate: "2024-09-20",
        maxScore: 20,
        grades: [
          { studentNumber: "STU-0002", score: 18, teacherEmployee: "EMP-0833" },
          { studentNumber: "STU-0003", score: 19, teacherEmployee: "EMP-0833" },
        ],
      },
    ],
  },
];

async function seedClassroomsAndGrades(lookupData) {
  const subjectMap = await seedSubjects();
  const assessmentTypeMap = await seedAssessmentTypes();

  const [teacherRecords, studentRecords] = await Promise.all([
    prisma.teacherStaff.findMany({ include: { person: true } }),
    prisma.student.findMany({ include: { person: true } }),
  ]);

  const teacherByEmployee = mapByField(teacherRecords, "employeeNumber");
  const studentByNumber = mapByField(studentRecords, "studentNumber");
  const gradeLevelByName = lookupData.gradeLevels;

  for (const sample of classSamples) {
    const gradeLevel = sample.gradeLevel ? gradeLevelByName[sample.gradeLevel] : null;
    const homeroomTeacher = sample.homeroomTeacherEmployee
      ? teacherByEmployee[sample.homeroomTeacherEmployee]
      : null;

    const classroom = await prisma.classroom.upsert({
      where: sample.code ? { code: sample.code } : { name: sample.name },
      update: {
        name: sample.name,
        description: sample.description || null,
        gradeLevelId: gradeLevel ? gradeLevel.id : null,
        homeroomTeacherId: homeroomTeacher ? homeroomTeacher.id : null,
        startDate: parseDate(sample.startDate),
        endDate: parseDate(sample.endDate),
        isArchived: false,
      },
      create: {
        name: sample.name,
        code: sample.code || null,
        description: sample.description || null,
        gradeLevel: gradeLevel ? { connect: { id: gradeLevel.id } } : undefined,
        homeroomTeacher: homeroomTeacher
          ? { connect: { id: homeroomTeacher.id } }
          : undefined,
        startDate: parseDate(sample.startDate),
        endDate: parseDate(sample.endDate),
        isArchived: false,
      },
    });

    const classSubjectMap = new Map();

    for (const assignment of sample.subjects ?? []) {
      const subject = subjectMap[assignment.subject];
      if (!subject) {
        // eslint-disable-next-line no-console
        console.warn(`Missing subject for assignment: ${assignment.subject}`);
        continue;
      }
      const teacher = assignment.teacherEmployee
        ? teacherByEmployee[assignment.teacherEmployee]
        : null;

      const classSubject = await prisma.classSubject.upsert({
        where: {
          classroomId_subjectId: {
            classroomId: classroom.id,
            subjectId: subject.id,
          },
        },
        update: {
          teacherId: teacher ? teacher.id : null,
          schedule: assignment.schedule || null,
        },
        create: {
          classroomId: classroom.id,
          subjectId: subject.id,
          teacherId: teacher ? teacher.id : null,
          schedule: assignment.schedule || null,
        },
      });

      classSubjectMap.set(assignment.subject, classSubject);
    }

    const enrollmentMap = new Map();

    for (const enrollmentSample of sample.enrollments ?? []) {
      const student = studentByNumber[enrollmentSample.studentNumber];
      if (!student) {
        // eslint-disable-next-line no-console
        console.warn(`Missing student for enrollment: ${enrollmentSample.studentNumber}`);
        continue;
      }

      const enrollment = await prisma.enrollment.upsert({
        where: {
          classroomId_studentId: {
            classroomId: classroom.id,
            studentId: student.id,
          },
        },
        update: {
          status: enrollmentSample.status || "Active",
          enrolledAt: parseDate(enrollmentSample.enrolledAt),
          exitDate: parseDate(enrollmentSample.exitDate),
          notes: enrollmentSample.notes || null,
        },
        create: {
          classroomId: classroom.id,
          studentId: student.id,
          status: enrollmentSample.status || "Active",
          enrolledAt: parseDate(enrollmentSample.enrolledAt),
          exitDate: parseDate(enrollmentSample.exitDate),
          notes: enrollmentSample.notes || null,
        },
      });

      enrollmentMap.set(`${student.id}`, enrollment);
    }

    for (const assessmentSample of sample.assessments ?? []) {
      const classSubjectRecord = classSubjectMap.get(assessmentSample.subject);
      if (!classSubjectRecord) {
        continue;
      }

      const assessmentType = assessmentTypeMap[assessmentSample.type];
      if (!assessmentType) {
        continue;
      }

      const existingAssessment = await prisma.assessment.findFirst({
        where: {
          classSubjectId: classSubjectRecord.id,
          title: assessmentSample.title,
        },
      });

      const assessmentData = {
        assessmentTypeId: assessmentType.id,
        title: assessmentSample.title,
        description: assessmentSample.description || null,
        dueDate: parseDate(assessmentSample.dueDate),
        maxScore:
          assessmentSample.maxScore !== undefined && assessmentSample.maxScore !== null
            ? Number(assessmentSample.maxScore)
            : null,
      };

      let assessment;
      if (existingAssessment) {
        assessment = await prisma.assessment.update({
          where: { id: existingAssessment.id },
          data: assessmentData,
        });
      } else {
        assessment = await prisma.assessment.create({
          data: {
            classSubjectId: classSubjectRecord.id,
            ...assessmentData,
          },
        });
      }

      for (const gradeSample of assessmentSample.grades ?? []) {
        const student = studentByNumber[gradeSample.studentNumber];
        if (!student) {
          continue;
        }
        const enrollment = enrollmentMap.get(`${student.id}`);
        if (!enrollment) {
          continue;
        }
        const teacher = gradeSample.teacherEmployee
          ? teacherByEmployee[gradeSample.teacherEmployee]
          : null;
        const maxScore =
          gradeSample.maxScore !== undefined && gradeSample.maxScore !== null
            ? gradeSample.maxScore
            : assessment.maxScore;

        const metrics = calculateGradeMetrics(gradeSample.score, maxScore);

        await prisma.gradeEntry.upsert({
          where: {
            assessmentId_studentId: {
              assessmentId: assessment.id,
              studentId: student.id,
            },
          },
          update: {
            enrollmentId: enrollment.id,
            score: gradeSample.score ?? null,
            maxScore: maxScore ?? null,
            percentage: metrics.percentage,
            letterGrade: metrics.letter,
            gradedAt: parseDate(gradeSample.gradedAt) || new Date(),
            gradedById: teacher ? teacher.id : null,
            comments: gradeSample.comments || null,
          },
          create: {
            assessmentId: assessment.id,
            enrollmentId: enrollment.id,
            studentId: student.id,
            score: gradeSample.score ?? null,
            maxScore: maxScore ?? null,
            percentage: metrics.percentage,
            letterGrade: metrics.letter,
            gradedAt: parseDate(gradeSample.gradedAt) || new Date(),
            gradedById: teacher ? teacher.id : null,
            comments: gradeSample.comments || null,
          },
        });
      }
    }
  }
}

async function seedFeeData(users) {
  const [categoryRecords, studentRecords] = await Promise.all([
    prisma.feeCategory.findMany(),
    prisma.student.findMany(),
  ]);

  const categoryByCode = mapByField(categoryRecords, "code");
  const studentByNumber = mapByField(studentRecords, "studentNumber");
  const recordedById = users.staff?.id || users.admin.id;

  for (const sample of feeLedgerSamples) {
    const student = studentByNumber[sample.studentNumber];
    if (!student) {
      // eslint-disable-next-line no-console
      console.warn(`Missing student for fee ledger seed: ${sample.studentNumber}`);
      continue;
    }

    for (const entry of sample.entries) {
      const category = entry.categoryCode ? categoryByCode[entry.categoryCode] : null;
      const entryDate = parseDate(entry.entryDate) || new Date();
      const baseData = {
        studentId: student.id,
        categoryId: category ? category.id : null,
        recordedById,
        entryType: entry.entryType,
        paymentMethod: entry.paymentMethod || null,
        amount: new Prisma.Decimal(entry.amount),
        entryDate,
        description: entry.description || null,
        receiptNumber: entry.receiptNumber || null,
      };

      if (entry.receiptNumber) {
        await prisma.feeLedgerEntry.upsert({
          where: { receiptNumber: entry.receiptNumber },
          update: baseData,
          create: baseData,
        });
        continue;
      }

      const existing = await prisma.feeLedgerEntry.findFirst({
        where: {
          studentId: student.id,
          entryType: entry.entryType,
          description: entry.description || null,
          entryDate,
        },
      });

      if (existing) {
        await prisma.feeLedgerEntry.update({
          where: { id: existing.id },
          data: baseData,
        });
      } else {
        await prisma.feeLedgerEntry.create({ data: baseData });
      }
    }
  }
}

async function seedLibraryData(users) {
  const [categoryRecords, studentRecords] = await Promise.all([
    prisma.libraryCategory.findMany(),
    prisma.student.findMany(),
  ]);

  const categoryByName = mapByName(categoryRecords);
  const categoryByCode = mapByField(categoryRecords, "code");
  const studentByNumber = mapByField(studentRecords, "studentNumber");

  const copyByBarcode = new Map();

  for (const sample of libraryBookSamples) {
    const category = sample.category
      ? categoryByName[sample.category] || categoryByCode[sample.category]
      : null;

    const book = await prisma.libraryBook.upsert({
      where: sample.isbn ? { isbn: sample.isbn } : { title: sample.title },
      update: {
        title: sample.title,
        author: sample.author || null,
        publisher: sample.publisher || null,
        publishedYear: sample.publishedYear || null,
        summary: sample.summary || null,
        isArchived: false,
        categoryId: category ? category.id : null,
      },
      create: {
        title: sample.title,
        isbn: sample.isbn || null,
        author: sample.author || null,
        publisher: sample.publisher || null,
        publishedYear: sample.publishedYear || null,
        summary: sample.summary || null,
        isArchived: false,
        categoryId: category ? category.id : null,
      },
    });

    for (const copySample of sample.copies ?? []) {
      const status = copySample.status || "AVAILABLE";
      let copy;
      if (copySample.barcode) {
        copy = await prisma.libraryCopy.upsert({
          where: { barcode: copySample.barcode },
          update: {
            bookId: book.id,
            status,
            acquiredAt: parseDate(copySample.acquiredAt),
            location: copySample.location || null,
            isArchived: false,
          },
          create: {
            bookId: book.id,
            barcode: copySample.barcode,
            status,
            acquiredAt: parseDate(copySample.acquiredAt),
            location: copySample.location || null,
            isArchived: false,
          },
        });
      } else {
        copy = await prisma.libraryCopy.create({
          data: {
            bookId: book.id,
            status,
            acquiredAt: parseDate(copySample.acquiredAt),
            location: copySample.location || null,
            isArchived: false,
          },
        });
      }

      if (copySample.barcode) {
        copyByBarcode.set(copySample.barcode, copy);
      }
    }
  }

  for (const sample of libraryBookSamples) {
    for (const loanSample of sample.loans ?? []) {
      const copy = loanSample.barcode ? copyByBarcode.get(loanSample.barcode) : null;
      const student = loanSample.studentNumber
        ? studentByNumber[loanSample.studentNumber]
        : null;

      if (!copy || !student) {
        // eslint-disable-next-line no-console
        console.warn(
          `Skipping library loan seed for barcode ${loanSample.barcode} and student ${loanSample.studentNumber}`
        );
        continue;
      }

      const issuedAt = parseDate(loanSample.issuedAt) || new Date();
      const dueAt = parseDate(loanSample.dueAt);
      const returnedAt = parseDate(loanSample.returnedAt);

      const issuedById = loanSample.issuedBy
        ? users[loanSample.issuedBy]?.id || users.admin.id
        : users.admin.id;
      const receivedById = loanSample.receivedBy
        ? users[loanSample.receivedBy]?.id || users.admin.id
        : null;

      const existing = await prisma.libraryLoan.findFirst({
        where: { copyId: copy.id, issuedAt },
      });

      if (existing) {
        await prisma.libraryLoan.update({
          where: { id: existing.id },
          data: {
            studentId: student.id,
            issuedById,
            receivedById,
            dueAt,
            returnedAt,
            notes: loanSample.notes || null,
          },
        });
      } else {
        await prisma.libraryLoan.create({
          data: {
            copyId: copy.id,
            studentId: student.id,
            issuedById,
            receivedById,
            issuedAt,
            dueAt,
            returnedAt,
            notes: loanSample.notes || null,
          },
        });
      }

      await prisma.libraryCopy.update({
        where: { id: copy.id },
        data: {
          status: returnedAt ? "AVAILABLE" : "LOANED",
        },
      });
    }
  }
}

async function main() {
  const saltRounds = Number(process.env.BCRYPT_COST || 10);

  const lookupData = await seedLookups();
  const users = await seedRolesAndUsers(saltRounds);
  await seedStudents(lookupData, users);
  await seedTeachers(lookupData, users);
  await seedCommunications(users);
  await seedFeeData(users);
  await seedLibraryData(users);
  await seedClassroomsAndGrades(lookupData);
}

main()
  .then(async () => {
    console.log(
      "Database seeded with default lookups, demo users, and representative academic, communications, fee, and library records."
    );
    await prisma.$disconnect();
  })
  .catch(async (error) => {
    console.error("Failed to seed database", error);
    await prisma.$disconnect();
    process.exit(1);
  });
