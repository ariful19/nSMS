<template>
  <section class="card card--interactive">
    <header class="card__header">
      <div>
        <h1 class="card__title">Students</h1>
        <p class="card__subtitle">
          <strong>{{ filteredStudents.length }}</strong>
          {{ filteredStudents.length === 1 ? "record" : "records" }} match your filters
        </p>
      </div>
      <div class="card__actions">
        <a class="btn" href="/students/create">Add student</a>
        <a class="btn btn--ghost" href="/students">Clear search</a>
      </div>
    </header>

    <form class="filters" method="get" action="/students" novalidate>
      <label class="filters__field">
        <span>Search</span>
        <input
          v-model="searchTerm"
          type="search"
          name="search"
          placeholder="Name or student number"
        />
      </label>
      <label class="filters__field">
        <span>Status</span>
        <select v-model="statusFilter">
          <option value="all">All statuses</option>
          <option
            v-for="status in statusOptions"
            :key="status.id"
            :value="String(status.id)"
          >
            {{ status.name }}
          </option>
        </select>
      </label>
      <label class="filters__field">
        <span>Grade</span>
        <select v-model="gradeFilter">
          <option value="all">All grades</option>
          <option
            v-for="grade in gradeOptions"
            :key="grade.id"
            :value="String(grade.id)"
          >
            {{ grade.name }}
          </option>
        </select>
      </label>
      <label class="filters__checkbox">
        <input type="checkbox" v-model="showOnlyActive" />
        <span>Show active enrollment only</span>
      </label>
      <button type="submit" class="btn btn--ghost filters__submit">
        Apply on server
      </button>
      <button type="button" class="btn btn--link filters__submit" @click="resetFilters">
        Reset filters
      </button>
    </form>

    <div v-if="hasResults" class="table-responsive">
      <table class="table">
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col">Status</th>
            <th scope="col">Grade</th>
            <th scope="col">Student #</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="student in filteredStudents" :key="student.id">
            <td data-title="Name">
              <button type="button" class="table__link" @click="openSummary(student)">
                {{ student.person.firstName }} {{ student.person.lastName }}
              </button>
            </td>
            <td data-title="Status">
              {{ student.enrollmentStatus?.name ?? "—" }}
            </td>
            <td data-title="Grade">
              {{ student.gradeLevel?.name ?? "—" }}
            </td>
            <td data-title="Student #">
              {{ student.studentNumber || "—" }}
            </td>
            <td class="table__actions">
              <a :href="`/students/${student.id}`" class="btn btn--small btn--ghost">View</a>
              <a :href="`/students/${student.id}/edit`" class="btn btn--small">Edit</a>
              <form
                class="form form--inline"
                :action="`/students/${student.id}/delete`"
                method="post"
                @submit.prevent="confirmDelete($event)"
              >
                <input type="hidden" name="_csrf" :value="csrfToken" />
                <button type="submit" class="btn btn--small btn--danger">Delete</button>
              </form>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <p v-else class="table__empty">
      No students match your filters. Try adjusting them above.
    </p>
  </section>

  <sq-modal :open="Boolean(selectedStudent)" heading="Student summary" @close="closeSummary">
    <template v-if="selectedStudent">
      <sq-avatar-badge
        :name="`${selectedStudent.person.firstName} ${selectedStudent.person.lastName}`"
        :subtitle="selectedStudent.enrollmentStatus?.name || 'Student'"
      ></sq-avatar-badge>
      <dl class="summary">
        <div>
          <dt>Grade</dt>
          <dd>{{ selectedStudent.gradeLevel?.name ?? "—" }}</dd>
        </div>
        <div>
          <dt>Student number</dt>
          <dd>{{ selectedStudent.studentNumber || "—" }}</dd>
        </div>
        <div>
          <dt>Admission date</dt>
          <dd>{{ formatDate(selectedStudent.admissionDate) }}</dd>
        </div>
        <div>
          <dt>Primary email</dt>
          <dd>{{ selectedStudent.person.primaryEmail || "—" }}</dd>
        </div>
        <div>
          <dt>Mobile phone</dt>
          <dd>{{ selectedStudent.person.mobilePhone || "—" }}</dd>
        </div>
      </dl>
      <div slot="footer" class="modal-footer">
        <a :href="`/students/${selectedStudent.id}`" class="btn btn--primary">Open profile</a>
        <button type="button" class="btn" @click="closeSummary">Close</button>
      </div>
    </template>
  </sq-modal>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  students: { type: Array, default: () => [] },
  search: { type: String, default: "" },
  lookups: {
    type: Object,
    default: () => ({ statuses: [], gradeLevels: [] }),
  },
  csrfToken: { type: String, default: "" },
});

const searchTerm = ref(props.search ?? "");
const statusFilter = ref("all");
const gradeFilter = ref("all");
const showOnlyActive = ref(false);
const selectedStudent = ref(null);

const statusOptions = computed(() => props.lookups?.statuses ?? []);
const gradeOptions = computed(() => props.lookups?.gradeLevels ?? []);

const activeStatusIds = computed(() =>
  new Set(
    (props.lookups?.statuses ?? [])
      .filter((status) => status.isActive !== false)
      .map((status) => String(status.id)),
  ),
);

function normalize(text) {
  return (text || "").toString().toLowerCase();
}

function matchesSearch(student) {
  const term = normalize(searchTerm.value);
  if (!term) {
    return true;
  }
  const fields = [
    `${student.person.firstName} ${student.person.lastName}`,
    student.studentNumber,
    student.enrollmentStatus?.name,
  ];
  return fields.some((value) => normalize(value).includes(term));
}

function matchesStatus(student) {
  if (statusFilter.value === "all") {
    return true;
  }
  return String(student.enrollmentStatusId ?? "") === statusFilter.value;
}

function matchesGrade(student) {
  if (gradeFilter.value === "all") {
    return true;
  }
  return String(student.gradeLevelId ?? "") === gradeFilter.value;
}

function matchesActive(student) {
  if (!showOnlyActive.value) {
    return true;
  }
  const statusId = String(student.enrollmentStatusId ?? "");
  return activeStatusIds.value.has(statusId);
}

const filteredStudents = computed(() =>
  props.students.filter(
    (student) =>
      matchesSearch(student) &&
      matchesStatus(student) &&
      matchesGrade(student) &&
      matchesActive(student),
  ),
);

const hasResults = computed(() => filteredStudents.value.length > 0);

function resetFilters() {
  searchTerm.value = "";
  statusFilter.value = "all";
  gradeFilter.value = "all";
  showOnlyActive.value = false;
}

function openSummary(student) {
  selectedStudent.value = reactive(student);
}

function closeSummary() {
  selectedStudent.value = null;
}

function formatDate(value) {
  if (!value) {
    return "—";
  }
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return "—";
  }
  return date.toLocaleDateString();
}

function confirmDelete(event) {
  if (window.confirm("Delete this student?")) {
    event.target.submit();
  }
}
</script>

<style scoped>
.card {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
  padding: 1.5rem;
}

.card__header {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  flex-wrap: wrap;
}

.card__title {
  margin: 0;
}

.card__actions {
  display: flex;
  gap: 0.75rem;
  align-items: center;
}

.filters {
  display: grid;
  gap: 1rem;
  grid-template-columns: repeat(auto-fit, minmax(12rem, 1fr));
  align-items: end;
}

.filters__field {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  font-size: 0.9rem;
}

.filters__field input,
.filters__field select {
  border: 1px solid rgba(15, 23, 42, 0.2);
  border-radius: 0.5rem;
  padding: 0.45rem 0.65rem;
  font: inherit;
}

.filters__checkbox {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.filters__submit {
  justify-self: flex-start;
}

.table__link {
  background: none;
  border: none;
  padding: 0;
  color: #2563eb;
  cursor: pointer;
  text-decoration: underline;
}

.summary {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(10rem, 1fr));
  gap: 0.75rem 1rem;
  margin: 1.25rem 0;
}

.summary dt {
  font-weight: 600;
  color: #0f172a;
}

.summary dd {
  margin: 0;
  color: #475569;
}

.modal-footer {
  display: flex;
  justify-content: flex-end;
  gap: 0.5rem;
}

.table__empty {
  text-align: center;
  color: #475569;
  margin: 0;
  padding: 1rem 0;
}
</style>
