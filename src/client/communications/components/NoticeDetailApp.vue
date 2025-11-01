<template>
  <article class="notice-detail">
    <header class="notice-detail__header">
      <div>
        <h1 class="notice-detail__title">{{ notice.title }}</h1>
        <p v-if="notice.summary" class="notice-detail__summary">{{ notice.summary }}</p>
        <div class="notice-detail__meta">
          <span class="badge" :class="statusClass(notice.status)">{{ notice.status }}</span>
          <span v-if="notice.isPinned" class="badge badge--accent">Pinned</span>
          <span v-if="notice.publishAt">Published {{ formatDate(notice.publishAt) }}</span>
          <span v-if="notice.expiresAt">Visible until {{ formatDate(notice.expiresAt, true) }}</span>
          <button type="button" class="badge badge--ghost" @click="showAudience = !showAudience">
            {{ showAudience ? "Hide" : "Show" }} audience
          </button>
        </div>
      </div>
      <div v-if="canManage" class="notice-detail__actions">
        <a :href="`/notices/${notice.id}/edit`" class="btn">Edit</a>
        <form
          method="post"
          :action="`/notices/${notice.id}/delete`"
          class="form form--inline"
          @submit="confirmDelete"
        >
          <input type="hidden" name="_csrf" :value="csrfToken" />
          <button type="submit" class="btn btn--danger">Delete</button>
        </form>
      </div>
    </header>

    <section class="notice-detail__content" v-html="notice.content"></section>

    <transition name="fade">
      <section v-if="showAudience" class="notice-detail__audience">
        <h2>Audience</h2>
        <ul>
          <li v-for="role in audienceNames" :key="role">{{ role }}</li>
        </ul>
      </section>
    </transition>
  </article>
</template>

<script setup>
import { computed, ref } from "vue";

const props = defineProps({
  notice: { type: Object, required: true },
  canManage: { type: Boolean, default: false },
  csrfToken: { type: String, default: "" },
});

const showAudience = ref(false);

function statusClass(status) {
  if (!status) {
    return "badge--status-draft";
  }
  return `badge--status-${status.toLowerCase()}`;
}

function formatDate(value, dateOnly = false) {
  if (!value) {
    return "";
  }
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return "";
  }
  return dateOnly ? date.toLocaleDateString() : date.toLocaleString();
}

function confirmDelete(event) {
  if (!window.confirm("Delete this notice?")) {
    event.preventDefault();
  }
}

const audienceNames = computed(() => {
  const names = (props.notice.audiences || [])
    .map((audience) => (audience.role ? audience.role.name : null))
    .filter(Boolean);
  return names.length > 0 ? names : ["All roles"];
});
</script>

<style scoped>
.notice-detail {
  display: grid;
  gap: 1.5rem;
}

.notice-detail__header {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.notice-detail__actions {
  display: inline-flex;
  gap: 0.75rem;
}

.notice-detail__meta {
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
  align-items: center;
  color: var(--color-muted);
}

.notice-detail__content {
  padding: 1.5rem;
  border-radius: var(--radius-lg);
  background: rgba(255, 255, 255, 0.92);
  border: 1px solid var(--color-soft-border);
  box-shadow: var(--shadow-soft);
  line-height: 1.7;
}

.notice-detail__audience {
  padding: 1rem 1.5rem;
  border-left: 4px solid var(--color-primary-500);
  background: rgba(79, 70, 229, 0.08);
  border-radius: var(--radius-lg);
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
