<template>
  <div class="dashboard-communications">
    <section class="dashboard-panel">
      <header class="dashboard-panel__header">
        <h2>Latest notices</h2>
        <a class="dashboard-panel__link" href="/notices">View all</a>
      </header>
      <p v-if="notices.length === 0" class="dashboard-panel__empty">No recent notices.</p>
      <ul v-else class="dashboard-list">
        <li v-for="notice in notices" :key="notice.id" class="dashboard-list__item">
          <a :href="`/notices/${notice.id}`" class="dashboard-list__title">{{ notice.title }}</a>
          <p v-if="notice.summary" class="dashboard-list__summary">{{ notice.summary }}</p>
          <div class="dashboard-list__meta">
            <span class="badge" :class="statusClass(notice.status)">{{ notice.status }}</span>
            <span v-if="notice.publishAt">{{ formatDate(notice.publishAt) }}</span>
          </div>
        </li>
      </ul>
    </section>

    <section class="dashboard-panel">
      <header class="dashboard-panel__header">
        <h2>Upcoming events</h2>
        <a class="dashboard-panel__link" href="/events">View calendar</a>
      </header>
      <p v-if="events.length === 0" class="dashboard-panel__empty">No upcoming events.</p>
      <ul v-else class="dashboard-list">
        <li v-for="event in events" :key="event.id" class="dashboard-list__item">
          <a :href="`/events/${event.id}`" class="dashboard-list__title">{{ event.title }}</a>
          <p v-if="event.summary" class="dashboard-list__summary">{{ event.summary }}</p>
          <div class="dashboard-list__meta">
            <span class="badge" :class="statusClass(event.status)">{{ event.status }}</span>
            <span>{{ formatDate(event.startAt) }}</span>
          </div>
        </li>
      </ul>
    </section>
  </div>
</template>

<script setup>
const props = defineProps({
  notices: { type: Array, default: () => [] },
  events: { type: Array, default: () => [] },
});

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
  return Number.isNaN(date.getTime()) ? "" : date.toLocaleDateString();
}
</script>

<style scoped>
.dashboard-communications {
  display: grid;
  gap: 1.5rem;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
}

.dashboard-panel {
  background: rgba(255, 255, 255, 0.92);
  border-radius: var(--radius-lg);
  border: 1px solid var(--color-soft-border);
  box-shadow: var(--shadow-soft);
  padding: 1.5rem;
  display: grid;
  gap: 1rem;
}

.dashboard-panel__header {
  display: flex;
  justify-content: space-between;
  align-items: baseline;
  gap: 1rem;
}

.dashboard-panel__link {
  font-size: 0.95rem;
  color: var(--color-primary-600);
}

.dashboard-panel__empty {
  color: var(--color-muted);
}

.dashboard-list {
  list-style: none;
  display: grid;
  gap: 1rem;
  margin: 0;
  padding: 0;
}

.dashboard-list__item {
  display: grid;
  gap: 0.35rem;
}

.dashboard-list__title {
  font-weight: 600;
  text-decoration: none;
  color: inherit;
}

.dashboard-list__summary {
  color: var(--color-muted);
  font-size: 0.95rem;
}

.dashboard-list__meta {
  display: flex;
  gap: 0.75rem;
  align-items: center;
  color: var(--color-muted);
  font-size: 0.9rem;
}
</style>
