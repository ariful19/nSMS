<template>
  <section class="card card--detail">
    <header class="card__header">
      <sq-avatar-badge :name="fullName" :subtitle="staffTypeName"></sq-avatar-badge>
      <div class="card__actions">
        <a :href="`/teachers/${teacher.id}/edit`" class="btn">Edit</a>
        <form
          class="form form--inline"
          :action="`/teachers/${teacher.id}/delete`"
          method="post"
          @submit.prevent="confirmDelete($event)"
        >
          <input type="hidden" name="_csrf" :value="csrfToken" />
          <button type="submit" class="btn btn--danger">Delete</button>
        </form>
        <a href="/teachers" class="btn btn--link">Back to list</a>
      </div>
    </header>

    <section class="detail-grid">
      <article class="detail-card">
        <h2>Employment overview</h2>
        <dl>
          <div>
            <dt>Staff type</dt>
            <dd>{{ staffTypeName }}</dd>
          </div>
          <div>
            <dt>Status</dt>
            <dd>{{ statusName }}</dd>
          </div>
          <div>
            <dt>Employee number</dt>
            <dd>{{ teacher.employeeNumber || "—" }}</dd>
          </div>
          <div>
            <dt>Primary subject</dt>
            <dd>{{ teacher.primarySubject || "—" }}</dd>
          </div>
          <div>
            <dt>Hire date</dt>
            <dd>{{ formatDate(teacher.hireDate) }}</dd>
          </div>
          <div>
            <dt>Contract end</dt>
            <dd>{{ formatDate(teacher.contractEndDate) }}</dd>
          </div>
        </dl>
        <button type="button" class="btn btn--ghost" @click="showContactModal = true">
          View contact details
        </button>
      </article>
      <article class="detail-card">
        <h2>Personal notes</h2>
        <p v-if="teacher.notes" class="detail-notes">{{ teacher.notes }}</p>
        <p v-else class="detail-empty">No additional notes available.</p>
        <ul class="detail-meta">
          <li>
            <strong>Preferred name</strong>
            <span>{{ teacher.person.preferredName || "—" }}</span>
          </li>
          <li>
            <strong>Date of birth</strong>
            <span>{{ formatDate(teacher.person.dateOfBirth) }}</span>
          </li>
          <li>
            <strong>Gender</strong>
            <span>{{ teacher.person.gender?.name || "—" }}</span>
          </li>
        </ul>
      </article>
    </section>
  </section>

  <sq-modal :open="showContactModal" heading="Contact details" @close="closeModal">
    <dl class="contact-list">
      <div>
        <dt>Primary email</dt>
        <dd>{{ teacher.person.primaryEmail || "—" }}</dd>
      </div>
      <div>
        <dt>Mobile phone</dt>
        <dd>{{ teacher.person.mobilePhone || "—" }}</dd>
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
  teacher: { type: Object, required: true },
  csrfToken: { type: String, default: "" },
});

const teacher = props.teacher;
const csrfToken = props.csrfToken;
const showContactModal = ref(false);

const fullName = computed(
  () => `${teacher.person.firstName} ${teacher.person.lastName}`.trim(),
);
const staffTypeName = computed(() => teacher.staffType?.name || "—");
const statusName = computed(() => teacher.employmentStatus?.name || "—");

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
  if (window.confirm("Delete this record?")) {
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
