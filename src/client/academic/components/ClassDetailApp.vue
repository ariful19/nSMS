<template>
  <div class="academic-app">
    <section class="page-hero page-hero--subtle">
      <div class="page-hero__content">
        <p class="page-hero__eyebrow">Class overview</p>
        <h1 class="page-hero__title">{{ classroom.name }}</h1>
        <p class="page-hero__subtitle">
          Manage enrollments, subject coverage, and day-to-day notes for a comfortable, informed classroom.
        </p>
      </div>
      <dl class="page-hero__stats">
        <div class="page-hero__stat">
          <dt>Subjects assigned</dt>
          <dd>{{ subjectCount }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Students enrolled</dt>
          <dd>{{ enrollmentCount }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Grade level</dt>
          <dd>{{ gradeDisplay }}</dd>
        </div>
      </dl>
    </section>

    <div class="insight-row">
      <article class="insight-card">
        <p class="insight-card__label">Homeroom teacher</p>
        <p class="insight-card__value">{{ homeroomDisplay }}</p>
        <p class="insight-card__hint">
          Keep staff assignments current so guardians always see the right point of contact.
        </p>
      </article>
      <article class="insight-card">
        <p class="insight-card__label">Schedule</p>
        <p class="insight-card__value">{{ scheduleSummary }}</p>
        <p class="insight-card__hint">Use start and end dates to coordinate year planning and reporting windows.</p>
      </article>
      <article class="insight-card">
        <p class="insight-card__label">Status</p>
        <p class="insight-card__value">
          <span class="pill" :class="{ 'pill--soft': !classroom.isArchived, 'pill--success': !classroom.isArchived }">
            {{ classroom.isArchived ? "Archived" : "Active" }}
          </span>
        </p>
        <p class="insight-card__hint">Archived classes remain read-only while keeping historical data in reports.</p>
      </article>
    </div>

    <section class="card card--interactive">
      <header class="card__header">
        <div>
          <h2 class="card__title">Class information</h2>
          <p class="card__subtitle">Update high-level details to keep schedules and reporting aligned.</p>
        </div>
        <div class="card__actions">
          <span v-if="classroom.isArchived" class="badge badge--warning">Archived</span>
        </div>
      </header>

      <form class="form" @submit.prevent="saveClassroom" novalidate>
        <div v-if="classErrors.length" class="alert alert--danger">
          <p>Unable to update class:</p>
          <ul>
            <li v-for="error in classErrors" :key="error">{{ error }}</li>
          </ul>
        </div>
        <div class="grid grid--two-column">
          <label class="form__group">
            <span>Name</span>
            <input v-model.trim="classForm.name" type="text" required class="form__control" placeholder="eg. Grade 5 - Maple" />
          </label>
          <label class="form__group">
            <span>Code</span>
            <input v-model.trim="classForm.code" type="text" class="form__control" placeholder="Optional short code" />
          </label>
        </div>
        <div class="grid grid--two-column">
          <label class="form__group">
            <span>Grade level</span>
            <select v-model="classForm.gradeLevelId" class="form__control">
              <option value="">Select grade</option>
              <option v-for="grade in lookups.gradeLevels" :key="grade.id" :value="String(grade.id)">
                {{ grade.name }}
              </option>
            </select>
          </label>
          <label class="form__group">
            <span>Homeroom teacher</span>
            <select v-model="classForm.homeroomTeacherId" class="form__control">
              <option value="">Unassigned</option>
              <option v-for="teacher in lookups.teachers" :key="teacher.id" :value="String(teacher.id)">
                {{ teacher.name }}
              </option>
            </select>
          </label>
        </div>
        <div class="grid grid--two-column">
          <label class="form__group">
            <span>Start date</span>
            <input v-model="classForm.startDate" type="date" class="form__control" />
          </label>
          <label class="form__group">
            <span>End date</span>
            <input v-model="classForm.endDate" type="date" class="form__control" />
          </label>
        </div>
        <label class="form__group">
          <span>Description</span>
          <textarea
            v-model.trim="classForm.description"
            rows="2"
            class="form__control"
            placeholder="Share classroom notes, planning highlights, or scheduling nuances"
          ></textarea>
        </label>
        <div class="form__actions">
          <button type="submit" class="btn" :disabled="isSavingClass">Save changes</button>
          <button
            v-if="classroom.isArchived"
            type="button"
            class="btn btn--ghost"
            @click="restoreClass"
          >
            Restore class
          </button>
          <button
            v-else
            type="button"
            class="btn btn--danger"
            @click="archiveClass"
          >
            Archive class
          </button>
        </div>
      </form>
    </section>

    <section class="card card--interactive">
      <header class="card__header">
        <div>
          <h2 class="card__title">Subject assignments</h2>
          <p class="card__subtitle">Pair subjects with teachers to create a smooth, predictable timetable.</p>
        </div>
      </header>
      <form class="form" @submit.prevent="assignSubject" novalidate>
        <div v-if="subjectErrors.length" class="alert alert--danger">
          <p>Unable to assign subject:</p>
          <ul>
            <li v-for="error in subjectErrors" :key="error">{{ error }}</li>
          </ul>
        </div>
        <div class="grid grid--two-column">
          <label class="form__group">
            <span>Subject</span>
            <select v-model="subjectForm.subjectId" required class="form__control">
              <option value="">Select subject</option>
              <option v-for="subject in subjects" :key="subject.id" :value="String(subject.id)">
                {{ subject.name }}
              </option>
            </select>
          </label>
          <label class="form__group">
            <span>Teacher</span>
            <select v-model="subjectForm.teacherId" class="form__control">
              <option value="">Unassigned</option>
              <option v-for="teacher in lookups.teachers" :key="teacher.id" :value="String(teacher.id)">
                {{ teacher.name }}
              </option>
            </select>
          </label>
        </div>
        <label class="form__group">
          <span>Schedule notes</span>
          <input v-model.trim="subjectForm.schedule" type="text" class="form__control" placeholder="eg. Tue & Thu • 10:30" />
        </label>
        <div class="form__actions">
          <button type="submit" class="btn" :disabled="isSavingSubject">Save assignment</button>
        </div>
      </form>

      <div class="card__body">
        <div v-if="!classroom.subjects.length" class="empty-state">
          <p class="empty-state__title">No subjects assigned yet</p>
          <p>Add a subject above to begin mapping the learning plan for this class.</p>
        </div>

        <div v-else class="table-responsive">
          <table class="table">
            <thead>
              <tr>
                <th scope="col">Subject</th>
                <th scope="col">Teacher</th>
                <th scope="col">Schedule</th>
                <th scope="col">Actions</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="assignment in classroom.subjects" :key="assignment.id">
                <td data-title="Subject">{{ assignment.subjectName }}</td>
                <td data-title="Teacher">{{ assignment.teacherName ?? "Unassigned" }}</td>
                <td data-title="Schedule">{{ assignment.schedule ?? "—" }}</td>
                <td class="table__actions">
                  <a
                    class="btn btn--small btn--ghost"
                    :href="`/classes/${classroom.id}/subjects/${assignment.id}/grades`"
                  >
                    Grade entry
                  </a>
                  <button
                    type="button"
                    class="btn btn--small btn--danger"
                    @click="removeSubject(assignment)"
                  >
                    Remove
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </section>

    <section class="card card--interactive">
      <header class="card__header">
        <div>
          <h2 class="card__title">Enrollments</h2>
          <p class="card__subtitle">Search for students and welcome them into this class roster.</p>
        </div>
      </header>
      <div class="card__body">
        <label class="form__group">
          <span>Search students</span>
          <input
            v-model.trim="studentSearch"
            type="search"
            class="form__control"
            placeholder="Type a name or student number"
            @input="queueStudentSearch"
          />
        </label>
        <div v-if="enrollmentErrors.length" class="alert alert--danger">
          <p>Unable to update enrollments:</p>
          <ul>
            <li v-for="error in enrollmentErrors" :key="error">{{ error }}</li>
          </ul>
        </div>
        <p v-if="isSearching" class="muted">Searching students…</p>
        <p
          v-if="studentSearch && !isSearching && searchResults.length === 0"
          class="muted"
        >
          No students match your search or they are already enrolled.
        </p>
        <ul v-if="searchResults.length" class="list list--compact">
          <li v-for="student in searchResults" :key="student.id" class="list__option">
            <div>
              <strong>{{ student.name }}</strong>
              <span v-if="student.studentNumber" class="badge badge--neutral">
                {{ student.studentNumber }}
              </span>
            </div>
            <button type="button" class="btn btn--small" @click="addEnrollment(student)">
              Add to class
            </button>
          </li>
        </ul>
      </div>
      <div class="card__body">
        <div v-if="!classroom.enrollments.length" class="empty-state">
          <p class="empty-state__title">No students enrolled yet</p>
          <p>Invite students above to build the roster and unlock grade entry.</p>
        </div>
        <div v-else class="table-responsive">
          <table class="table">
            <thead>
              <tr>
                <th scope="col">Student</th>
                <th scope="col">Grade</th>
                <th scope="col">Status</th>
                <th scope="col">Actions</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="enrollment in classroom.enrollments" :key="enrollment.id">
                <td data-title="Student">
                  {{ enrollment.name }}
                  <span v-if="enrollment.studentNumber" class="badge badge--neutral">
                    {{ enrollment.studentNumber }}
                  </span>
                </td>
                <td data-title="Grade">{{ enrollment.gradeLevel ?? "—" }}</td>
                <td data-title="Status">{{ enrollment.status ?? "Active" }}</td>
                <td class="table__actions">
                  <button
                    type="button"
                    class="btn btn--small btn--danger"
                    @click="removeEnrollment(enrollment)"
                  >
                    Remove
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </section>
  </div>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  detail: { type: Object, default: () => ({}) },
  csrfToken: { type: String, default: "" },
});

const classroom = reactive({ ...(props.detail?.classroom ?? {}) });
const subjects = ref([...(props.detail?.subjects ?? [])]);
const lookups = reactive({
  teachers: props.detail?.teachers ?? [],
  gradeLevels: props.detail?.gradeLevels ?? [],
});
const assessmentTypes = ref([...(props.detail?.assessmentTypes ?? [])]);

const classForm = reactive({
  name: classroom.name || "",
  code: classroom.code || "",
  gradeLevelId: classroom.gradeLevel ? String(classroom.gradeLevel.id) : "",
  homeroomTeacherId: classroom.homeroomTeacher ? String(classroom.homeroomTeacher.id) : "",
  startDate: classroom.startDate ? classroom.startDate.slice(0, 10) : "",
  endDate: classroom.endDate ? classroom.endDate.slice(0, 10) : "",
  description: classroom.description || "",
});

const classErrors = ref([]);
const isSavingClass = ref(false);
const subjectErrors = ref([]);
const subjectForm = reactive({ subjectId: "", teacherId: "", schedule: "" });
const isSavingSubject = ref(false);

const studentSearch = ref("");
const searchResults = ref([]);
const isSearching = ref(false);
const enrollmentErrors = ref([]);
let searchTimeout;

const classroomId = computed(() => classroom.id);
const subjectCount = computed(() => classroom.subjects?.length ?? 0);
const enrollmentCount = computed(() => classroom.enrollments?.length ?? 0);
const gradeDisplay = computed(() => classroom.gradeLevel?.name ?? "Unassigned");
const homeroomDisplay = computed(
  () => classroom.homeroomTeacher?.name ?? "No homeroom assigned",
);

function formatDate(value) {
  if (!value) {
    return null;
  }
  const parsed = new Date(value);
  if (Number.isNaN(parsed.getTime())) {
    return null;
  }
  return parsed.toLocaleDateString(undefined, {
    year: "numeric",
    month: "short",
    day: "numeric",
  });
}

const scheduleSummary = computed(() => {
  const start = formatDate(classroom.startDate);
  const end = formatDate(classroom.endDate);
  if (start && end) {
    return `${start} → ${end}`;
  }
  if (start) {
    return `Starts ${start}`;
  }
  if (end) {
    return `Ends ${end}`;
  }
  return "Flexible schedule";
});

async function refreshDetail() {
  const response = await fetch(`/classes/${classroomId.value}`, {
    headers: { Accept: "application/json" },
  });
  if (!response.ok) {
    throw new Error("Failed to refresh class");
  }
  const data = await response.json();
  const detail = data.detail;
  Object.assign(classroom, detail.classroom);
  subjects.value = detail.subjects;
  if (Array.isArray(detail.teachers)) {
    lookups.teachers = detail.teachers;
  }
  if (Array.isArray(detail.gradeLevels)) {
    lookups.gradeLevels = detail.gradeLevels;
  }
  if (detail.classroom.gradeLevel) {
    classForm.gradeLevelId = String(detail.classroom.gradeLevel.id);
  } else {
    classForm.gradeLevelId = "";
  }
  classForm.name = detail.classroom.name || "";
  classForm.code = detail.classroom.code || "";
  classForm.homeroomTeacherId = detail.classroom.homeroomTeacher
    ? String(detail.classroom.homeroomTeacher.id)
    : "";
  classForm.startDate = detail.classroom.startDate
    ? detail.classroom.startDate.slice(0, 10)
    : "";
  classForm.endDate = detail.classroom.endDate
    ? detail.classroom.endDate.slice(0, 10)
    : "";
  classForm.description = detail.classroom.description || "";
  if (Array.isArray(detail.assessmentTypes)) {
    assessmentTypes.value = detail.assessmentTypes;
  }
}

async function saveClassroom() {
  classErrors.value = [];
  if (!classForm.name) {
    classErrors.value.push("Class name is required.");
    return;
  }
  isSavingClass.value = true;
  try {
    const response = await fetch(`/classes/${classroomId.value}/update`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify(classForm),
    });
    const data = await response.json().catch(() => ({}));
    if (!response.ok) {
      classErrors.value = data.errors || [data.message || "Unable to update class."];
      return;
    }
    await refreshDetail();
  } catch (error) {
    classErrors.value = [error.message || "Unexpected error updating class."];
  } finally {
    isSavingClass.value = false;
  }
}

async function archiveClass() {
  await mutateClass(`/classes/${classroomId.value}/archive`);
}

async function restoreClass() {
  await mutateClass(`/classes/${classroomId.value}/restore`);
}

async function mutateClass(url) {
  classErrors.value = [];
  try {
    const response = await fetch(url, {
      method: "POST",
      headers: {
        Accept: "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
    });
    if (!response.ok) {
      const data = await response.json().catch(() => ({}));
      throw new Error(data.message || "Unable to update class");
    }
    await refreshDetail();
  } catch (error) {
    classErrors.value = [error.message || "Unable to update class."];
  }
}

async function assignSubject() {
  subjectErrors.value = [];
  if (!subjectForm.subjectId) {
    subjectErrors.value.push("Subject is required.");
    return;
  }
  isSavingSubject.value = true;
  try {
    const response = await fetch(`/classes/${classroomId.value}/subjects`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify(subjectForm),
    });
    const data = await response.json().catch(() => ({}));
    if (!response.ok) {
      subjectErrors.value = data.errors || [data.message || "Unable to assign subject."];
      return;
    }
    subjectForm.subjectId = "";
    subjectForm.teacherId = "";
    subjectForm.schedule = "";
    await refreshDetail();
  } catch (error) {
    subjectErrors.value = [error.message || "Unable to assign subject."];
  } finally {
    isSavingSubject.value = false;
  }
}

async function removeSubject(assignment) {
  subjectErrors.value = [];
  try {
    const response = await fetch(
      `/classes/${classroomId.value}/subjects/${assignment.id}/delete`,
      {
        method: "POST",
        headers: {
          Accept: "application/json",
          "X-CSRF-Token": props.csrfToken,
        },
      },
    );
    if (!response.ok) {
      const data = await response.json().catch(() => ({}));
      throw new Error(data.message || "Unable to remove subject.");
    }
    await refreshDetail();
  } catch (error) {
    subjectErrors.value = [error.message || "Unable to remove subject."];
  }
}

function queueStudentSearch() {
  clearTimeout(searchTimeout);
  if (!studentSearch.value) {
    searchResults.value = [];
    return;
  }
  searchTimeout = setTimeout(runStudentSearch, 250);
}

async function runStudentSearch() {
  isSearching.value = true;
  try {
    const params = new URLSearchParams({ search: studentSearch.value });
    const response = await fetch(
      `/classes/${classroomId.value}/enrollment/search?${params.toString()}`,
      {
        headers: { Accept: "application/json" },
      },
    );
    if (!response.ok) {
      throw new Error("Search failed");
    }
    const data = await response.json();
    searchResults.value = data.students || [];
  } catch (error) {
    searchResults.value = [];
  } finally {
    isSearching.value = false;
  }
}

async function addEnrollment(student) {
  enrollmentErrors.value = [];
  try {
    const response = await fetch(`/classes/${classroomId.value}/enrollments`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify({ studentId: student.id }),
    });
    const data = await response.json().catch(() => ({}));
    if (!response.ok) {
      throw new Error(data.message || "Unable to add student");
    }
    studentSearch.value = "";
    searchResults.value = [];
    await refreshDetail();
  } catch (error) {
    enrollmentErrors.value = [error.message || "Unable to add student."];
  }
}

async function removeEnrollment(enrollment) {
  enrollmentErrors.value = [];
  try {
    const response = await fetch(
      `/classes/${classroomId.value}/enrollments/${enrollment.id}/delete`,
      {
        method: "POST",
        headers: {
          Accept: "application/json",
          "X-CSRF-Token": props.csrfToken,
        },
      },
    );
    if (!response.ok) {
      const data = await response.json().catch(() => ({}));
      throw new Error(data.message || "Unable to remove student");
    }
    await refreshDetail();
  } catch (error) {
    enrollmentErrors.value = [error.message || "Unable to remove student."];
  }
}
</script>
