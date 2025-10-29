const { prisma } = require("../db/client");

const defaultTeacherInclude = {
  person: {
    include: {
      gender: true,
    },
  },
  staffType: true,
  employmentStatus: true,
};

const teacherOrderBy = [
  { person: { lastName: "asc" } },
  { person: { firstName: "asc" } },
];

async function listTeachers({ search } = {}) {
  const where = search
    ? {
        OR: [
          { employeeNumber: { contains: search } },
          { person: { firstName: { contains: search } } },
          { person: { lastName: { contains: search } } },
          { primarySubject: { contains: search } },
        ],
      }
    : undefined;

  return prisma.teacherStaff.findMany({
    where,
    include: defaultTeacherInclude,
    orderBy: teacherOrderBy,
  });
}

async function getTeacherById(id) {
  return prisma.teacherStaff.findUnique({
    where: { id },
    include: defaultTeacherInclude,
  });
}

async function getTeacherLookups() {
  const [genders, staffTypes, employmentStatuses] = await Promise.all([
    prisma.gender.findMany({ orderBy: [{ sortOrder: "asc" }, { name: "asc" }] }),
    prisma.staffType.findMany({
      where: { isActive: true },
      orderBy: [{ sortOrder: "asc" }, { name: "asc" }],
    }),
    prisma.employmentStatus.findMany({
      where: { isActive: true },
      orderBy: [{ sortOrder: "asc" }, { name: "asc" }],
    }),
  ]);

  return { genders, staffTypes, employmentStatuses };
}

async function createTeacher(payload) {
  return prisma.$transaction(async (tx) => {
    const person = await tx.person.create({
      data: payload.person,
    });

    return tx.teacherStaff.create({
      data: {
        ...payload.teacher,
        personId: person.id,
      },
      include: defaultTeacherInclude,
    });
  });
}

async function updateTeacher(id, payload) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.teacherStaff.findUnique({
      where: { id },
    });

    if (!existing) {
      return null;
    }

    await tx.person.update({
      where: { id: existing.personId },
      data: payload.person,
    });

    return tx.teacherStaff.update({
      where: { id },
      data: payload.teacher,
      include: defaultTeacherInclude,
    });
  });
}

async function deleteTeacher(id) {
  return prisma.$transaction(async (tx) => {
    const existing = await tx.teacherStaff.findUnique({
      where: { id },
    });

    if (!existing) {
      return false;
    }

    await tx.teacherStaff.delete({ where: { id } });
    await tx.person.delete({ where: { id: existing.personId } });

    return true;
  });
}

async function searchTeachers(term, limit = 10) {
  const searchTerm = term ? term.trim() : "";

  const teachers = await prisma.teacherStaff.findMany({
    where: searchTerm
      ? {
          OR: [
            { employeeNumber: { contains: searchTerm } },
            { person: { firstName: { contains: searchTerm } } },
            { person: { lastName: { contains: searchTerm } } },
            { primarySubject: { contains: searchTerm } },
          ],
        }
      : undefined,
    include: { person: true },
    orderBy: teacherOrderBy,
    take: limit,
  });

  return teachers.map((teacher) => ({
    id: teacher.id,
    label: `${teacher.person.firstName} ${teacher.person.lastName}`.trim(),
    value: teacher.id,
    employeeNumber: teacher.employeeNumber,
  }));
}

module.exports = {
  listTeachers,
  getTeacherById,
  getTeacherLookups,
  createTeacher,
  updateTeacher,
  deleteTeacher,
  searchTeachers,
};
