<template>
  <section class="card card--interactive">
    <header class="card__header">
      <div>
        <h1 class="card__title">Teachers &amp; Staff</h1>
        <p class="card__subtitle">
          Showing {{ filteredTeachers.length }} {{ filteredTeachers.length === 1 ? "person" : "people" }}
        </p>
      </div>
      <div class="card__actions">
        <a class="btn" href="/teachers/create">Add teacher or staff</a>
        <a class="btn btn--ghost" href="/teachers">Clear search</a>
      </div>
    </header>

    <form class="filters" method="get" action="/teachers" novalidate>
      <label class="filters__field">
        <span>Search</span>
        <input
          v-model="searchTerm"
          type="search"
          name="search"
          placeholder="Name, employee number, or subject"
        />
      </label>
      <label class="filters__field">
        <span>Staff type</span>
        <select v-model="staffTypeFilter">
          <option value="all">All staff</option>
          <option v-for="type in staffTypes" :key="type.id" :value="String(type.id)">
            {{ type.name }}
          </option>
        </select>
      </label>
      <label class="filters__field">
        <span>Status</span>
        <select v-model="statusFilter">
          <option value="all">All statuses</option>
          <option v-for="status in employmentStatuses" :key="status.id" :value="String(status.id)">
            {{ status.name }}
          </option>
        </select>
      </label>
      <label class="filters__checkbox">
        <input type="checkbox" v-model="showOnlyActive" />
        <span>Currently employed</span>
      </label>
      <button type="submit" class="btn btn--ghost filters__submit">Apply on server</button>
      <button type="button" class="btn btn--link filters__submit" @click="resetFilters">Reset filters</button>
    </form>

    <div v-if="hasResults" class="table-responsive">
      <table class="table">
        <thead>
          <tr>
            <th scope="col">Name</th>
            <th scope="col">Role</th>
            <th scope="col">Status</th>
            <th scope="col">Employee #</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="teacher in filteredTeachers" :key="teacher.id">
            <td data-title="Name">
              <button type="button" class="table__link" @click="openSummary(teacher)">
                {{ teacher.person.firstName }} {{ teacher.person.lastName }}
              </button>
            </td>
            <td data-title="Role">{{ teacher.staffType?.name ?? "—" }}</td>
            <td data-title="Status">{{ teacher.employmentStatus?.name ?? "—" }}</td>
            <td data-title="Employee #">{{ teacher.employeeNumber || "—" }}</td>
            <td class="table__actions">
              <a :href="`/teachers/${teacher.id}`" class="btn btn--small btn--ghost">View</a>
              <a :href="`/teachers/${teacher.id}/edit`" class="btn btn--small">Edit</a>
              <form
                class="form form--inline"
                :action="`/teachers/${teacher.id}/delete`"
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
    <p v-else class="table__empty">No results. Adjust the filters above.</p>
  </section>

  <sq-modal :open="Boolean(selectedTeacher)" heading="Profile summary" @close="closeSummary">
    <template v-if="selectedTeacher">
      <sq-avatar-badge
        :name="`${selectedTeacher.person.firstName} ${selectedTeacher.person.lastName}`"
        :subtitle="selectedTeacher.staffType?.name || 'Team member'"
      ></sq-avatar-badge>
      <dl class="summary">
        <div>
          <dt>Employment status</dt>
          <dd>{{ selectedTeacher.employmentStatus?.name ?? "—" }}</dd>
        </div>
        <div>
          <dt>Primary subject</dt>
          <dd>{{ selectedTeacher.primarySubject || "—" }}</dd>
        </div>
        <div>
          <dt>Hire date</dt>
          <dd>{{ formatDate(selectedTeacher.hireDate) }}</dd>
        </div>
        <div>
          <dt>Contract end</dt>
          <dd>{{ formatDate(selectedTeacher.contractEndDate) }}</dd>
        </div>
        <div>
          <dt>Email</dt>
          <dd>{{ selectedTeacher.person.primaryEmail || "—" }}</dd>
        </div>
      </dl>
      <div slot="footer" class="modal-footer">
        <a :href="`/teachers/${selectedTeacher.id}`" class="btn btn--primary">Open profile</a>
        <button type="button" class="btn" @click="closeSummary">Close</button>
      </div>
    </template>
  </sq-modal>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  teachers: { type: Array, default: () => [] },
  search: { type: String, default: "" },
  lookups: {
    type: Object,
    default: () => ({ staffTypes: [], employmentStatuses: [] }),
  },
  csrfToken: { type: String, default: "" },
});

const searchTerm = ref(props.search ?? "");
const staffTypeFilter = ref("all");
const statusFilter = ref("all");
const showOnlyActive = ref(false);
const selectedTeacher = ref(null);

const staffTypes = computed(() => props.lookups?.staffTypes ?? []);
const employmentStatuses = computed(() => props.lookups?.employmentStatuses ?? []);

const activeEmploymentIds = computed(() =>
  new Set(
    (props.lookups?.employmentStatuses ?? [])
      .filter((status) => status.isActive !== false)
      .map((status) => String(status.id)),
  ),
);

function normalize(value) {
  return (value || "").toString().toLowerCase();
}

function matchesSearch(teacher) {
  const term = normalize(searchTerm.value);
  if (!term) {
    return true;
  }
  const fields = [
    `${teacher.person.firstName} ${teacher.person.lastName}`,
    teacher.employeeNumber,
    teacher.primarySubject,
    teacher.staffType?.name,
  ];
  return fields.some((field) => normalize(field).includes(term));
}

function matchesStaffType(teacher) {
  if (staffTypeFilter.value === "all") {
    return true;
  }
  return String(teacher.staffTypeId ?? "") === staffTypeFilter.value;
}

function matchesStatus(teacher) {
  if (statusFilter.value === "all") {
    return true;
  }
  return String(teacher.employmentStatusId ?? "") === statusFilter.value;
}

function matchesActive(teacher) {
  if (!showOnlyActive.value) {
    return true;
  }
  return activeEmploymentIds.value.has(String(teacher.employmentStatusId ?? ""));
}

const filteredTeachers = computed(() =>
  props.teachers.filter(
    (teacher) =>
      matchesSearch(teacher) && matchesStaffType(teacher) && matchesStatus(teacher) && matchesActive(teacher),
  ),
);

const hasResults = computed(() => filteredTeachers.value.length > 0);

function resetFilters() {
  searchTerm.value = "";
  staffTypeFilter.value = "all";
  statusFilter.value = "all";
  showOnlyActive.value = false;
}

function openSummary(teacher) {
  selectedTeacher.value = reactive(teacher);
}

function closeSummary() {
  selectedTeacher.value = null;
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
  if (window.confirm("Delete this record?")) {
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

.card__actions {
  display: flex;
  align-items: center;
  gap: 0.75rem;
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
