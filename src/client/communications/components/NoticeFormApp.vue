<template>
  <section class="card card--form">
    <header class="card__header">
      <div>
        <h1 class="card__title">{{ isEditMode ? "Edit notice" : "Create notice" }}</h1>
        <p class="card__subtitle">Craft updates and choose when and where they appear.</p>
      </div>
      <a class="btn btn--ghost" href="/notices">Back to notices</a>
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
        <span class="form__label">Content *</span>
        <textarea v-model="form.content" name="content" rows="8" required></textarea>
      </label>

      <div class="form__grid">
        <label class="form__field">
          <span class="form__label">Status</span>
          <select v-model="form.status" name="status">
            <option value="">Draft</option>
            <option v-for="status in lookups.statuses" :key="status" :value="status">{{ status }}</option>
          </select>
        </label>
        <label class="form__field">
          <span class="form__label">Publish at</span>
          <input v-model="form.publishAt" type="datetime-local" name="publishAt" />
        </label>
        <label class="form__field">
          <span class="form__label">Expires at</span>
          <input v-model="form.expiresAt" type="datetime-local" name="expiresAt" />
        </label>
        <label class="form__field form__field--checkbox">
          <input v-model="form.isPinned" type="checkbox" name="isPinned" value="true" />
          <span>Pin to top</span>
        </label>
      </div>

      <fieldset class="form__fieldset">
        <legend class="form__legend">Audience</legend>
        <p class="form__hint">Select the roles that should receive this notice.</p>
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
        <button type="submit" class="btn btn--primary">{{ isEditMode ? "Save changes" : "Create notice" }}</button>
        <button type="button" class="btn btn--ghost" @click="togglePreview">
          {{ showPreview ? "Hide preview" : "Preview" }}
        </button>
      </div>
    </form>

    <transition name="fade">
      <section v-if="showPreview" class="notice-preview">
        <header class="notice-preview__header">
          <h2>{{ form.title || "Untitled notice" }}</h2>
          <div class="notice-preview__meta">
            <span class="badge" :class="statusClass(form.status)">{{ form.status || "DRAFT" }}</span>
            <span v-if="form.publishAt">Publishes {{ formatDate(form.publishAt) }}</span>
            <span v-if="form.expiresAt">Expires {{ formatDate(form.expiresAt) }}</span>
          </div>
        </header>
        <p v-if="form.summary" class="notice-preview__summary">{{ form.summary }}</p>
        <div class="notice-preview__content" v-html="form.content"></div>
        <footer class="notice-preview__footer">
          <strong>Audience:</strong> {{ previewAudience }}
        </footer>
      </section>
    </transition>
  </section>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  lookups: { type: Object, default: () => ({ statuses: [], roles: [] }) },
  values: { type: Object, default: () => ({}) },
  errors: { type: Array, default: () => [] },
  isEditMode: { type: Boolean, default: false },
  noticeId: { type: Number, default: null },
  csrfToken: { type: String, default: "" },
  formAction: { type: String, default: "/notices/create" },
});

const serverErrors = computed(() => props.errors || []);
const formRef = ref(null);
const showPreview = ref(false);

const form = reactive({
  title: props.values.title || "",
  summary: props.values.summary || "",
  content: props.values.content || "",
  status: props.values.status || "",
  publishAt: props.values.publishAt || "",
  expiresAt: props.values.expiresAt || "",
  isPinned: Boolean(props.values.isPinned),
});

form.audienceRoleIds = (props.values.audienceRoleIds || []).map((id) => String(id));

function submitForm() {
  const formElement = formRef.value;
  if (!formElement) {
    return;
  }
  formElement.submit();
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
.notice-preview {
  margin-top: 2rem;
  padding: 1.5rem;
  border-radius: var(--radius-lg);
  border: 1px solid var(--color-soft-border);
  background: rgba(255, 255, 255, 0.92);
  box-shadow: var(--shadow-soft);
  display: grid;
  gap: 1rem;
}

.notice-preview__header {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  justify-content: space-between;
  align-items: center;
}

.notice-preview__meta {
  display: inline-flex;
  gap: 0.75rem;
  align-items: center;
}

.notice-preview__summary {
  font-style: italic;
  color: var(--color-muted);
}

.notice-preview__content {
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
