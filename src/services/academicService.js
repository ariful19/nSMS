const { prisma } = require("../db/client");

function buildPersonName(person) {
  if (!person) {
    return null;
  }
  const parts = [person.firstName, person.middleName, person.lastName].filter(Boolean);
  return parts.join(" ") || null;
}

function normalizeOptionalInt(value) {
  if (value === undefined || value === null || value === "") {
    return null;
  }
  const parsed = Number.parseInt(value, 10);
  return Number.isNaN(parsed) ? null : parsed;
}

function calculateLetterFromPercentage(percentage) {
  if (percentage === null || percentage === undefined) {
    return null;
  }
  if (percentage >= 90) {
    return "A";
  }
  if (percentage >= 80) {
    return "B";
  }
  if (percentage >= 70) {
    return "C";
  }
  if (percentage >= 60) {
    return "D";
  }
  return "F";
}

function calculateGradeMetrics(score, maxScore) {
  if (
    score === null ||
    score === undefined ||
    maxScore === null ||
    maxScore === undefined ||
    Number.isNaN(score) ||
    Number.isNaN(maxScore) ||
    maxScore <= 0
  ) {
    return { percentage: null, letter: null };
  }

  const clampedScore = Math.max(0, Math.min(Number(score), Number(maxScore)));
  const rawPercentage = (clampedScore / Number(maxScore)) * 100;
  const percentage = Number(rawPercentage.toFixed(2));
  const letter = calculateLetterFromPercentage(percentage);

  return { percentage, letter };
}

async function listClassrooms({ includeArchived = false } = {}) {
  const classrooms = await prisma.classroom.findMany({
    where: includeArchived ? {} : { isArchived: false },
    include: {
      gradeLevel: true,
      homeroomTeacher: {
        include: { person: true },
      },
      _count: {
        select: { enrollments: true, subjects: true },
      },
    },
    orderBy: [{ isArchived: "asc" }, { name: "asc" }],
  });

  return classrooms.map((classroom) => ({
    id: classroom.id,
    name: classroom.name,
    code: classroom.code,
    description: classroom.description,
    gradeLevel: classroom.gradeLevel
      ? { id: classroom.gradeLevel.id, name: classroom.gradeLevel.name }
      : null,
    homeroomTeacher: classroom.homeroomTeacher
      ? {
          id: classroom.homeroomTeacher.id,
          name: buildPersonName(classroom.homeroomTeacher.person) || "Assigned teacher",
        }
      : null,
    startDate: classroom.startDate,
    endDate: classroom.endDate,
    isArchived: classroom.isArchived,
    studentCount: classroom._count.enrollments,
    subjectCount: classroom._count.subjects,
  }));
}

async function listSubjects({ includeArchived = false } = {}) {
  const subjects = await prisma.subject.findMany({
    where: includeArchived ? {} : { isArchived: false },
    include: {
      _count: { select: { classSubjects: true } },
    },
    orderBy: [{ isArchived: "asc" }, { name: "asc" }],
  });

  return subjects.map((subject) => ({
    id: subject.id,
    name: subject.name,
    code: subject.code,
    description: subject.description,
    isArchived: subject.isArchived,
    classCount: subject._count.classSubjects,
  }));
}

async function getClassManagementData() {
  const [classes, subjects, gradeLevels, teachers] = await Promise.all([
    listClassrooms({ includeArchived: true }),
    listSubjects({ includeArchived: true }),
    prisma.gradeLevel.findMany({
      where: { isActive: true },
      orderBy: { sortOrder: "asc" },
    }),
    prisma.teacherStaff.findMany({
      where: { employmentStatus: { isActive: true } },
      include: { person: true },
      orderBy: [{ person: { firstName: "asc" } }, { person: { lastName: "asc" } }],
    }),
  ]);

  return {
    classes,
    subjects,
    lookups: {
      gradeLevels: gradeLevels.map((grade) => ({
        id: grade.id,
        name: grade.name,
      })),
      teachers: teachers.map((teacher) => ({
        id: teacher.id,
        name: buildPersonName(teacher.person) || teacher.primarySubject || "Teacher",
      })),
    },
  };
}

async function createClassroom(payload) {
  const data = {
    name: payload.name,
    code: payload.code || null,
    description: payload.description || null,
    gradeLevelId: normalizeOptionalInt(payload.gradeLevelId),
    homeroomTeacherId: normalizeOptionalInt(payload.homeroomTeacherId),
    startDate: payload.startDate ? new Date(payload.startDate) : null,
    endDate: payload.endDate ? new Date(payload.endDate) : null,
  };

  return prisma.classroom.create({ data });
}

async function updateClassroom(id, payload) {
  const data = {
    name: payload.name,
    code: payload.code || null,
    description: payload.description || null,
    gradeLevelId: normalizeOptionalInt(payload.gradeLevelId),
    homeroomTeacherId: normalizeOptionalInt(payload.homeroomTeacherId),
    startDate: payload.startDate ? new Date(payload.startDate) : null,
    endDate: payload.endDate ? new Date(payload.endDate) : null,
  };

  return prisma.classroom.update({
    where: { id },
    data,
  });
}

async function setClassroomArchived(id, archived) {
  return prisma.classroom.update({
    where: { id },
    data: { isArchived: archived, updatedAt: new Date() },
  });
}

async function createSubject(payload) {
  return prisma.subject.create({
    data: {
      name: payload.name,
      code: payload.code || null,
      description: payload.description || null,
    },
  });
}

async function updateSubject(id, payload) {
  return prisma.subject.update({
    where: { id },
    data: {
      name: payload.name,
      code: payload.code || null,
      description: payload.description || null,
    },
  });
}

async function setSubjectArchived(id, archived) {
  return prisma.subject.update({
    where: { id },
    data: { isArchived: archived, updatedAt: new Date() },
  });
}

async function getClassDetail(classroomId) {
  const classroom = await prisma.classroom.findUnique({
    where: { id: classroomId },
    include: {
      gradeLevel: true,
      homeroomTeacher: { include: { person: true } },
      subjects: {
        include: {
          subject: true,
          teacher: { include: { person: true } },
        },
        orderBy: { subject: { name: "asc" } },
      },
      enrollments: {
        include: {
          student: {
            include: { person: true, gradeLevel: true },
          },
        },
      },
    },
  });

  if (!classroom) {
    return null;
  }

  classroom.enrollments.sort((a, b) => {
    const nameA = buildPersonName(a.student.person) || "";
    const nameB = buildPersonName(b.student.person) || "";
    return nameA.localeCompare(nameB);
  });

  const [subjects, teachers, assessmentTypes, gradeLevels] = await Promise.all([
    prisma.subject.findMany({
      where: { isArchived: false },
      orderBy: { name: "asc" },
    }),
    prisma.teacherStaff.findMany({
      where: { employmentStatus: { isActive: true } },
      include: { person: true },
      orderBy: [{ person: { firstName: "asc" } }, { person: { lastName: "asc" } }],
    }),
    prisma.assessmentType.findMany({
      where: { isArchived: false },
      orderBy: { name: "asc" },
    }),
    prisma.gradeLevel.findMany({
      where: { isActive: true },
      orderBy: { sortOrder: "asc" },
    }),
  ]);

  return {
    classroom: {
      id: classroom.id,
      name: classroom.name,
      code: classroom.code,
      description: classroom.description,
      gradeLevel: classroom.gradeLevel
        ? { id: classroom.gradeLevel.id, name: classroom.gradeLevel.name }
        : null,
      homeroomTeacher: classroom.homeroomTeacher
        ? {
            id: classroom.homeroomTeacher.id,
            name: buildPersonName(classroom.homeroomTeacher.person) || "Assigned teacher",
          }
        : null,
      startDate: classroom.startDate,
      endDate: classroom.endDate,
      isArchived: classroom.isArchived,
      subjects: classroom.subjects.map((assignment) => ({
        id: assignment.id,
        subjectId: assignment.subjectId,
        subjectName: assignment.subject.name,
        teacherId: assignment.teacherId,
        teacherName: assignment.teacher
          ? buildPersonName(assignment.teacher.person) || assignment.teacher.primarySubject || null
          : null,
        schedule: assignment.schedule,
      })),
      enrollments: classroom.enrollments.map((enrollment) => ({
        id: enrollment.id,
        studentId: enrollment.studentId,
        studentNumber: enrollment.student.studentNumber,
        name: buildPersonName(enrollment.student.person) || "Student",
        gradeLevel: enrollment.student.gradeLevel
          ? enrollment.student.gradeLevel.name
          : null,
        status: enrollment.status,
        enrolledAt: enrollment.enrolledAt,
        notes: enrollment.notes,
      })),
    },
    subjects: subjects.map((subject) => ({
      id: subject.id,
      name: subject.name,
      code: subject.code,
    })),
    teachers: teachers.map((teacher) => ({
      id: teacher.id,
      name: buildPersonName(teacher.person) || teacher.primarySubject || "Teacher",
    })),
    gradeLevels: gradeLevels.map((grade) => ({
      id: grade.id,
      name: grade.name,
    })),
    assessmentTypes: assessmentTypes.map((type) => ({
      id: type.id,
      name: type.name,
      weight: type.weight,
    })),
  };
}

async function assignSubjectToClassroom(classroomId, payload) {
  const subjectId = normalizeOptionalInt(payload.subjectId);
  if (!subjectId) {
    throw new Error("Subject is required");
  }

  const teacherId = normalizeOptionalInt(payload.teacherId);
  const schedule = payload.schedule ? payload.schedule.trim() : null;

  return prisma.classSubject.upsert({
    where: {
      classroomId_subjectId: {
        classroomId,
        subjectId,
      },
    },
    update: {
      teacherId: teacherId || null,
      schedule,
    },
    create: {
      classroomId,
      subjectId,
      teacherId: teacherId || null,
      schedule,
    },
    include: {
      subject: true,
      teacher: { include: { person: true } },
    },
  });
}

async function removeSubjectFromClassroom(classSubjectId) {
  return prisma.classSubject.delete({
    where: { id: classSubjectId },
  });
}

async function listAvailableStudents(classroomId, { search } = {}) {
  const enrolled = await prisma.enrollment.findMany({
    where: { classroomId },
    select: { studentId: true },
  });
  const excludedIds = enrolled.map((row) => row.studentId);

  const where = {
    id: { notIn: excludedIds },
  };

  if (search && search.trim().length > 0) {
    const term = search.trim();
    where.OR = [
      { studentNumber: { contains: term, mode: "insensitive" } },
      { person: { firstName: { contains: term, mode: "insensitive" } } },
      { person: { lastName: { contains: term, mode: "insensitive" } } },
    ];
  }

  const students = await prisma.student.findMany({
    where,
    include: { person: true, gradeLevel: true },
    take: 12,
    orderBy: [{ person: { firstName: "asc" } }, { person: { lastName: "asc" } }],
  });

  return students.map((student) => ({
    id: student.id,
    studentNumber: student.studentNumber,
    name: buildPersonName(student.person) || "Student",
    gradeLevel: student.gradeLevel ? student.gradeLevel.name : null,
    enrollmentStatusId: student.enrollmentStatusId,
  }));
}

async function addEnrollment(classroomId, payload) {
  const studentId = normalizeOptionalInt(payload.studentId);
  if (!studentId) {
    throw new Error("Student is required");
  }

  return prisma.enrollment.create({
    data: {
      classroomId,
      studentId,
      status: payload.status ? payload.status.trim() || "Active" : "Active",
      enrolledAt: payload.enrolledAt ? new Date(payload.enrolledAt) : null,
      notes: payload.notes ? payload.notes.trim() || null : null,
    },
    include: {
      student: { include: { person: true, gradeLevel: true } },
    },
  });
}

async function removeEnrollment(enrollmentId) {
  return prisma.enrollment.delete({
    where: { id: enrollmentId },
  });
}

async function getGradeEntryContext(classroomId, classSubjectId) {
  const classSubject = await prisma.classSubject.findUnique({
    where: { id: classSubjectId },
    include: {
      classroom: {
        include: {
          gradeLevel: true,
          homeroomTeacher: { include: { person: true } },
        },
      },
      subject: true,
      teacher: { include: { person: true } },
      assessments: {
        include: {
          assessmentType: true,
          grades: {
            include: {
              student: { include: { person: true } },
              enrollment: true,
            },
          },
        },
        orderBy: [{ dueDate: "asc" }, { createdAt: "asc" }],
      },
    },
  });

  if (!classSubject || classSubject.classroomId !== classroomId) {
    return null;
  }

  const enrollments = await prisma.enrollment.findMany({
    where: { classroomId },
    include: {
      student: { include: { person: true, gradeLevel: true } },
    },
    orderBy: [{ student: { person: { firstName: "asc" } } }, { student: { person: { lastName: "asc" } } }],
  });

  const assessmentTypes = await prisma.assessmentType.findMany({
    where: { isArchived: false },
    orderBy: { name: "asc" },
  });

  return {
    classSubject: {
      id: classSubject.id,
      classroomId: classSubject.classroomId,
      subjectId: classSubject.subjectId,
      schedule: classSubject.schedule,
      classroom: {
        id: classSubject.classroom.id,
        name: classSubject.classroom.name,
        gradeLevel: classSubject.classroom.gradeLevel
          ? classSubject.classroom.gradeLevel.name
          : null,
      },
      subject: {
        id: classSubject.subject.id,
        name: classSubject.subject.name,
      },
      teacher: classSubject.teacher
        ? {
            id: classSubject.teacher.id,
            name: buildPersonName(classSubject.teacher.person) || classSubject.teacher.primarySubject || "Teacher",
          }
        : null,
      assessments: classSubject.assessments.map((assessment) => ({
        id: assessment.id,
        title: assessment.title,
        description: assessment.description,
        dueDate: assessment.dueDate,
        maxScore: assessment.maxScore,
        assessmentType: {
          id: assessment.assessmentType.id,
          name: assessment.assessmentType.name,
        },
        grades: assessment.grades.map((grade) => ({
          id: grade.id,
          enrollmentId: grade.enrollmentId,
          studentId: grade.studentId,
          studentName: buildPersonName(grade.student.person) || "Student",
          studentNumber: grade.student.studentNumber,
          score: grade.score,
          maxScore: grade.maxScore ?? assessment.maxScore,
          percentage: grade.percentage,
          letterGrade: grade.letterGrade,
          comments: grade.comments,
        })),
      })),
    },
    enrollments: enrollments.map((enrollment) => ({
      id: enrollment.id,
      studentId: enrollment.studentId,
      studentNumber: enrollment.student.studentNumber,
      name: buildPersonName(enrollment.student.person) || "Student",
      gradeLevel: enrollment.student.gradeLevel
        ? enrollment.student.gradeLevel.name
        : null,
    })),
    assessmentTypes: assessmentTypes.map((type) => ({
      id: type.id,
      name: type.name,
      weight: type.weight,
    })),
  };
}

async function createAssessment(classSubjectId, payload) {
  const assessmentTypeId = normalizeOptionalInt(payload.assessmentTypeId);
  if (!assessmentTypeId) {
    throw new Error("Assessment type is required");
  }

  return prisma.assessment.create({
    data: {
      classSubjectId,
      assessmentTypeId,
      title: payload.title,
      description: payload.description ? payload.description.trim() || null : null,
      dueDate: payload.dueDate ? new Date(payload.dueDate) : null,
      maxScore: payload.maxScore !== undefined && payload.maxScore !== null
        ? Number(payload.maxScore)
        : null,
    },
    include: {
      assessmentType: true,
      grades: true,
    },
  });
}

async function saveGradeEntries({
  classSubjectId,
  assessmentId,
  entries,
  gradedById,
}) {
  const assessment = await prisma.assessment.findUnique({
    where: { id: assessmentId },
  });

  if (!assessment || assessment.classSubjectId !== classSubjectId) {
    throw new Error("Assessment not found for class");
  }

  const results = [];
  const now = new Date();

  for (const entry of entries) {
    const enrollmentId = normalizeOptionalInt(entry.enrollmentId);
    const studentId = normalizeOptionalInt(entry.studentId);
    if (!enrollmentId || !studentId) {
      // Skip invalid rows silently to keep UX forgiving
      // eslint-disable-next-line no-continue
      continue;
    }

    const score = entry.score === "" || entry.score === null || entry.score === undefined
      ? null
      : Number(entry.score);
    const maxScore = entry.maxScore !== undefined && entry.maxScore !== null && entry.maxScore !== ""
      ? Number(entry.maxScore)
      : assessment.maxScore;
    const metrics = calculateGradeMetrics(score, maxScore);

    const data = {
      enrollmentId,
      studentId,
      score,
      maxScore: maxScore ?? null,
      percentage: metrics.percentage,
      letterGrade: metrics.letter,
      gradedAt: score !== null ? now : null,
      gradedById: gradedById ? Number(gradedById) : null,
      comments: entry.comments ? entry.comments.trim() || null : null,
    };

    const saved = await prisma.gradeEntry.upsert({
      where: {
        assessmentId_studentId: {
          assessmentId,
          studentId,
        },
      },
      update: data,
      create: {
        assessmentId,
        ...data,
      },
      include: {
        student: { include: { person: true } },
        enrollment: true,
      },
    });

    results.push(saved);
  }

  return results;
}

async function getGradeReport({ classId, studentId } = {}) {
  const classroomFilter = classId ? { id: classId } : {};

  const classrooms = await prisma.classroom.findMany({
    where: classroomFilter,
    include: {
      gradeLevel: true,
      subjects: {
        include: {
          subject: true,
          assessments: {
            include: {
              assessmentType: true,
              grades: {
                include: {
                  student: { include: { person: true } },
                  enrollment: true,
                },
              },
            },
          },
        },
      },
      enrollments: {
        include: {
          student: { include: { person: true, gradeLevel: true } },
        },
      },
    },
    orderBy: { name: "asc" },
  });

  const studentFilterId = studentId ? Number(studentId) : null;

  const summaries = classrooms.map((classroom) => {
    const subjects = classroom.subjects.map((subject) => ({
      classSubjectId: subject.id,
      name: subject.subject.name,
    }));

    const studentMap = new Map();

    const ensureStudent = (studentRecord) => {
      const key = studentRecord.id;
      if (!studentMap.has(key)) {
        studentMap.set(key, {
          studentId: studentRecord.id,
          studentNumber: studentRecord.studentNumber,
          name: buildPersonName(studentRecord.person) || "Student",
          subjectGrades: {},
          overall: { total: 0, count: 0 },
        });
      }
      return studentMap.get(key);
    };

    for (const enrollment of classroom.enrollments) {
      ensureStudent(enrollment.student);
    }

    classroom.subjects.forEach((classSubject) => {
      classSubject.assessments.forEach((assessment) => {
        assessment.grades.forEach((grade) => {
          if (studentFilterId && grade.studentId !== studentFilterId) {
            return;
          }

          const studentRecord = grade.student;
          const student = ensureStudent(studentRecord);
          const subjectStats =
            student.subjectGrades[classSubject.id] || { total: 0, count: 0 };

          const percentage =
            grade.percentage !== null && grade.percentage !== undefined
              ? Number(grade.percentage)
              : calculateGradeMetrics(grade.score, grade.maxScore ?? assessment.maxScore)
                  .percentage;

          if (percentage !== null && percentage !== undefined && !Number.isNaN(percentage)) {
            subjectStats.total += percentage;
            subjectStats.count += 1;
            student.overall.total += percentage;
            student.overall.count += 1;
          }

          student.subjectGrades[classSubject.id] = subjectStats;
        });
      });
    });

    const students = Array.from(studentMap.values())
      .filter((summary) => {
        if (!studentFilterId) {
          return true;
        }
        return summary.studentId === studentFilterId;
      })
      .map((summary) => {
        const subjectGrades = {};
        Object.entries(summary.subjectGrades).forEach(([subjectId, stats]) => {
          if (stats.count === 0) {
            subjectGrades[subjectId] = { percentage: null, letterGrade: null };
            return;
          }
          const percentage = Number((stats.total / stats.count).toFixed(2));
          subjectGrades[subjectId] = {
            percentage,
            letterGrade: calculateLetterFromPercentage(percentage),
          };
        });

        let overallPercentage = null;
        let overallLetter = null;
        if (summary.overall.count > 0) {
          overallPercentage = Number((summary.overall.total / summary.overall.count).toFixed(2));
          overallLetter = calculateLetterFromPercentage(overallPercentage);
        }

        return {
          studentId: summary.studentId,
          studentNumber: summary.studentNumber,
          name: summary.name,
          subjectGrades,
          overall: {
            percentage: overallPercentage,
            letterGrade: overallLetter,
          },
        };
      })
      .sort((a, b) => a.name.localeCompare(b.name));

    return {
      classId: classroom.id,
      className: classroom.name,
      gradeLevel: classroom.gradeLevel ? classroom.gradeLevel.name : null,
      subjects,
      students,
    };
  });

  const availableClasses = classrooms.map((classroom) => ({
    id: classroom.id,
    name: classroom.name,
  }));

  const availableStudents = [];
  classrooms.forEach((classroom) => {
    classroom.enrollments.forEach((enrollment) => {
      const exists = availableStudents.some((student) => student.id === enrollment.student.id);
      if (!exists) {
        availableStudents.push({
          id: enrollment.student.id,
          name: buildPersonName(enrollment.student.person) || "Student",
        });
      }
    });
  });

  availableStudents.sort((a, b) => a.name.localeCompare(b.name));

  return {
    summaries,
    filters: {
      classes: availableClasses,
      students: availableStudents,
    },
  };
}

module.exports = {
  listClassrooms,
  listSubjects,
  getClassManagementData,
  createClassroom,
  updateClassroom,
  setClassroomArchived,
  createSubject,
  updateSubject,
  setSubjectArchived,
  getClassDetail,
  assignSubjectToClassroom,
  removeSubjectFromClassroom,
  listAvailableStudents,
  addEnrollment,
  removeEnrollment,
  getGradeEntryContext,
  createAssessment,
  saveGradeEntries,
  getGradeReport,
  calculateGradeMetrics,
};
