<template>
  <section class="notice-center">
    <header class="notice-center__header">
      <div>
        <h1>Notices</h1>
        <p>Filter announcements and share updates with the community.</p>
      </div>
      <div class="notice-center__actions">
        <div class="notice-center__filters">
          <label class="form__field">
            <span class="form__label">Search</span>
            <input v-model.trim="filtersState.search" type="search" placeholder="Title or summary" />
          </label>
          <label class="form__field">
            <span class="form__label">Status</span>
            <select v-model="filtersState.status">
              <option value="">All</option>
              <option v-for="status in lookups.statuses" :key="status" :value="status">{{ status }}</option>
            </select>
          </label>
          <label v-if="canManage" class="form__field form__field--checkbox">
            <input v-model="filtersState.includeExpired" type="checkbox" />
            <span>Show expired</span>
          </label>
          <div class="notice-center__audiences" role="group" aria-label="Audience">
            <span class="form__label">Audience</span>
            <div class="chip-group">
              <button
                v-for="role in lookups.roles"
                :key="role.id"
                type="button"
                class="chip"
                :class="{ 'chip--active': selectedAudienceIds.has(role.id) }"
                @click="toggleAudience(role.id)"
              >
                {{ role.name }}
              </button>
            </div>
          </div>
        </div>
        <div class="notice-center__cta" v-if="canManage">
          <a class="btn btn--primary" href="/notices/create">New notice</a>
        </div>
      </div>
    </header>

    <div v-if="filteredNotices.length === 0" class="notice-center__empty">
      <p>No notices match your filters.</p>
    </div>

    <div v-else class="notice-center__list">
      <article v-for="notice in filteredNotices" :key="notice.id" class="notice-card">
        <header class="notice-card__header">
          <div>
            <h2 class="notice-card__title">
              <a :href="`/notices/${notice.id}`">{{ notice.title }}</a>
            </h2>
            <p v-if="notice.summary" class="notice-card__summary">{{ notice.summary }}</p>
          </div>
          <div class="notice-card__badges">
            <span class="badge" :class="statusClass(notice.status)">{{ notice.status }}</span>
            <span v-if="notice.isPinned" class="badge badge--accent">Pinned</span>
          </div>
        </header>
        <div class="notice-card__body" v-html="notice.content"></div>
        <footer class="notice-card__footer">
          <div>
            <strong>Audience:</strong>
            <span>{{ formatAudience(notice.audiences) }}</span>
          </div>
          <div class="notice-card__dates">
            <span v-if="notice.publishAt">Published {{ formatDate(notice.publishAt) }}</span>
            <span v-if="notice.expiresAt">Visible until {{ formatDate(notice.expiresAt, true) }}</span>
          </div>
          <div v-if="canManage" class="notice-card__actions">
            <a :href="`/notices/${notice.id}/edit`" class="btn btn--small">Edit</a>
            <form
              method="post"
              :action="`/notices/${notice.id}/delete`"
              class="form form--inline"
              @submit="confirmDelete($event)"
            >
              <input type="hidden" name="_csrf" :value="csrfToken" />
              <button type="submit" class="btn btn--small btn--danger">Delete</button>
            </form>
          </div>
        </footer>
      </article>
    </div>
  </section>
</template>

<script setup>
import { computed, reactive } from "vue";

const props = defineProps({
  notices: { type: Array, default: () => [] },
  lookups: { type: Object, default: () => ({ statuses: [], roles: [] }) },
  filters: { type: Object, default: () => ({}) },
  canManage: { type: Boolean, default: false },
  csrfToken: { type: String, default: "" },
});

const filtersState = reactive({
  search: props.filters.search || "",
  status: props.filters.status || "",
  includeExpired: Boolean(props.filters.includeExpired),
});

const selectedAudienceIds = reactive(new Set((props.filters.audienceRoleIds || []).map(Number)));

function toggleAudience(roleId) {
  if (selectedAudienceIds.has(roleId)) {
    selectedAudienceIds.delete(roleId);
  } else {
    selectedAudienceIds.add(roleId);
  }
}

function statusClass(status) {
  if (!status) {
    return "";
  }
  return `badge--status-${status.toLowerCase()}`;
}

function formatDate(value, dateOnly = false) {
  if (!value) {
    return "";
  }
  const date = value instanceof Date ? value : new Date(value);
  if (Number.isNaN(date.getTime())) {
    return "";
  }
  return dateOnly ? date.toLocaleDateString() : date.toLocaleString();
}

function formatAudience(audiences = []) {
  const names = audiences
    .map((audience) => (audience.role ? audience.role.name : null))
    .filter(Boolean);
  return names.length > 0 ? names.join(", ") : "All";
}

function confirmDelete(event) {
  if (!window.confirm("Delete this notice?")) {
    event.preventDefault();
  }
}

const filteredNotices = computed(() => {
  const searchTerm = filtersState.search.toLowerCase();
  const statusFilter = filtersState.status;
  const includeExpired = filtersState.includeExpired;
  const audienceFilter = selectedAudienceIds.size > 0 ? Array.from(selectedAudienceIds) : null;
  const now = new Date();

  return (props.notices || []).filter((notice) => {
    if (searchTerm) {
      const haystack = `${notice.title || ""} ${notice.summary || ""}`.toLowerCase();
      if (!haystack.includes(searchTerm)) {
        return false;
      }
    }

    if (statusFilter && notice.status !== statusFilter) {
      return false;
    }

    if (!includeExpired && notice.expiresAt) {
      const expiresAt = new Date(notice.expiresAt);
      if (!Number.isNaN(expiresAt.getTime()) && expiresAt < now) {
        return false;
      }
    }

    if (audienceFilter) {
      const noticeRoles = new Set((notice.audiences || []).map((audience) => audience.roleId));
      const hasAny = audienceFilter.some((roleId) => noticeRoles.has(roleId));
      if (!hasAny) {
        return false;
      }
    }

    return true;
  });
});
</script>

<style scoped>
.notice-center {
  display: grid;
  gap: 2rem;
}

.notice-center__header {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.notice-center__actions {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  align-items: flex-end;
}

.notice-center__filters {
  display: grid;
  gap: 1rem;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  align-items: flex-end;
}

.notice-center__audiences {
  display: grid;
  gap: 0.5rem;
}

.chip-group {
  display: flex;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.chip {
  border: 1px solid var(--color-border);
  padding: 0.35rem 0.75rem;
  border-radius: 999px;
  background: rgba(255, 255, 255, 0.6);
  cursor: pointer;
  transition: all 0.2s ease;
}

.chip--active {
  background: var(--color-primary-500);
  color: #fff;
  border-color: var(--color-primary-500);
}

.notice-center__list {
  display: grid;
  gap: 1.5rem;
}

.notice-card {
  background: rgba(255, 255, 255, 0.92);
  border-radius: var(--radius-lg);
  padding: 1.5rem;
  box-shadow: var(--shadow-soft);
  border: 1px solid var(--color-soft-border);
  display: grid;
  gap: 1rem;
}

.notice-card__header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  gap: 1rem;
}

.notice-card__badges {
  display: flex;
  gap: 0.5rem;
  align-items: center;
}

.notice-card__body {
  color: var(--color-muted);
  line-height: 1.6;
}

.notice-card__footer {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem 2rem;
  justify-content: space-between;
  align-items: center;
}

.notice-card__actions {
  display: inline-flex;
  gap: 0.75rem;
}

.notice-center__empty {
  padding: 2rem;
  text-align: center;
  background: rgba(255, 255, 255, 0.8);
  border-radius: var(--radius-lg);
  border: 1px solid var(--color-soft-border);
}
</style>
