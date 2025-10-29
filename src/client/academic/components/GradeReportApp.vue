<template>
  <div class="academic-app">
    <section class="page-hero page-hero--compact">
      <div class="page-hero__content">
        <p class="page-hero__eyebrow">Progress analytics</p>
        <h1 class="page-hero__title">Grade reports</h1>
        <p class="page-hero__subtitle">
          Explore class performance, celebrate wins, and spot support opportunities with calm, clear visuals.
        </p>
      </div>
      <dl class="page-hero__stats">
        <div class="page-hero__stat">
          <dt>Classes tracked</dt>
          <dd>{{ classCount }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Students represented</dt>
          <dd>{{ studentCount }}</dd>
        </div>
        <div class="page-hero__stat">
          <dt>Average overall</dt>
          <dd>{{ overallAverageLabel }}</dd>
        </div>
      </dl>
    </section>

    <section class="card card--interactive">
      <header class="card__header">
        <div>
          <h2 class="card__title">Filters</h2>
          <p class="card__subtitle">Focus on a specific class or learner to tailor the insights you need.</p>
        </div>
      </header>
      <form class="form" @submit.prevent="applyFilters">
        <div class="grid grid--three-column">
          <label class="form__group">
            <span>Class</span>
            <select v-model="filters.classId" class="form__control">
              <option value="">All classes</option>
              <option v-for="option in report.filters.classes" :key="option.id" :value="String(option.id)">
                {{ option.name }}
              </option>
            </select>
          </label>
          <label class="form__group">
            <span>Student</span>
            <select v-model="filters.studentId" class="form__control">
              <option value="">All students</option>
              <option v-for="option in report.filters.students" :key="option.id" :value="String(option.id)">
                {{ option.name }}
              </option>
            </select>
          </label>
          <div class="form__actions align-end">
            <button type="submit" class="btn" :disabled="isLoading">Apply filters</button>
            <button
              type="button"
              class="btn btn--ghost btn--small"
              :disabled="!filtersApplied"
              @click="resetFilters"
            >
              Reset
            </button>
          </div>
        </div>
      </form>

      <div class="card__body">
        <p v-if="isLoading" class="muted">Loading report…</p>
        <div v-else-if="!report.summaries.length" class="empty-state">
          <p class="empty-state__title">No grade data yet</p>
          <p>Record assessments for your classes to populate this space with insights.</p>
        </div>
      </div>

      <div v-if="!isLoading && report.summaries.length" class="stack">
        <section v-for="summary in report.summaries" :key="summary.classId" class="report-section">
          <header class="report-section__header">
            <div>
              <h2>{{ summary.className }}</h2>
              <p v-if="summary.gradeLevel" class="muted">Grade level: {{ summary.gradeLevel }}</p>
            </div>
            <div class="pill pill--soft">
              {{ summary.subjects.length }} subject{{ summary.subjects.length === 1 ? '' : 's' }}
            </div>
          </header>
          <div class="table-responsive">
            <table class="table">
              <thead>
                <tr>
                  <th scope="col">Student</th>
                  <th
                    v-for="subject in summary.subjects"
                    :key="subject.classSubjectId"
                    scope="col"
                  >
                    {{ subject.name }}
                  </th>
                  <th scope="col">Overall</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="student in summary.students" :key="student.studentId">
                  <td data-title="Student">
                    <strong>{{ student.name }}</strong>
                    <span v-if="student.studentNumber" class="badge badge--neutral">
                      {{ student.studentNumber }}
                    </span>
                  </td>
                  <td
                    v-for="subject in summary.subjects"
                    :key="subject.classSubjectId"
                    data-title="Subject"
                  >
                    {{ formatPercentage(student.subjectGrades[String(subject.classSubjectId)]?.percentage) }}
                    <span
                      v-if="student.subjectGrades[String(subject.classSubjectId)]?.letterGrade"
                      class="badge"
                    >
                      {{ student.subjectGrades[String(subject.classSubjectId)].letterGrade }}
                    </span>
                  </td>
                  <td data-title="Overall">
                    {{ formatPercentage(student.overall.percentage) }}
                    <span v-if="student.overall.letterGrade" class="badge badge--success">
                      {{ student.overall.letterGrade }}
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </section>
      </div>
    </section>
  </div>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  report: { type: Object, default: () => ({ summaries: [], filters: { classes: [], students: [] } }) },
});

const report = reactive({
  summaries: props.report?.summaries ?? [],
  filters: {
    classes: props.report?.filters?.classes ?? [],
    students: props.report?.filters?.students ?? [],
  },
});

const filters = reactive({
  classId: props.report?.filters?.classId ? String(props.report.filters.classId) : "",
  studentId: props.report?.filters?.studentId ? String(props.report.filters.studentId) : "",
});

const isLoading = ref(false);

const classCount = computed(() => report.summaries?.length ?? 0);
const studentCount = computed(() => {
  const ids = new Set();
  report.summaries?.forEach((summary) => {
    summary.students?.forEach((student) => ids.add(student.studentId));
  });
  return ids.size;
});

const overallAverage = computed(() => {
  let total = 0;
  let count = 0;
  report.summaries?.forEach((summary) => {
    summary.students?.forEach((student) => {
      if (student.overall?.percentage !== null && student.overall?.percentage !== undefined) {
        const value = Number(student.overall.percentage);
        if (!Number.isNaN(value)) {
          total += value;
          count += 1;
        }
      }
    });
  });
  if (count === 0) {
    return null;
  }
  return Number((total / count).toFixed(2));
});

const overallAverageLabel = computed(() => {
  if (overallAverage.value === null) {
    return "—";
  }
  return `${overallAverage.value.toFixed(2)}%`;
});

const filtersApplied = computed(() => Boolean(filters.classId || filters.studentId));

function formatPercentage(value) {
  if (value === null || value === undefined || Number.isNaN(value)) {
    return "—";
  }
  return `${Number(value).toFixed(2)}%`;
}

function resetFilters() {
  filters.classId = "";
  filters.studentId = "";
  applyFilters();
}

async function applyFilters() {
  isLoading.value = true;
  try {
    const params = new URLSearchParams();
    if (filters.classId) {
      params.set("classId", filters.classId);
    }
    if (filters.studentId) {
      params.set("studentId", filters.studentId);
    }
    const response = await fetch(`/reports/grades/data?${params.toString()}`);
    if (!response.ok) {
      throw new Error("Failed to load report");
    }
    const data = await response.json();
    report.summaries = data.report?.summaries ?? [];
    report.filters.classes = data.report?.filters?.classes ?? report.filters.classes;
    report.filters.students = data.report?.filters?.students ?? report.filters.students;
  } catch (error) {
    // eslint-disable-next-line no-console
    console.error(error);
  } finally {
    isLoading.value = false;
  }
}
</script>
