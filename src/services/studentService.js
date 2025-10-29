const { prisma } = require("../db/client");

const defaultStudentInclude = {
  person: {
    include: {
      gender: true,
    },
  },
  gradeLevel: true,
  enrollmentStatus: true,
};

const studentOrderBy = [
  { person: { lastName: "asc" } },
  { person: { firstName: "asc" } },
];

async function listStudents({ search } = {}) {
  const where = search
    ? {
        OR: [
          { studentNumber: { contains: search } },
          { person: { firstName: { contains: search } } },
          { person: { lastName: { contains: search } } },
        ],
      }
    : undefined;

  return prisma.student.findMany({
    where,
    include: defaultStudentInclude,
    orderBy: studentOrderBy,
  });
}

async function getStudentById(id) {
  return prisma.student.findUnique({
    where: { id },
    include: defaultStudentInclude,
  });
}

async function getStudentLookups() {
  const [genders, statuses, gradeLevels] = await Promise.all([
    prisma.gender.findMany({ orderBy: [{ sortOrder: "asc" }, { name: "asc" }] }),
    prisma.studentStatus.findMany({
      where: { isActive: true },
      orderBy: [{ sortOrder: "asc" }, { name: "asc" }],
    }),
    prisma.gradeLevel.findMany({
      where: { isActive: true },
      orderBy: [{ sortOrder: "asc" }, { name: "asc" }],
    }),
  ]);

  return { genders, statuses, gradeLevels };
}

async function createStudent(payload) {
  return prisma.$transaction(async (tx) => {
    const person = await tx.person.create({
      data: payload.person,
    });

    return tx.student.create({
      data: {
        ...payload.student,
        personId: person.id,
      },
      include: defaultStudentInclude,
    });
  });
}

async function updateStudent(id, payload) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.student.findUnique({
      where: { id },
    });

    if (!existing) {
      return null;
    }

    await tx.person.update({
      where: { id: existing.personId },
      data: payload.person,
    });

    return tx.student.update({
      where: { id },
      data: payload.student,
      include: defaultStudentInclude,
    });
  });
}

async function deleteStudent(id) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.student.findUnique({
      where: { id },
    });

    if (!existing) {
      return false;
    }

    await tx.student.delete({ where: { id } });
    await tx.person.delete({ where: { id: existing.personId } });

    return true;
  });
}

async function searchStudents(term, limit = 10) {
  const searchTerm = term ? term.trim() : "";

  const students = await prisma.student.findMany({
    where: searchTerm
      ? {
          OR: [
            {
              studentNumber: { contains: searchTerm },
            },
            { person: { firstName: { contains: searchTerm } } },
            { person: { lastName: { contains: searchTerm } } },
          ],
        }
      : undefined,
    include: { person: true },
    orderBy: studentOrderBy,
    take: limit,
  });

  return students.map((student) => ({
    id: student.id,
    label: `${student.person.firstName} ${student.person.lastName}`.trim(),
    value: student.id,
    studentNumber: student.studentNumber,
  }));
}

module.exports = {
  listStudents,
  getStudentById,
  getStudentLookups,
  createStudent,
  updateStudent,
  deleteStudent,
  searchStudents,
};
