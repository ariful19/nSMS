<template>
  <div class="academic-app">
    <section class="page-hero page-hero--subtle">
      <div class="page-hero__content">
        <p class="page-hero__eyebrow">Grade workspace</p>
        <h1 class="page-hero__title">
          {{ classSubject.classroom.name }} • {{ classSubject.subject.name }}
        </h1>
        <p class="page-hero__subtitle">
          Capture grades with clarity, track assessment progress, and keep every student supported.
        </p>
      </div>
      <dl class="page-hero__stats">
        <div class="page-hero__stat">
          <dt>Enrolled students</dt>
          <dd>{{ enrollments.length }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Assessments</dt>
          <dd>{{ classSubject.assessments.length }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Instructor</dt>
          <dd>{{ teacherDisplay }}</dd>
        </div>
      </dl>
    </section>

    <section class="card card--interactive">
      <header class="card__header">
        <div>
          <h2 class="card__title">Assessments</h2>
          <p class="card__subtitle">Choose an assessment to review or add a new experience below.</p>
        </div>
      </header>
      <div class="card__body">
        <div class="toolbar">
          <div class="toolbar__search">
            <label class="form__group">
              <span>Assessment</span>
              <select
                v-model="selectedAssessmentId"
                class="form__control"
                @change="applySelectedAssessment"
              >
                <option v-if="classSubject.assessments.length === 0" value="">
                  Create an assessment to begin
                </option>
                <option
                  v-for="assessment in classSubject.assessments"
                  :key="assessment.id"
                  :value="String(assessment.id)"
                >
                  {{ assessment.title }}
                </option>
              </select>
            </label>
          </div>
          <div class="toolbar__actions">
            <span v-if="classSubject.schedule" class="pill pill--soft">
              {{ classSubject.schedule }}
            </span>
          </div>
        </div>

        <div v-if="selectedAssessment" class="insight-row">
          <article class="insight-card">
            <p class="insight-card__label">Assessment type</p>
            <p class="insight-card__value">
              {{ selectedAssessment.assessmentType?.name ?? "Not set" }}
            </p>
            <p class="insight-card__hint">Consistent types keep reporting balanced.</p>
          </article>
          <article class="insight-card">
            <p class="insight-card__label">Due date</p>
            <p class="insight-card__value">{{ formatDueDate(selectedAssessment.dueDate) }}</p>
            <p class="insight-card__hint">Schedule clearly signals expectations to students.</p>
          </article>
          <article class="insight-card">
            <p class="insight-card__label">Default max score</p>
            <p class="insight-card__value">
              {{ selectedAssessment.maxScore != null ? selectedAssessment.maxScore : "Flexible" }}
            </p>
            <p class="insight-card__hint">You can customise per student in the grid below.</p>
          </article>
        </div>
      </div>

      <form class="form form--inline" @submit.prevent="createAssessment" novalidate>
        <fieldset>
          <legend>Add assessment</legend>
          <div v-if="assessmentErrors.length" class="alert alert--danger">
            <p>Unable to create assessment:</p>
            <ul>
              <li v-for="error in assessmentErrors" :key="error">{{ error }}</li>
            </ul>
          </div>
          <div class="grid grid--three-column">
            <label class="form__group">
              <span>Title</span>
              <input v-model.trim="assessmentForm.title" type="text" required class="form__control" placeholder="eg. Module reflection" />
            </label>
            <label class="form__group">
              <span>Type</span>
              <select v-model="assessmentForm.assessmentTypeId" required class="form__control">
                <option value="">Select type</option>
                <option v-for="type in assessmentTypes" :key="type.id" :value="String(type.id)">
                  {{ type.name }}
                </option>
              </select>
            </label>
            <label class="form__group">
              <span>Due date</span>
              <input v-model="assessmentForm.dueDate" type="date" class="form__control" />
            </label>
          </div>
          <div class="grid grid--two-column">
            <label class="form__group">
              <span>Max score</span>
              <input v-model="assessmentForm.maxScore" type="number" step="0.01" class="form__control" />
            </label>
            <label class="form__group">
              <span>Description</span>
              <input v-model.trim="assessmentForm.description" type="text" class="form__control" placeholder="Optional context" />
            </label>
          </div>
          <div class="form__actions">
            <button type="submit" class="btn" :disabled="isCreatingAssessment">Add assessment</button>
          </div>
        </fieldset>
      </form>
    </section>

    <section class="card card--interactive">
      <header class="card__header">
        <div>
          <h2 class="card__title">Grade entry</h2>
          <p class="card__subtitle">
            Enter scores, adjust maximums, and leave caring comments for each learner.
          </p>
        </div>
      </header>
      <div class="card__body">
        <div v-if="!selectedAssessment" class="empty-state">
          <p class="empty-state__title">No assessment selected</p>
          <p>Create an assessment above or choose one to begin recording grades.</p>
        </div>
        <form v-else class="stack" @submit.prevent="saveGrades" novalidate>
          <div v-if="gradeErrors.length" class="alert alert--danger">
            <p>Unable to save grades:</p>
            <ul>
              <li v-for="error in gradeErrors" :key="error">{{ error }}</li>
            </ul>
          </div>
          <div class="table-responsive">
            <table class="table">
              <thead>
                <tr>
                  <th scope="col">Student</th>
                  <th scope="col">Score</th>
                  <th scope="col">Max</th>
                  <th scope="col">Percentage</th>
                  <th scope="col">Letter</th>
                  <th scope="col">Comments</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="enrollment in enrollments" :key="enrollment.id">
                  <td data-title="Student">
                    <strong>{{ enrollment.name }}</strong>
                    <span v-if="enrollment.studentNumber" class="badge badge--neutral">
                      {{ enrollment.studentNumber }}
                    </span>
                  </td>
                  <td data-title="Score">
                    <input
                      v-model.number="gradeEntries[enrollment.studentId].score"
                      type="number"
                      step="0.01"
                      class="form__control"
                      @input="recalculate(enrollment.studentId)"
                    />
                  </td>
                  <td data-title="Max">
                    <input
                      v-model.number="gradeEntries[enrollment.studentId].maxScore"
                      type="number"
                      step="0.01"
                      class="form__control"
                      @input="recalculate(enrollment.studentId)"
                    />
                  </td>
                  <td data-title="Percentage">
                    {{ formatPercentage(gradeEntries[enrollment.studentId].percentage) }}
                  </td>
                  <td data-title="Letter">
                    {{ gradeEntries[enrollment.studentId].letter || "—" }}
                  </td>
                  <td data-title="Comments">
                    <input
                      v-model.trim="gradeEntries[enrollment.studentId].comments"
                      type="text"
                      class="form__control"
                      placeholder="Encouragement or feedback"
                    />
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
          <div class="form__actions">
            <button type="submit" class="btn" :disabled="isSavingGrades">Save grades</button>
          </div>
        </form>
      </div>
    </section>
  </div>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  classSubject: { type: Object, default: () => ({}) },
  enrollments: { type: Array, default: () => [] },
  assessmentTypes: { type: Array, default: () => [] },
  csrfToken: { type: String, default: "" },
});

const classSubject = reactive({ ...(props.classSubject ?? {}) });
if (!Array.isArray(classSubject.assessments)) {
  classSubject.assessments = [];
}
const enrollments = ref([...(props.enrollments ?? [])]);
const assessmentTypes = ref([...(props.assessmentTypes ?? [])]);

const selectedAssessmentId = ref(
  classSubject.assessments?.[0]?.id ? String(classSubject.assessments[0].id) : "",
);

const assessmentForm = reactive({
  title: "",
  assessmentTypeId: "",
  dueDate: "",
  maxScore: "",
  description: "",
});

const assessmentErrors = ref([]);
const isCreatingAssessment = ref(false);
const gradeErrors = ref([]);
const isSavingGrades = ref(false);

const gradeEntries = reactive({});

const selectedAssessment = computed(() => {
  if (!selectedAssessmentId.value) {
    return null;
  }
  return (
    classSubject.assessments?.find(
      (assessment) => String(assessment.id) === String(selectedAssessmentId.value),
    ) || null
  );
});

const teacherDisplay = computed(
  () => classSubject.teacher?.name ?? "Unassigned teacher",
);

function ensureEntry(studentId) {
  if (!gradeEntries[studentId]) {
    gradeEntries[studentId] = {
      score: "",
      maxScore: selectedAssessment.value?.maxScore ?? "",
      percentage: null,
      letter: null,
      comments: "",
    };
  }
  return gradeEntries[studentId];
}

function hydrateEntries() {
  enrollments.value.forEach((enrollment) => {
    const entry = ensureEntry(enrollment.studentId);
    const grade = selectedAssessment.value?.grades?.find(
      (row) => row.studentId === enrollment.studentId,
    );
    entry.score = grade?.score ?? "";
    entry.maxScore = grade?.maxScore ?? selectedAssessment.value?.maxScore ?? "";
    entry.percentage = grade?.percentage ?? null;
    entry.letter = grade?.letterGrade ?? null;
    entry.comments = grade?.comments ?? "";
  });
}

function calculateMetrics(score, maxScore) {
  if (
    score === null ||
    score === undefined ||
    score === "" ||
    maxScore === null ||
    maxScore === undefined ||
    maxScore === ""
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

function recalculate(studentId) {
  const entry = ensureEntry(studentId);
  const metrics = calculateMetrics(entry.score, entry.maxScore);
  entry.percentage = metrics.percentage;
  entry.letter = metrics.letter;
}

function formatPercentage(value) {
  if (value === null || value === undefined || Number.isNaN(value)) {
    return "—";
  }
  return `${Number(value).toFixed(2)}%`;
}

function formatDueDate(value) {
  if (!value) {
    return "Not scheduled";
  }
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return "Not scheduled";
  }
  return date.toLocaleDateString(undefined, {
    weekday: "short",
    month: "short",
    day: "numeric",
    year: "numeric",
  });
}

function applySelectedAssessment() {
  hydrateEntries();
}

hydrateEntries();

async function createAssessment() {
  assessmentErrors.value = [];
  if (!assessmentForm.title) {
    assessmentErrors.value.push("Title is required.");
  }
  if (!assessmentForm.assessmentTypeId) {
    assessmentErrors.value.push("Assessment type is required.");
  }
  if (assessmentErrors.value.length) {
    return;
  }

  isCreatingAssessment.value = true;
  try {
    const response = await fetch(
      `/classes/${classSubject.classroomId}/subjects/${classSubject.id}/assessments`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "X-CSRF-Token": props.csrfToken,
        },
        body: JSON.stringify(assessmentForm),
      },
    );
    const data = await response.json().catch(() => ({}));
    if (!response.ok) {
      assessmentErrors.value = data.errors || [data.message || "Unable to create assessment."];
      return;
    }
    updateContext(data.context);
    if (data.context?.classSubject?.assessments?.length) {
      const latest = data.context.classSubject.assessments.at(-1);
      selectedAssessmentId.value = String(latest.id);
      hydrateEntries();
    }
    assessmentForm.title = "";
    assessmentForm.assessmentTypeId = "";
    assessmentForm.dueDate = "";
    assessmentForm.maxScore = "";
    assessmentForm.description = "";
  } catch (error) {
    assessmentErrors.value = [error.message || "Unable to create assessment."];
  } finally {
    isCreatingAssessment.value = false;
  }
}

async function saveGrades() {
  gradeErrors.value = [];
  if (!selectedAssessment.value) {
    gradeErrors.value.push("Select an assessment before saving.");
    return;
  }
  isSavingGrades.value = true;
  try {
    const payload = {
      assessmentId: selectedAssessment.value.id,
      entries: enrollments.value.map((enrollment) => ({
        enrollmentId: enrollment.id,
        studentId: enrollment.studentId,
        score: gradeEntries[enrollment.studentId].score,
        maxScore: gradeEntries[enrollment.studentId].maxScore,
        comments: gradeEntries[enrollment.studentId].comments,
      })),
    };
    const response = await fetch(
      `/classes/${classSubject.classroomId}/subjects/${classSubject.id}/grades`,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "X-CSRF-Token": props.csrfToken,
        },
        body: JSON.stringify(payload),
      },
    );
    const data = await response.json().catch(() => ({}));
    if (!response.ok) {
      gradeErrors.value = data.errors || [data.message || "Unable to save grades."];
      return;
    }
    updateContext(data.context);
    hydrateEntries();
  } catch (error) {
    gradeErrors.value = [error.message || "Unable to save grades."];
  } finally {
    isSavingGrades.value = false;
  }
}

function updateContext(context) {
  if (!context) {
    return;
  }
  Object.assign(classSubject, context.classSubject ?? {});
  enrollments.value = context.enrollments ?? enrollments.value;
  if (Array.isArray(context.assessmentTypes)) {
    assessmentTypes.value = context.assessmentTypes;
  }
}
</script>
