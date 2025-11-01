<template>
  <section class="event-board">
    <header class="event-board__header">
      <div>
        <h1>Events</h1>
        <p>Explore upcoming happenings and revisit recently completed gatherings.</p>
      </div>
      <div class="event-board__actions">
        <div class="event-board__filters">
          <label class="form__field">
            <span class="form__label">Search</span>
            <input v-model.trim="filterState.search" type="search" placeholder="Title or location" />
          </label>
          <label class="form__field">
            <span class="form__label">Status</span>
            <select v-model="filterState.status">
              <option value="">All</option>
              <option v-for="status in lookups.statuses" :key="status" :value="status">{{ status }}</option>
            </select>
          </label>
          <label class="form__field">
            <span class="form__label">Visibility</span>
            <select v-model="filterState.visibility">
              <option value="">All</option>
              <option v-for="visibility in lookups.visibilities" :key="visibility" :value="visibility">
                {{ visibility }}
              </option>
            </select>
          </label>
          <label class="form__field">
            <span class="form__label">Start from</span>
            <input v-model="filterState.startAfter" type="date" />
          </label>
          <label class="form__field">
            <span class="form__label">Start to</span>
            <input v-model="filterState.startBefore" type="date" />
          </label>
          <label v-if="canManage" class="form__field form__field--checkbox">
            <input v-model="filterState.includePast" type="checkbox" />
            <span>Include past</span>
          </label>
        </div>
        <div class="event-board__audience">
          <span class="form__label">Audience</span>
          <div class="chip-group">
            <button
              v-for="role in lookups.roles"
              :key="role.id"
              type="button"
              class="chip"
              :class="{ 'chip--active': selectedAudiences.has(role.id) }"
              @click="toggleAudience(role.id)"
            >
              {{ role.name }}
            </button>
          </div>
        </div>
        <div v-if="canManage" class="event-board__cta">
          <a class="btn btn--primary" href="/events/create">New event</a>
        </div>
      </div>
    </header>

    <div v-if="filteredEvents.length === 0" class="event-board__empty">
      <p>No events match your filters.</p>
    </div>

    <ol v-else class="event-timeline">
      <li v-for="event in filteredEvents" :key="event.id" class="event-timeline__item">
        <div class="event-timeline__marker"></div>
        <div class="event-timeline__content">
          <header class="event-timeline__header">
            <h2>
              <a :href="`/events/${event.id}`">{{ event.title }}</a>
            </h2>
            <div class="event-timeline__badges">
              <span class="badge" :class="statusClass(event.status)">{{ event.status }}</span>
              <span class="badge badge--ghost">{{ event.visibility }}</span>
            </div>
          </header>
          <p v-if="event.summary" class="event-timeline__summary">{{ event.summary }}</p>
          <dl class="event-timeline__meta">
            <div>
              <dt>Starts</dt>
              <dd><time :datetime="isoDate(event.startAt)">{{ formatDate(event.startAt) }}</time></dd>
            </div>
            <div v-if="event.endAt">
              <dt>Ends</dt>
              <dd><time :datetime="isoDate(event.endAt)">{{ formatDate(event.endAt) }}</time></dd>
            </div>
            <div v-if="event.location">
              <dt>Location</dt>
              <dd>{{ event.location }}</dd>
            </div>
          </dl>
          <footer class="event-timeline__footer">
            <span><strong>Audience:</strong> {{ formatAudience(event.audiences) }}</span>
            <div v-if="canManage" class="event-timeline__actions">
              <a :href="`/events/${event.id}/edit`" class="btn btn--small">Edit</a>
              <form
                method="post"
                :action="`/events/${event.id}/delete`"
                class="form form--inline"
                @submit="confirmDelete"
              >
                <input type="hidden" name="_csrf" :value="csrfToken" />
                <button type="submit" class="btn btn--small btn--danger">Delete</button>
              </form>
            </div>
          </footer>
        </div>
      </li>
    </ol>
  </section>
</template>

<script setup>
import { computed, reactive } from "vue";

const props = defineProps({
  events: { type: Array, default: () => [] },
  lookups: {
    type: Object,
    default: () => ({ statuses: [], visibilities: [], roles: [] }),
  },
  filters: { type: Object, default: () => ({}) },
  canManage: { type: Boolean, default: false },
  csrfToken: { type: String, default: "" },
});

const filterState = reactive({
  search: props.filters.search || "",
  status: props.filters.status || "",
  visibility: props.filters.visibility || "",
  startAfter: props.filters.startAfter || "",
  startBefore: props.filters.startBefore || "",
  includePast: Boolean(props.filters.includePast),
});

const selectedAudiences = reactive(new Set((props.filters.audienceRoleIds || []).map(Number)));

function toggleAudience(roleId) {
  if (selectedAudiences.has(roleId)) {
    selectedAudiences.delete(roleId);
  } else {
    selectedAudiences.add(roleId);
  }
}

function statusClass(status) {
  if (!status) {
    return "badge--status-draft";
  }
  return `badge--status-${status.toLowerCase()}`;
}

function isoDate(value) {
  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? "" : date.toISOString();
}

function formatDate(value) {
  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? "" : date.toLocaleString();
}

function formatAudience(audiences = []) {
  const names = audiences
    .map((audience) => (audience.role ? audience.role.name : null))
    .filter(Boolean);
  return names.length > 0 ? names.join(", ") : "All";
}

function confirmDelete(event) {
  if (!window.confirm("Delete this event?")) {
    event.preventDefault();
  }
}

const filteredEvents = computed(() => {
  const searchTerm = filterState.search.toLowerCase();
  const statusFilter = filterState.status;
  const visibilityFilter = filterState.visibility;
  const audienceFilter = selectedAudiences.size > 0 ? Array.from(selectedAudiences) : null;
  const now = new Date();
  const startAfter = filterState.startAfter ? new Date(filterState.startAfter) : null;
  const startBefore = filterState.startBefore ? new Date(filterState.startBefore) : null;

  return (props.events || [])
    .filter((event) => {
      if (searchTerm) {
        const haystack = `${event.title || ""} ${event.location || ""}`.toLowerCase();
        if (!haystack.includes(searchTerm)) {
          return false;
        }
      }

      if (statusFilter && event.status !== statusFilter) {
        return false;
      }

      if (visibilityFilter && event.visibility !== visibilityFilter) {
        return false;
      }

      if (!filterState.includePast) {
        const endAt = event.endAt ? new Date(event.endAt) : null;
        const startAt = event.startAt ? new Date(event.startAt) : null;
        const isPast = endAt ? endAt < now : startAt && startAt < now;
        if (isPast) {
          return false;
        }
      }

      if (startAfter && !Number.isNaN(startAfter.getTime())) {
        const startAt = new Date(event.startAt);
        if (!Number.isNaN(startAt.getTime()) && startAt < startAfter) {
          return false;
        }
      }

      if (startBefore && !Number.isNaN(startBefore.getTime())) {
        const startAt = new Date(event.startAt);
        if (!Number.isNaN(startAt.getTime()) && startAt > startBefore) {
          return false;
        }
      }

      if (audienceFilter) {
        const roleSet = new Set((event.audiences || []).map((audience) => audience.roleId));
        const hasAny = audienceFilter.some((roleId) => roleSet.has(roleId));
        if (!hasAny) {
          return false;
        }
      }

      return true;
    })
    .sort((a, b) => new Date(a.startAt).getTime() - new Date(b.startAt).getTime());
});
</script>

<style scoped>
.event-board {
  display: grid;
  gap: 2rem;
}

.event-board__header {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.event-board__actions {
  display: grid;
  gap: 1rem;
}

.event-board__filters {
  display: grid;
  gap: 1rem;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
}

.event-board__audience {
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

.event-board__empty {
  padding: 2rem;
  text-align: center;
  border-radius: var(--radius-lg);
  background: rgba(255, 255, 255, 0.8);
  border: 1px solid var(--color-soft-border);
}

.event-timeline {
  position: relative;
  padding-left: 1.5rem;
  display: grid;
  gap: 2rem;
  list-style: none;
}

.event-timeline::before {
  content: "";
  position: absolute;
  left: 0.5rem;
  top: 0.5rem;
  bottom: 0;
  width: 2px;
  background: rgba(79, 70, 229, 0.2);
}

.event-timeline__item {
  position: relative;
  padding-left: 1.5rem;
}

.event-timeline__marker {
  position: absolute;
  left: -1.1rem;
  top: 0.6rem;
  width: 12px;
  height: 12px;
  border-radius: 50%;
  background: var(--color-primary-500);
  box-shadow: 0 0 0 4px rgba(79, 70, 229, 0.15);
}

.event-timeline__content {
  background: rgba(255, 255, 255, 0.92);
  border-radius: var(--radius-lg);
  border: 1px solid var(--color-soft-border);
  box-shadow: var(--shadow-soft);
  padding: 1.5rem;
  display: grid;
  gap: 1rem;
}

.event-timeline__header {
  display: flex;
  justify-content: space-between;
  gap: 1rem;
  align-items: flex-start;
}

.event-timeline__badges {
  display: flex;
  gap: 0.5rem;
  align-items: center;
}

.event-timeline__meta {
  display: grid;
  gap: 0.5rem;
  font-size: 0.95rem;
  color: var(--color-muted);
}

.event-timeline__meta div {
  display: flex;
  gap: 0.35rem;
}

.event-timeline__meta dt {
  font-weight: 600;
}

.event-timeline__footer {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  justify-content: space-between;
  align-items: center;
}

.event-timeline__actions {
  display: inline-flex;
  gap: 0.75rem;
}
</style>
