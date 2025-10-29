<template>
  <section class="card card--interactive">
    <header class="card__header">
      <div>
        <h1 class="card__title">Fees workspace</h1>
        <p class="card__subtitle">
          Manage fee categories, review activity, and locate student ledgers.
        </p>
      </div>
    </header>

    <div class="grid grid--two-column">
      <article class="card">
        <header class="card__header">
          <div>
            <h2 class="card__title">Student search</h2>
            <p class="card__hint">Search by name or student number.</p>
          </div>
        </header>
        <form class="form" @submit.prevent="runSearch">
          <label class="form__group">
            <span>Student</span>
            <input
              v-model.trim="searchTerm"
              type="search"
              name="term"
              class="form__control"
              placeholder="Start typing to search"
            />
          </label>
          <div class="form__actions">
            <button type="submit" class="btn btn--primary" :disabled="isSearching">
              {{ isSearching ? "Searching…" : "Search" }}
            </button>
            <button type="button" class="btn btn--ghost" @click="resetSearch" :disabled="isSearching">
              Clear
            </button>
          </div>
        </form>
        <div v-if="searchError" class="alert alert--danger">
          {{ searchError }}
        </div>
        <ul v-if="searchResults.length" class="list list--compact">
          <li v-for="result in searchResults" :key="result.id" class="list__option">
            <div>
              <div class="list__heading">
                <strong>{{ result.label }}</strong>
                <span class="badge badge--info">{{ result.studentNumber || "No number" }}</span>
              </div>
            </div>
            <a :href="`/fees/ledger/${result.id}`" class="btn btn--ghost btn--small">Open ledger</a>
          </li>
        </ul>
        <p v-else-if="hasSearched && !isSearching" class="empty-state__title">
          No students found. Try another search term.
        </p>
      </article>

      <article class="card">
        <header class="card__header">
          <div>
            <h2 class="card__title">Recent ledger activity</h2>
            <p class="card__hint">Latest 10 entries across all students.</p>
          </div>
        </header>
        <div v-if="recentEntries.length" class="table-responsive">
          <table class="table">
            <thead>
              <tr>
                <th scope="col">Date</th>
                <th scope="col">Student</th>
                <th scope="col">Type</th>
                <th scope="col">Description</th>
                <th scope="col" class="table__cell--numeric">Amount</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="entry in recentEntries" :key="entry.id">
                <td>{{ formatDate(entry.entryDate) }}</td>
                <td>{{ entry.student?.name ?? "—" }}</td>
                <td>{{ entry.entryType }}</td>
                <td>{{ entry.description || "—" }}</td>
                <td class="table__cell--numeric">{{ formatCurrency(entry.amount) }}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <p v-else class="empty-state__title">No ledger entries recorded yet.</p>
      </article>
    </div>

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Fee categories</h2>
          <p class="card__hint">Active and archived billing categories.</p>
        </div>
      </header>
      <div v-if="categories.length" class="table-responsive">
        <table class="table">
          <thead>
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Code</th>
              <th scope="col" class="table__cell--numeric">Default amount</th>
              <th scope="col">Status</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="category in categories" :key="category.id">
              <td>{{ category.name }}</td>
              <td>{{ category.code || "—" }}</td>
              <td class="table__cell--numeric">
                {{ category.defaultAmount ? formatCurrency(category.defaultAmount) : "—" }}
              </td>
              <td>
                <span :class="['badge', category.isActive ? 'badge--success' : 'badge--neutral']">
                  {{ category.isActive ? "Active" : "Archived" }}
                </span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <p v-else class="empty-state__title">No categories configured.</p>
    </article>
  </section>
</template>

<script setup>
import { computed, ref } from "vue";

const props = defineProps({
  categories: { type: Array, default: () => [] },
  recentEntries: { type: Array, default: () => [] },
});

const searchTerm = ref("");
const isSearching = ref(false);
const hasSearched = ref(false);
const searchResults = ref([]);
const searchError = ref("");

const recentEntries = computed(() => props.recentEntries ?? []);
const categories = computed(() => props.categories ?? []);

function formatCurrency(value) {
  const amount = Number.parseFloat(value ?? 0);
  return amount.toLocaleString(undefined, { style: "currency", currency: "USD" });
}

function formatDate(date) {
  const parsed = new Date(date);
  if (Number.isNaN(parsed.getTime())) {
    return "—";
  }
  return parsed.toLocaleDateString();
}

async function runSearch() {
  if (!searchTerm.value) {
    searchResults.value = [];
    hasSearched.value = true;
    return;
  }
  isSearching.value = true;
  searchError.value = "";
  try {
    const params = new URLSearchParams({ term: searchTerm.value });
    const response = await fetch(`/fees/api/students?${params.toString()}`);
    if (!response.ok) {
      throw new Error(`Request failed with status ${response.status}`);
    }
    const data = await response.json();
    searchResults.value = Array.isArray(data.results) ? data.results : [];
  } catch (error) {
    console.error(error);
    searchError.value = "Unable to complete search. Please try again.";
  } finally {
    isSearching.value = false;
    hasSearched.value = true;
  }
}

function resetSearch() {
  searchTerm.value = "";
  searchResults.value = [];
  hasSearched.value = false;
  searchError.value = "";
}
</script>
