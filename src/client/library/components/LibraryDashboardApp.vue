<template>
  <section class="card card--interactive">
    <header class="card__header">
      <div>
        <h1 class="card__title">Library workspace</h1>
        <p class="card__subtitle">Search the catalog, review activity, and add new titles.</p>
      </div>
    </header>

    <div class="grid grid--two-column">
      <article class="card">
        <header class="card__header">
          <div>
            <h2 class="card__title">Catalog search</h2>
            <p class="card__hint">Filter by keyword or category.</p>
          </div>
        </header>
        <form class="form" @submit.prevent="loadBooks">
          <label class="form__group">
            <span>Keywords</span>
            <input
              v-model.trim="searchTerm"
              type="search"
              name="term"
              class="form__control"
              placeholder="Title, author, or ISBN"
            />
          </label>
          <label class="form__group">
            <span>Category</span>
            <select v-model="selectedCategory" class="form__control">
              <option value="">All categories</option>
              <option v-for="category in categories" :key="category.id" :value="category.id">
                {{ category.name }}
              </option>
            </select>
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
        <div v-if="searchError" class="alert alert--danger">{{ searchError }}</div>
        <div v-if="bookResults.length" class="table-responsive">
          <table class="table">
            <thead>
              <tr>
                <th scope="col">Title</th>
                <th scope="col">Author</th>
                <th scope="col">Category</th>
                <th scope="col">Available</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="book in bookResults" :key="book.id">
                <th scope="row">
                  <a :href="`/library/books/${book.id}`">{{ book.title }}</a>
                </th>
                <td>{{ book.author || "—" }}</td>
                <td>{{ book.category?.name ?? "Uncategorized" }}</td>
                <td>
                  <strong>{{ book.copySummary.available }}</strong>
                  of {{ book.copySummary.total }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <p v-else-if="!isSearching" class="empty-state__title">No books matched your filters.</p>
      </article>

      <article class="card">
        <header class="card__header">
          <div>
            <h2 class="card__title">Recent loan activity</h2>
            <p class="card__hint">Latest 10 check-outs and returns.</p>
          </div>
          <button type="button" class="btn btn--ghost btn--small" @click="loadRecentLoans" :disabled="refreshingLoans">
            {{ refreshingLoans ? "Refreshing…" : "Refresh" }}
          </button>
        </header>
        <div v-if="recentLoans.length" class="table-responsive">
          <table class="table">
            <thead>
              <tr>
                <th scope="col">Title</th>
                <th scope="col">Borrower</th>
                <th scope="col">Issued</th>
                <th scope="col">Status</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="loan in recentLoans" :key="loan.id">
                <td>{{ loan.book?.title ?? "—" }}</td>
                <td>{{ loan.student?.name ?? "—" }}</td>
                <td>{{ formatDate(loan.issuedAt) }}</td>
                <td>
                  <span :class="['badge', loan.returnedAt ? 'badge--success' : 'badge--info']">
                    {{ loan.returnedAt ? `Returned ${formatDate(loan.returnedAt)}` : loan.dueAt ? `Due ${formatDate(loan.dueAt)}` : 'On loan' }}
                  </span>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
        <p v-else class="empty-state__title">No recent loan records.</p>
      </article>
    </div>

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Add a new book</h2>
          <p class="card__hint">Capture core metadata to grow the catalog.</p>
        </div>
        <button type="button" class="btn btn--ghost btn--small" @click="toggleCreate">
          {{ showCreateForm ? "Hide form" : "Add book" }}
        </button>
      </header>
      <form v-if="showCreateForm" class="form" @submit.prevent="submitBook">
        <div class="grid grid--two-column">
          <label class="form__group">
            <span>Title</span>
            <input v-model.trim="newBook.title" type="text" required class="form__control" />
          </label>
          <label class="form__group">
            <span>Author</span>
            <input v-model.trim="newBook.author" type="text" class="form__control" />
          </label>
          <label class="form__group">
            <span>ISBN</span>
            <input v-model.trim="newBook.isbn" type="text" class="form__control" />
          </label>
          <label class="form__group">
            <span>Category</span>
            <select v-model="newBook.categoryId" class="form__control">
              <option value="">Select category</option>
              <option v-for="category in categories" :key="category.id" :value="category.id">
                {{ category.name }}
              </option>
            </select>
          </label>
          <label class="form__group">
            <span>Published year</span>
            <input v-model.trim="newBook.publishedYear" type="number" min="0" class="form__control" />
          </label>
          <label class="form__group form__group--full">
            <span>Summary</span>
            <textarea v-model.trim="newBook.summary" rows="3" class="form__control"></textarea>
          </label>
        </div>
        <div v-if="createError" class="alert alert--danger">{{ createError }}</div>
        <div class="form__actions">
          <button type="submit" class="btn btn--primary" :disabled="isCreating">
            {{ isCreating ? "Saving…" : "Save book" }}
          </button>
          <button type="button" class="btn btn--ghost" @click="resetForm" :disabled="isCreating">
            Reset
          </button>
        </div>
      </form>
      <p v-else class="empty-state__title">Use the button above to capture a new title.</p>
    </article>
  </section>
</template>

<script setup>
import { computed, reactive, ref, watch } from "vue";

const props = defineProps({
  categories: { type: Array, default: () => [] },
  books: { type: Array, default: () => [] },
  recentLoans: { type: Array, default: () => [] },
  csrfToken: { type: String, default: "" },
});

const categories = computed(() => props.categories ?? []);
const searchTerm = ref("");
const selectedCategory = ref("");
const isSearching = ref(false);
const searchError = ref("");
const refreshingLoans = ref(false);
const bookResults = ref([]);
const recentLoans = ref([]);
const showCreateForm = ref(false);
const createError = ref("");
const isCreating = ref(false);
const newBook = reactive({
  title: "",
  author: "",
  isbn: "",
  categoryId: "",
  publishedYear: "",
  summary: "",
});

watch(
  () => props.books,
  (value) => {
    bookResults.value = Array.isArray(value) ? value : [];
  },
  { immediate: true }
);

watch(
  () => props.recentLoans,
  (value) => {
    recentLoans.value = Array.isArray(value) ? value : [];
  },
  { immediate: true }
);

function formatDate(value) {
  const date = new Date(value);
  if (Number.isNaN(date.getTime())) {
    return "—";
  }
  return date.toLocaleDateString();
}

async function loadBooks() {
  isSearching.value = true;
  searchError.value = "";
  try {
    const params = new URLSearchParams();
    if (searchTerm.value) {
      params.set("term", searchTerm.value);
    }
    if (selectedCategory.value) {
      params.set("categoryId", String(selectedCategory.value));
    }
    const response = await fetch(`/library/api/books?${params.toString()}`);
    if (!response.ok) {
      throw new Error(`Request failed with status ${response.status}`);
    }
    const data = await response.json();
    bookResults.value = Array.isArray(data.results) ? data.results : [];
  } catch (error) {
    console.error(error);
    searchError.value = "Unable to load books. Please try again.";
  } finally {
    isSearching.value = false;
  }
}

function resetSearch() {
  searchTerm.value = "";
  selectedCategory.value = "";
  bookResults.value = Array.isArray(props.books) ? props.books : [];
  searchError.value = "";
}

function toggleCreate() {
  showCreateForm.value = !showCreateForm.value;
}

function resetForm() {
  newBook.title = "";
  newBook.author = "";
  newBook.isbn = "";
  newBook.categoryId = "";
  newBook.publishedYear = "";
  newBook.summary = "";
  createError.value = "";
}

async function submitBook() {
  if (!newBook.title) {
    createError.value = "Title is required.";
    return;
  }
  isCreating.value = true;
  createError.value = "";
  try {
    const response = await fetch("/library/books", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify(newBook),
    });
    if (!response.ok) {
      const data = await response.json().catch(() => ({ error: "" }));
      throw new Error(data.error || `Request failed with status ${response.status}`);
    }
    resetForm();
    showCreateForm.value = false;
    await loadBooks();
    await loadRecentLoans();
  } catch (error) {
    console.error(error);
    createError.value = error.message || "Unable to save book.";
  } finally {
    isCreating.value = false;
  }
}

async function loadRecentLoans() {
  refreshingLoans.value = true;
  try {
    const response = await fetch("/library/api/loans/recent?limit=10");
    if (!response.ok) {
      throw new Error(`Request failed with status ${response.status}`);
    }
    const data = await response.json();
    recentLoans.value = Array.isArray(data.loans) ? data.loans : [];
  } catch (error) {
    console.error(error);
  } finally {
    refreshingLoans.value = false;
  }
}
</script>
