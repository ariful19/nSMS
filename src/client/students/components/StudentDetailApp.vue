<template>
  <section class="card card--detail">
    <header class="card__header">
      <sq-avatar-badge :name="fullName" :subtitle="statusName"></sq-avatar-badge>
      <div class="card__actions">
        <a :href="`/students/${student.id}/edit`" class="btn">Edit</a>
        <form
          class="form form--inline"
          :action="`/students/${student.id}/delete`"
          method="post"
          @submit.prevent="confirmDelete($event)"
        >
          <input type="hidden" name="_csrf" :value="csrfToken" />
          <button type="submit" class="btn btn--danger">Delete</button>
        </form>
        <a href="/students" class="btn btn--link">Back to list</a>
      </div>
    </header>

    <section class="detail-grid">
      <article class="detail-card">
        <h2>Enrollment overview</h2>
        <dl>
          <div>
            <dt>Status</dt>
            <dd>{{ statusName }}</dd>
          </div>
          <div>
            <dt>Grade level</dt>
            <dd>{{ gradeName }}</dd>
          </div>
          <div>
            <dt>Student number</dt>
            <dd>{{ student.studentNumber || "—" }}</dd>
          </div>
          <div>
            <dt>Admission date</dt>
            <dd>{{ formatDate(student.admissionDate) }}</dd>
          </div>
          <div>
            <dt>Graduation date</dt>
            <dd>{{ formatDate(student.graduationDate) }}</dd>
          </div>
        </dl>
        <button type="button" class="btn btn--ghost" @click="showContactModal = true">
          View contact details
        </button>
      </article>
      <article class="detail-card">
        <h2>Personal notes</h2>
        <p v-if="student.notes" class="detail-notes">{{ student.notes }}</p>
        <p v-else class="detail-empty">No additional notes available.</p>
        <ul class="detail-meta">
          <li>
            <strong>Preferred name</strong>
            <span>{{ student.person.preferredName || "—" }}</span>
          </li>
          <li>
            <strong>Date of birth</strong>
            <span>{{ formatDate(student.person.dateOfBirth) }}</span>
          </li>
          <li>
            <strong>Gender</strong>
            <span>{{ student.person.gender?.name || "—" }}</span>
          </li>
        </ul>
      </article>
    </section>
  </section>

  <sq-modal :open="showContactModal" heading="Contact details" @close="closeModal">
    <dl class="contact-list">
      <div>
        <dt>Primary email</dt>
        <dd>{{ student.person.primaryEmail || "—" }}</dd>
      </div>
      <div>
        <dt>Mobile phone</dt>
        <dd>{{ student.person.mobilePhone || "—" }}</dd>
      </div>
    </dl>
    <div slot="footer">
      <button type="button" class="btn" @click="closeModal">Close</button>
    </div>
  </sq-modal>
</template>

<script setup>
import { computed, ref } from "vue";

const props = defineProps({
  student: { type: Object, required: true },
  csrfToken: { type: String, default: "" },
});

const student = props.student;
const csrfToken = props.csrfToken;

const showContactModal = ref(false);

const fullName = computed(
  () => `${student.person.firstName} ${student.person.lastName}`.trim(),
);
const statusName = computed(() => student.enrollmentStatus?.name || "—");
const gradeName = computed(() => student.gradeLevel?.name || "—");

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

function closeModal() {
  showContactModal.value = false;
}

function confirmDelete(event) {
  if (window.confirm("Delete this student?")) {
    event.target.submit();
  }
}
</script>

<style scoped>
.card--detail {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
  padding: 1.5rem;
}

.card__header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  flex-wrap: wrap;
  gap: 1rem;
}

.card__actions {
  display: flex;
  gap: 0.75rem;
  flex-wrap: wrap;
}

.detail-grid {
  display: grid;
  gap: 1.5rem;
  grid-template-columns: repeat(auto-fit, minmax(18rem, 1fr));
}

.detail-card {
  background: #f8fafc;
  border-radius: 0.75rem;
  padding: 1.25rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.detail-card h2 {
  margin: 0;
  font-size: 1.1rem;
}

.detail-card dl {
  display: grid;
  gap: 0.75rem;
  margin: 0;
}

.detail-card dt {
  font-weight: 600;
  color: #0f172a;
}

.detail-card dd {
  margin: 0;
  color: #475569;
}

.detail-notes {
  margin: 0;
  white-space: pre-wrap;
}

.detail-empty {
  margin: 0;
  color: #94a3b8;
}

.detail-meta {
  list-style: none;
  padding: 0;
  margin: 0;
  display: grid;
  gap: 0.5rem;
}

.detail-meta li {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
}

.detail-meta strong {
  color: #0f172a;
}

.contact-list {
  display: grid;
  gap: 0.75rem;
  margin: 0;
}

.contact-list dt {
  font-weight: 600;
}

.contact-list dd {
  margin: 0;
}
</style>
