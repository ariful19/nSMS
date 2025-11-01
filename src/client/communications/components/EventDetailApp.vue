<template>
  <article class="event-detail">
    <header class="event-detail__header">
      <div>
        <h1 class="event-detail__title">{{ event.title }}</h1>
        <p v-if="event.summary" class="event-detail__summary">{{ event.summary }}</p>
        <div class="event-detail__meta">
          <span class="badge" :class="statusClass(event.status)">{{ event.status }}</span>
          <span class="badge badge--ghost">{{ event.visibility }}</span>
          <span v-if="event.startAt">Starts {{ formatDate(event.startAt) }}</span>
          <span v-if="event.endAt">Ends {{ formatDate(event.endAt) }}</span>
        </div>
      </div>
      <div v-if="canManage" class="event-detail__actions">
        <a :href="`/events/${event.id}/edit`" class="btn">Edit</a>
        <form
          method="post"
          :action="`/events/${event.id}/delete`"
          class="form form--inline"
          @submit="confirmDelete"
        >
          <input type="hidden" name="_csrf" :value="csrfToken" />
          <button type="submit" class="btn btn--danger">Delete</button>
        </form>
      </div>
    </header>

    <section class="event-detail__content">
      <p v-if="event.location" class="event-detail__location">📍 <strong>{{ event.location }}</strong></p>
      <article v-if="event.description" class="event-detail__description" v-html="event.description"></article>
      <dl class="event-detail__schedule">
        <div>
          <dt>Starts</dt>
          <dd><time :datetime="isoDate(event.startAt)">{{ formatDate(event.startAt) }}</time></dd>
        </div>
        <div v-if="event.endAt">
          <dt>Ends</dt>
          <dd><time :datetime="isoDate(event.endAt)">{{ formatDate(event.endAt) }}</time></dd>
        </div>
        <div v-if="event.registrationDeadline">
          <dt>RSVP by</dt>
          <dd>{{ formatDate(event.registrationDeadline, true) }}</dd>
        </div>
      </dl>
    </section>

    <section v-if="event.audiences && event.audiences.length" class="event-detail__audience">
      <h2>Audience</h2>
      <ul>
        <li v-for="role in audienceNames" :key="role">{{ role }}</li>
      </ul>
    </section>

    <section class="event-detail__rsvp">
      <h2>Your RSVP</h2>
      <form ref="rsvpForm" method="post" :action="`/events/${event.id}/rsvp`" class="form form--stacked">
        <input type="hidden" name="_csrf" :value="csrfToken" />
        <label class="form__field">
          <span class="form__label">Response</span>
          <select v-model="rsvp.status" name="status">
            <option v-for="status in lookups.rsvpStatuses" :key="status" :value="status">{{ status }}</option>
          </select>
        </label>
        <label class="form__field">
          <span class="form__label">Notes</span>
          <textarea v-model="rsvp.notes" name="notes" rows="2"></textarea>
        </label>
        <div class="form__actions">
          <button type="submit" class="btn btn--primary">Save response</button>
        </div>
      </form>
      <form
        v-if="registration"
        method="post"
        :action="`/events/${event.id}/rsvp/cancel`"
        class="form form--inline event-detail__cancel"
      >
        <input type="hidden" name="_csrf" :value="csrfToken" />
        <button type="submit" class="btn btn--link">Cancel RSVP</button>
      </form>
    </section>
  </article>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  event: { type: Object, required: true },
  registration: { type: Object, default: null },
  lookups: {
    type: Object,
    default: () => ({ rsvpStatuses: ["GOING", "INTERESTED", "DECLINED"] }),
  },
  canManage: { type: Boolean, default: false },
  csrfToken: { type: String, default: "" },
});

const rsvpForm = ref(null);
const rsvp = reactive({
  status: (props.registration && props.registration.status) || props.lookups.rsvpStatuses?.[0] || "GOING",
  notes: (props.registration && props.registration.notes) || "",
});

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

function isoDate(value) {
  const date = new Date(value);
  return Number.isNaN(date.getTime()) ? "" : date.toISOString();
}

function confirmDelete(event) {
  if (!window.confirm("Delete this event?")) {
    event.preventDefault();
  }
}

const audienceNames = computed(() => {
  const names = (props.event.audiences || [])
    .map((audience) => (audience.role ? audience.role.name : null))
    .filter(Boolean);
  return names.length > 0 ? names : ["All roles"];
});
</script>

<style scoped>
.event-detail {
  display: grid;
  gap: 1.5rem;
}

.event-detail__header {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.event-detail__meta {
  display: flex;
  flex-wrap: wrap;
  gap: 0.75rem;
  align-items: center;
  color: var(--color-muted);
}

.event-detail__actions {
  display: inline-flex;
  gap: 0.75rem;
}

.event-detail__content {
  background: rgba(255, 255, 255, 0.92);
  border-radius: var(--radius-lg);
  border: 1px solid var(--color-soft-border);
  box-shadow: var(--shadow-soft);
  padding: 1.5rem;
  display: grid;
  gap: 1rem;
}

.event-detail__description {
  line-height: 1.7;
}

.event-detail__schedule {
  display: grid;
  gap: 0.5rem;
  color: var(--color-muted);
}

.event-detail__schedule div {
  display: flex;
  gap: 0.5rem;
}

.event-detail__audience {
  padding: 1rem 1.5rem;
  border-left: 4px solid var(--color-primary-500);
  background: rgba(79, 70, 229, 0.08);
  border-radius: var(--radius-lg);
}

.event-detail__rsvp {
  padding: 1.5rem;
  border: 1px solid var(--color-soft-border);
  border-radius: var(--radius-lg);
  background: rgba(255, 255, 255, 0.95);
  display: grid;
  gap: 1rem;
}

.event-detail__cancel {
  justify-content: flex-start;
}
</style>
