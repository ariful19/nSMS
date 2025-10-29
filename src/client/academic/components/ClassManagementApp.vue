<template>
  <div class="academic-app">
    <section class="page-hero page-hero--compact">
      <div class="page-hero__content">
        <p class="page-hero__eyebrow">Academic planning</p>
        <h1 class="page-hero__title">Classes &amp; Subjects</h1>
        <p class="page-hero__subtitle">
          Create calm class rosters and curate your subject catalogue with confident clarity.
        </p>
      </div>
      <dl class="page-hero__stats">
        <div class="page-hero__stat">
          <dt>Active classes</dt>
          <dd>{{ activeClassCount }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Students managed</dt>
          <dd>{{ totalStudents }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Subjects available</dt>
          <dd>{{ activeSubjectCount }}</dd>
        </div>
      </dl>
    </section>

    <section class="grid grid--two-column">
      <article class="card card--interactive">
        <header class="card__header">
          <div>
            <h2 class="card__title">Classes</h2>
            <p class="card__subtitle">
              {{ filteredClasses.length }} of {{ classes.length }} class{{ classes.length === 1 ? '' : 'es' }} in view
            </p>
          </div>
          <div class="card__actions">
            <div class="toolbar">
              <div class="toolbar__search">
                <input
                  v-model="classSearch"
                  type="search"
                  class="form__control"
                  placeholder="Search by name, code, grade or homeroom teacher"
                />
              </div>
              <div class="toolbar__actions">
                <label class="toggle">
                  <input type="checkbox" v-model="showArchivedClasses" />
                  <span>Show archived</span>
                </label>
              </div>
            </div>
          </div>
        </header>

        <form class="form" @submit.prevent="submitClassForm" novalidate>
          <div v-if="classErrors.length" class="alert alert--danger">
            <p>Unable to save the class:</p>
            <ul>
              <li v-for="error in classErrors" :key="error">{{ error }}</li>
            </ul>
          </div>

          <label class="form__group">
            <span>Name</span>
            <input v-model.trim="classForm.name" type="text" required class="form__control" placeholder="eg. Grade 5 - Maple" />
          </label>
          <label class="form__group">
            <span>Code</span>
            <input v-model.trim="classForm.code" type="text" class="form__control" placeholder="Optional short code" />
          </label>
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
          <div class="form__row">
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
            <textarea v-model.trim="classForm.description" rows="2" class="form__control" placeholder="Share the class focus or schedule"></textarea>
          </label>
          <div class="form__actions">
            <button type="submit" class="btn" :disabled="isSavingClass">
              {{ classEditingId ? "Update class" : "Create class" }}
            </button>
            <button v-if="classEditingId" type="button" class="btn btn--ghost" @click="resetClassForm">
              Cancel edit
            </button>
            <button
              v-if="isClassFilterActive"
              type="button"
              class="btn btn--ghost btn--small"
              @click="resetClassFilters"
            >
              Clear filters
            </button>
          </div>
        </form>

        <div class="card__body">
          <div v-if="!filteredClasses.length" class="empty-state">
            <p class="empty-state__title">No classes match this view</p>
            <p v-if="isClassFilterActive">
              Adjust the search or show archived records to discover more classes.
            </p>
            <p v-else>Add your first class to begin organising enrollments.</p>
          </div>

          <div v-else class="table-responsive">
            <table class="table">
              <thead>
                <tr>
                  <th scope="col">Name</th>
                  <th scope="col">Grade</th>
                  <th scope="col">Students</th>
                  <th scope="col">Subjects</th>
                  <th scope="col">Actions</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="classroom in filteredClasses" :key="classroom.id">
                  <td data-title="Name">
                    <div class="table__title">
                      <a :href="`/classes/${classroom.id}`">{{ classroom.name }}</a>
                    </div>
                    <div class="table__meta">
                      <span v-if="classroom.homeroomTeacher" class="pill pill--soft">
                        Homeroom • {{ classroom.homeroomTeacher.name }}
                      </span>
                      <span v-if="classroom.code" class="pill pill--soft">Code • {{ classroom.code }}</span>
                      <span v-if="classroom.isArchived" class="badge badge--warning">Archived</span>
                    </div>
                  </td>
                  <td data-title="Grade">
                    {{ classroom.gradeLevel?.name ?? "—" }}
                  </td>
                  <td data-title="Students">{{ classroom.studentCount }}</td>
                  <td data-title="Subjects">{{ classroom.subjectCount }}</td>
                  <td class="table__actions">
                    <button type="button" class="btn btn--small btn--ghost" @click="beginClassEdit(classroom)">
                      Edit
                    </button>
                    <a class="btn btn--small" :href="`/classes/${classroom.id}`">Open</a>
                    <button
                      v-if="!classroom.isArchived"
                      type="button"
                      class="btn btn--small btn--danger"
                      @click="archiveClass(classroom)"
                    >
                      Archive
                    </button>
                    <button
                      v-else
                      type="button"
                      class="btn btn--small"
                      @click="restoreClass(classroom)"
                    >
                      Restore
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </article>

      <article class="card card--interactive">
        <header class="card__header">
          <div>
            <h2 class="card__title">Subjects</h2>
            <p class="card__subtitle">
              {{ visibleSubjects.length }} of {{ subjects.length }} subject{{ subjects.length === 1 ? '' : 's' }} in view
            </p>
          </div>
          <div class="card__actions">
            <div class="toolbar">
              <div class="toolbar__search">
                <input
                  v-model="subjectSearch"
                  type="search"
                  class="form__control"
                  placeholder="Search subjects by name or code"
                />
              </div>
              <div class="toolbar__actions">
                <label class="toggle">
                  <input type="checkbox" v-model="showArchivedSubjects" />
                  <span>Show archived</span>
                </label>
              </div>
            </div>
          </div>
        </header>

        <form class="form" @submit.prevent="submitSubjectForm" novalidate>
          <div v-if="subjectErrors.length" class="alert alert--danger">
            <p>Unable to save the subject:</p>
            <ul>
              <li v-for="error in subjectErrors" :key="error">{{ error }}</li>
            </ul>
          </div>
          <label class="form__group">
            <span>Name</span>
            <input v-model.trim="subjectForm.name" type="text" required class="form__control" placeholder="eg. Mathematics" />
          </label>
          <label class="form__group">
            <span>Code</span>
            <input v-model.trim="subjectForm.code" type="text" class="form__control" placeholder="Optional short code" />
          </label>
          <label class="form__group">
            <span>Description</span>
            <textarea v-model.trim="subjectForm.description" rows="2" class="form__control" placeholder="Add context or curriculum notes"></textarea>
          </label>
          <div class="form__actions">
            <button type="submit" class="btn" :disabled="isSavingSubject">
              {{ subjectEditingId ? "Update subject" : "Create subject" }}
            </button>
            <button v-if="subjectEditingId" type="button" class="btn btn--ghost" @click="resetSubjectForm">
              Cancel edit
            </button>
            <button
              v-if="isSubjectFilterActive"
              type="button"
              class="btn btn--ghost btn--small"
              @click="resetSubjectFilters"
            >
              Clear filters
            </button>
          </div>
        </form>

        <div class="card__body">
          <div v-if="!visibleSubjects.length" class="empty-state">
            <p class="empty-state__title">No subjects match this view</p>
            <p v-if="isSubjectFilterActive">
              Refine your search or reveal archived subjects to broaden the catalogue.
            </p>
            <p v-else>Capture your first subject to build the curriculum.</p>
          </div>

          <ul v-else class="list">
            <li v-for="subject in visibleSubjects" :key="subject.id">
              <div class="list__heading">
                <strong>{{ subject.name }}</strong>
                <span v-if="subject.code" class="badge badge--neutral">{{ subject.code }}</span>
                <span v-if="subject.isArchived" class="badge badge--warning">Archived</span>
              </div>
              <p v-if="subject.description" class="list__description">
                {{ subject.description }}
              </p>
              <div class="list__meta">
                <span>Linked classes: {{ subject.classCount }}</span>
              </div>
              <div class="list__actions">
                <button type="button" class="btn btn--small btn--ghost" @click="beginSubjectEdit(subject)">
                  Edit
                </button>
                <button
                  v-if="!subject.isArchived"
                  type="button"
                  class="btn btn--small btn--danger"
                  @click="archiveSubject(subject)"
                >
                  Archive
                </button>
                <button
                  v-else
                  type="button"
                  class="btn btn--small"
                  @click="restoreSubject(subject)"
                >
                  Restore
                </button>
              </div>
            </li>
          </ul>
        </div>
      </article>
    </section>
  </div>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  classes: { type: Array, default: () => [] },
  subjects: { type: Array, default: () => [] },
  lookups: {
    type: Object,
    default: () => ({ gradeLevels: [], teachers: [] }),
  },
  csrfToken: { type: String, default: "" },
});

const classes = ref([...props.classes]);
const subjects = ref([...props.subjects]);
const lookups = reactive({
  gradeLevels: props.lookups?.gradeLevels ?? [],
  teachers: props.lookups?.teachers ?? [],
});

const showArchivedClasses = ref(false);
const showArchivedSubjects = ref(false);
const classSearch = ref("");
const subjectSearch = ref("");

const classForm = reactive({
  name: "",
  code: "",
  gradeLevelId: "",
  homeroomTeacherId: "",
  startDate: "",
  endDate: "",
  description: "",
});
const classErrors = ref([]);
const classEditingId = ref(null);
const isSavingClass = ref(false);

const subjectForm = reactive({
  name: "",
  code: "",
  description: "",
});
const subjectErrors = ref([]);
const subjectEditingId = ref(null);
const isSavingSubject = ref(false);

const classSearchTerm = computed(() => classSearch.value.trim().toLowerCase());
const subjectSearchTerm = computed(() => subjectSearch.value.trim().toLowerCase());

const filteredClasses = computed(() => {
  return classes.value
    .filter((classroom) => showArchivedClasses.value || !classroom.isArchived)
    .filter((classroom) => {
      if (!classSearchTerm.value) {
        return true;
      }
      const haystack = [
        classroom.name,
        classroom.code,
        classroom.gradeLevel?.name,
        classroom.homeroomTeacher?.name,
      ]
        .filter(Boolean)
        .join(" ")
        .toLowerCase();
      return haystack.includes(classSearchTerm.value);
    });
});

const visibleSubjects = computed(() => {
  return subjects.value
    .filter((subject) => showArchivedSubjects.value || !subject.isArchived)
    .filter((subject) => {
      if (!subjectSearchTerm.value) {
        return true;
      }
      const haystack = [subject.name, subject.code]
        .filter(Boolean)
        .join(" ")
        .toLowerCase();
      return haystack.includes(subjectSearchTerm.value);
    });
});

const isClassFilterActive = computed(
  () => !!classSearchTerm.value || showArchivedClasses.value,
);
const isSubjectFilterActive = computed(
  () => !!subjectSearchTerm.value || showArchivedSubjects.value,
);

const activeClassCount = computed(
  () => classes.value.filter((classroom) => !classroom.isArchived).length,
);
const totalStudents = computed(() =>
  classes.value.reduce((sum, classroom) => sum + (classroom.studentCount ?? 0), 0),
);
const activeSubjectCount = computed(
  () => subjects.value.filter((subject) => !subject.isArchived).length,
);

function resetClassForm() {
  classForm.name = "";
  classForm.code = "";
  classForm.gradeLevelId = "";
  classForm.homeroomTeacherId = "";
  classForm.startDate = "";
  classForm.endDate = "";
  classForm.description = "";
  classEditingId.value = null;
  classErrors.value = [];
}

function resetSubjectForm() {
  subjectForm.name = "";
  subjectForm.code = "";
  subjectForm.description = "";
  subjectEditingId.value = null;
  subjectErrors.value = [];
}

function resetClassFilters() {
  classSearch.value = "";
  showArchivedClasses.value = false;
}

function resetSubjectFilters() {
  subjectSearch.value = "";
  showArchivedSubjects.value = false;
}

function beginClassEdit(classroom) {
  classForm.name = classroom.name || "";
  classForm.code = classroom.code || "";
  classForm.gradeLevelId = classroom.gradeLevel ? String(classroom.gradeLevel.id) : "";
  classForm.homeroomTeacherId = classroom.homeroomTeacher
    ? String(classroom.homeroomTeacher.id)
    : "";
  classForm.startDate = classroom.startDate ? classroom.startDate.slice(0, 10) : "";
  classForm.endDate = classroom.endDate ? classroom.endDate.slice(0, 10) : "";
  classForm.description = classroom.description || "";
  classEditingId.value = classroom.id;
  classErrors.value = [];
}

function beginSubjectEdit(subject) {
  subjectForm.name = subject.name || "";
  subjectForm.code = subject.code || "";
  subjectForm.description = subject.description || "";
  subjectEditingId.value = subject.id;
  subjectErrors.value = [];
}

async function refreshManagementState() {
  const response = await fetch("/classes", {
    headers: { Accept: "application/json" },
  });
  if (!response.ok) {
    throw new Error("Failed to refresh data");
  }
  const data = await response.json();
  classes.value = data.management.classes || [];
  subjects.value = data.management.subjects || [];
  if (data.management.lookups) {
    lookups.gradeLevels = data.management.lookups.gradeLevels || lookups.gradeLevels;
    lookups.teachers = data.management.lookups.teachers || lookups.teachers;
  }
}

async function submitClassForm() {
  classErrors.value = [];
  if (!classForm.name) {
    classErrors.value.push("Class name is required.");
    return;
  }

  isSavingClass.value = true;
  try {
    const url = classEditingId.value
      ? `/classes/${classEditingId.value}/update`
      : "/classes";
    const response = await fetch(url, {
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
      classErrors.value = data.errors || [data.message || "Unable to save class."];
      return;
    }
    await refreshManagementState();
    resetClassForm();
  } catch (error) {
    classErrors.value = [error.message || "Unexpected error saving class."];
  } finally {
    isSavingClass.value = false;
  }
}

async function archiveClass(classroom) {
  await mutateClass(`/classes/${classroom.id}/archive`);
}

async function restoreClass(classroom) {
  await mutateClass(`/classes/${classroom.id}/restore`);
}

async function mutateClass(url) {
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
    await refreshManagementState();
  } catch (error) {
    classErrors.value = [error.message || "Unable to update class."];
  }
}

async function submitSubjectForm() {
  subjectErrors.value = [];
  if (!subjectForm.name) {
    subjectErrors.value.push("Subject name is required.");
    return;
  }

  isSavingSubject.value = true;
  try {
    const url = subjectEditingId.value
      ? `/classes/subjects/${subjectEditingId.value}/update`
      : "/classes/subjects";
    const response = await fetch(url, {
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
      subjectErrors.value = data.errors || [data.message || "Unable to save subject."];
      return;
    }
    await refreshManagementState();
    resetSubjectForm();
  } catch (error) {
    subjectErrors.value = [error.message || "Unexpected error saving subject."];
  } finally {
    isSavingSubject.value = false;
  }
}

async function archiveSubject(subject) {
  await mutateSubject(`/classes/subjects/${subject.id}/archive`);
}

async function restoreSubject(subject) {
  await mutateSubject(`/classes/subjects/${subject.id}/restore`);
}

async function mutateSubject(url) {
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
      throw new Error(data.message || "Unable to update subject");
    }
    await refreshManagementState();
  } catch (error) {
    subjectErrors.value = [error.message || "Unable to update subject."];
  }
}
</script>
