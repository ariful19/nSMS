<template>
  <section class="card card--interactive">
    <header class="card__header">
      <div>
        <h1 class="card__title">Grade reports</h1>
        <p class="card__subtitle">
          Review class and student averages across recorded assessments.
        </p>
      </div>
    </header>
    <form class="form" @submit.prevent="applyFilters">
      <div class="grid grid--three-column">
        <label class="form__group">
          <span>Class</span>
          <select v-model="filters.classId">
            <option value="">All classes</option>
            <option v-for="option in report.filters.classes" :key="option.id" :value="String(option.id)">
              {{ option.name }}
            </option>
          </select>
        </label>
        <label class="form__group">
          <span>Student</span>
          <select v-model="filters.studentId">
            <option value="">All students</option>
            <option v-for="option in report.filters.students" :key="option.id" :value="String(option.id)">
              {{ option.name }}
            </option>
          </select>
        </label>
        <div class="form__actions align-end">
          <button type="submit" class="btn" :disabled="isLoading">Apply filters</button>
          <button type="button" class="btn btn--ghost" @click="resetFilters">Reset</button>
        </div>
      </div>
    </form>

    <div class="card__body">
      <p v-if="isLoading" class="muted">Loading report…</p>
      <p v-else-if="!report.summaries.length" class="muted">No grade data is available for the selected filters.</p>
    </div>

    <div v-if="!isLoading" class="stack">
      <section v-for="summary in report.summaries" :key="summary.classId" class="report-section">
        <header class="report-section__header">
          <h2>{{ summary.className }}</h2>
          <p v-if="summary.gradeLevel" class="muted">Grade level: {{ summary.gradeLevel }}</p>
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
                  <span v-if="student.overall.letterGrade" class="badge">
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
</template>

<script setup>
import { reactive, ref } from "vue";

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

function formatPercentage(value) {
  if (value === null || value === undefined) {
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
