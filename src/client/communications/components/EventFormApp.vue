<template>
  <section class="card card--form">
    <header class="card__header">
      <div>
        <h1 class="card__title">{{ isEditMode ? "Edit event" : "Create event" }}</h1>
        <p class="card__subtitle">Plan gatherings, workshops, and celebrations with clarity.</p>
      </div>
      <a class="btn btn--ghost" href="/events">Back to events</a>
    </header>

    <div v-if="serverErrors.length" class="alert alert--danger" role="alert">
      <p><strong>Please review the following:</strong></p>
      <ul>
        <li v-for="error in serverErrors" :key="error">{{ error }}</li>
      </ul>
    </div>

    <form ref="formRef" method="post" :action="formAction" class="form" @submit.prevent="submitForm">
      <input type="hidden" name="_csrf" :value="csrfToken" />

      <label class="form__field">
        <span class="form__label">Title *</span>
        <input v-model.trim="form.title" type="text" name="title" required />
      </label>

      <label class="form__field">
        <span class="form__label">Summary</span>
        <textarea v-model.trim="form.summary" name="summary" rows="2"></textarea>
      </label>

      <label class="form__field form__field--full">
        <span class="form__label">Description</span>
        <textarea v-model="form.description" name="description" rows="6"></textarea>
      </label>

      <div class="form__grid">
        <label class="form__field">
          <span class="form__label">Location</span>
          <input v-model.trim="form.location" type="text" name="location" />
        </label>
        <label class="form__field">
          <span class="form__label">Status</span>
          <select v-model="form.status" name="status">
            <option value="">Draft</option>
            <option v-for="status in lookups.statuses" :key="status" :value="status">{{ status }}</option>
          </select>
        </label>
        <label class="form__field">
          <span class="form__label">Visibility</span>
          <select v-model="form.visibility" name="visibility">
            <option value="">Internal</option>
            <option v-for="visibility in lookups.visibilities" :key="visibility" :value="visibility">
              {{ visibility }}
            </option>
          </select>
        </label>
        <label class="form__field form__field--checkbox">
          <input v-model="form.isAllDay" type="checkbox" name="isAllDay" value="true" />
          <span>All-day event</span>
        </label>
      </div>

      <div class="form__grid">
        <label class="form__field">
          <span class="form__label">Starts *</span>
          <input v-model="form.startAt" type="datetime-local" name="startAt" required />
        </label>
        <label class="form__field">
          <span class="form__label">Ends</span>
          <input v-model="form.endAt" type="datetime-local" name="endAt" />
        </label>
        <label class="form__field">
          <span class="form__label">Publish at</span>
          <input v-model="form.publishAt" type="datetime-local" name="publishAt" />
        </label>
        <label class="form__field">
          <span class="form__label">Registration deadline</span>
          <input v-model="form.registrationDeadline" type="datetime-local" name="registrationDeadline" />
        </label>
      </div>

      <fieldset class="form__fieldset">
        <legend class="form__legend">Audience</legend>
        <p class="form__hint">Select the groups invited to attend.</p>
        <div class="form__checkbox-grid">
          <label v-for="role in lookups.roles" :key="role.id" class="form__checkbox">
            <input
              type="checkbox"
              name="audienceRoleIds"
              :value="role.id"
              v-model="form.audienceRoleIds"
            />
            <span>{{ role.name }}</span>
          </label>
        </div>
      </fieldset>

      <div class="form__actions">
        <button type="submit" class="btn btn--primary">{{ isEditMode ? "Save changes" : "Create event" }}</button>
        <button type="button" class="btn btn--ghost" @click="togglePreview">
          {{ showPreview ? "Hide preview" : "Preview" }}
        </button>
      </div>
    </form>

    <transition name="fade">
      <section v-if="showPreview" class="event-preview">
        <header class="event-preview__header">
          <div>
            <h2>{{ form.title || "Untitled event" }}</h2>
            <p v-if="form.summary" class="event-preview__summary">{{ form.summary }}</p>
          </div>
          <div class="event-preview__badges">
            <span class="badge" :class="statusClass(form.status)">{{ form.status || "DRAFT" }}</span>
            <span class="badge badge--ghost">{{ form.visibility || "INTERNAL" }}</span>
          </div>
        </header>
        <dl class="event-preview__meta">
          <div>
            <dt>Starts</dt>
            <dd>{{ formatDate(form.startAt) || "—" }}</dd>
          </div>
          <div>
            <dt>Ends</dt>
            <dd>{{ formatDate(form.endAt) || "—" }}</dd>
          </div>
          <div v-if="form.location">
            <dt>Location</dt>
            <dd>{{ form.location }}</dd>
          </div>
        </dl>
        <article v-if="form.description" class="event-preview__description">{{ form.description }}</article>
        <footer class="event-preview__footer">
          <strong>Audience:</strong> {{ previewAudience }}
        </footer>
      </section>
    </transition>
  </section>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  lookups: {
    type: Object,
    default: () => ({ statuses: [], visibilities: [], roles: [] }),
  },
  values: { type: Object, default: () => ({}) },
  errors: { type: Array, default: () => [] },
  isEditMode: { type: Boolean, default: false },
  eventId: { type: Number, default: null },
  csrfToken: { type: String, default: "" },
  formAction: { type: String, default: "/events/create" },
});

const serverErrors = computed(() => props.errors || []);
const formRef = ref(null);
const showPreview = ref(false);

const form = reactive({
  title: props.values.title || "",
  summary: props.values.summary || "",
  description: props.values.description || "",
  location: props.values.location || "",
  status: props.values.status || "",
  visibility: props.values.visibility || "",
  isAllDay: Boolean(props.values.isAllDay),
  startAt: props.values.startAt || "",
  endAt: props.values.endAt || "",
  publishAt: props.values.publishAt || "",
  registrationDeadline: props.values.registrationDeadline || "",
  audienceRoleIds: (props.values.audienceRoleIds || []).map((id) => String(id)),
});

function submitForm() {
  formRef.value?.submit();
}

function togglePreview() {
  showPreview.value = !showPreview.value;
}

function statusClass(status) {
  if (!status) {
    return "badge--status-draft";
  }
  return `badge--status-${status.toLowerCase()}`;
}

function formatDate(value) {
  if (!value) {
    return "";
  }
  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? "" : date.toLocaleString();
}

const previewAudience = computed(() => {
  const names = props.lookups.roles
    .filter((role) => form.audienceRoleIds.includes(String(role.id)))
    .map((role) => role.name);
  return names.length > 0 ? names.join(", ") : "All";
});
</script>

<style scoped>
.event-preview {
  margin-top: 2rem;
  padding: 1.5rem;
  background: rgba(255, 255, 255, 0.92);
  border-radius: var(--radius-lg);
  border: 1px solid var(--color-soft-border);
  box-shadow: var(--shadow-soft);
  display: grid;
  gap: 1rem;
}

.event-preview__header {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  align-items: flex-start;
}

.event-preview__badges {
  display: flex;
  gap: 0.5rem;
  align-items: center;
}

.event-preview__meta {
  display: grid;
  gap: 0.5rem;
  color: var(--color-muted);
}

.event-preview__meta div {
  display: flex;
  gap: 0.5rem;
}

.event-preview__summary {
  color: var(--color-muted);
  font-style: italic;
}

.event-preview__description {
  line-height: 1.6;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
