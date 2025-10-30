<template>
  <section class="grid grid--two-column">
    <article class="card card--interactive">
      <header class="card__header">
        <div>
          <h1 class="card__title">{{ form.title || "Untitled book" }}</h1>
          <p class="card__hint">Update book metadata to keep the catalog current.</p>
        </div>
      </header>
      <form class="form" @submit.prevent="saveBook">
        <div class="grid grid--two-column">
          <label class="form__group">
            <span>Title</span>
            <input v-model.trim="form.title" type="text" required class="form__control" />
          </label>
          <label class="form__group">
            <span>Author</span>
            <input v-model.trim="form.author" type="text" class="form__control" />
          </label>
          <label class="form__group">
            <span>ISBN</span>
            <input v-model.trim="form.isbn" type="text" class="form__control" />
          </label>
          <label class="form__group">
            <span>Category</span>
            <select v-model="form.categoryId" class="form__control">
              <option value="">Uncategorized</option>
              <option v-for="category in categories" :key="category.id" :value="category.id">
                {{ category.name }}
              </option>
            </select>
          </label>
          <label class="form__group">
            <span>Published year</span>
            <input v-model.trim="form.publishedYear" type="number" min="0" class="form__control" />
          </label>
          <label class="form__group form__group--full">
            <span>Summary</span>
            <textarea v-model.trim="form.summary" rows="3" class="form__control"></textarea>
          </label>
        </div>
        <label class="form__group form__group--checkbox">
          <input v-model="form.isArchived" type="checkbox" />
          <span>Archive this title</span>
        </label>
        <div v-if="formError" class="alert alert--danger">{{ formError }}</div>
        <div v-if="formMessage" class="alert alert--success">{{ formMessage }}</div>
        <div class="form__actions">
          <button type="submit" class="btn btn--primary" :disabled="isSaving">
            {{ isSaving ? "Saving…" : "Save changes" }}
          </button>
          <button type="button" class="btn btn--ghost" @click="resetForm" :disabled="isSaving">
            Reset
          </button>
        </div>
      </form>
    </article>

    <LibraryLoanApp
      :copies="bookState.copies"
      :csrf-token="csrfToken"
      :book-id="bookState.id"
      @loan-issued="handleLoanIssued"
    />

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Copy inventory</h2>
          <p class="card__hint">Track each barcode assigned to this title.</p>
        </div>
      </header>
      <div v-if="bookState.copies.length" class="table-responsive">
        <table class="table">
          <thead>
            <tr>
              <th scope="col">Barcode</th>
              <th scope="col">Location</th>
              <th scope="col">Status</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="copy in bookState.copies" :key="copy.id">
              <td>{{ copy.barcode || copy.id }}</td>
              <td>{{ copy.location || "—" }}</td>
              <td>{{ copy.status }}</td>
            </tr>
          </tbody>
        </table>
      </div>
      <p v-else class="empty-state__title">No copies recorded.</p>
    </article>

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Add a copy</h2>
          <p class="card__hint">Register additional barcodes for circulation.</p>
        </div>
      </header>
      <form class="form" @submit.prevent="addCopy">
        <div class="grid grid--two-column">
          <label class="form__group">
            <span>Barcode</span>
            <input v-model.trim="copyForm.barcode" type="text" class="form__control" placeholder="Optional" />
          </label>
          <label class="form__group">
            <span>Location</span>
            <input v-model.trim="copyForm.location" type="text" class="form__control" />
          </label>
          <label class="form__group">
            <span>Status</span>
            <select v-model="copyForm.status" class="form__control">
              <option value="AVAILABLE">Available</option>
              <option value="LOANED">On loan</option>
              <option value="MAINTENANCE">Maintenance</option>
              <option value="LOST">Lost</option>
            </select>
          </label>
          <label class="form__group">
            <span>Acquired date</span>
            <input v-model="copyForm.acquiredAt" type="date" class="form__control" />
          </label>
        </div>
        <div v-if="copyError" class="alert alert--danger">{{ copyError }}</div>
        <div v-if="copyMessage" class="alert alert--success">{{ copyMessage }}</div>
        <div class="form__actions">
          <button type="submit" class="btn btn--primary" :disabled="copySubmitting">
            {{ copySubmitting ? "Saving…" : "Add copy" }}
          </button>
          <button type="button" class="btn btn--ghost" @click="resetCopyForm" :disabled="copySubmitting">
            Reset
          </button>
        </div>
      </form>
    </article>

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Active loans</h2>
          <p class="card__hint">Close out loans when items are returned.</p>
        </div>
      </header>
      <div v-if="activeLoanList.length" class="table-responsive">
        <table class="table">
          <thead>
            <tr>
              <th scope="col">Copy</th>
              <th scope="col">Borrower</th>
              <th scope="col">Issued</th>
              <th scope="col">Due</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="loan in activeLoanList" :key="loan.id">
              <td>{{ loan.copy?.barcode || loan.copy?.id || "—" }}</td>
              <td>{{ loan.student?.name || "—" }}</td>
              <td>{{ formatDate(loan.issuedAt) }}</td>
              <td>{{ formatDate(loan.dueAt) }}</td>
              <td>
                <button type="button" class="btn btn--ghost btn--small" @click="returnLoan(loan)" :disabled="returningLoanId === loan.id">
                  {{ returningLoanId === loan.id ? "Saving…" : "Mark returned" }}
                </button>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <p v-else class="empty-state__title">No active loans recorded.</p>
      <div v-if="loanError" class="alert alert--danger">{{ loanError }}</div>
    </article>

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Recent activity</h2>
          <p class="card__hint">Chronological list of loan events for this title.</p>
        </div>
        <button type="button" class="btn btn--ghost btn--small" @click="refreshState" :disabled="isRefreshing">
          {{ isRefreshing ? "Refreshing…" : "Refresh" }}
        </button>
      </header>
      <div v-if="recentLoanList.length" class="table-responsive">
        <table class="table">
          <thead>
            <tr>
              <th scope="col">Copy</th>
              <th scope="col">Borrower</th>
              <th scope="col">Issued</th>
              <th scope="col">Status</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="loan in recentLoanList" :key="loan.id">
              <td>{{ loan.copy?.barcode || loan.copy?.id || "—" }}</td>
              <td>{{ loan.student?.name || "—" }}</td>
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
      <p v-else class="empty-state__title">No historical activity yet.</p>
    </article>
  </section>
</template>

<script setup>
import { computed, reactive, ref, watch } from "vue";
import LibraryLoanApp from "./LibraryLoanApp.vue";

const props = defineProps({
  book: { type: Object, default: () => ({}) },
  activeLoans: { type: Array, default: () => [] },
  recentLoans: { type: Array, default: () => [] },
  categories: { type: Array, default: () => [] },
  csrfToken: { type: String, default: "" },
});

const bookState = reactive({ ...props.book, copies: props.book?.copies ?? [] });
const activeLoanList = ref([...props.activeLoans]);
const recentLoanList = ref([...props.recentLoans]);
const isSaving = ref(false);
const formError = ref("");
const formMessage = ref("");
const copyForm = reactive({ barcode: "", location: "", status: "AVAILABLE", acquiredAt: "" });
const copySubmitting = ref(false);
const copyError = ref("");
const copyMessage = ref("");
const loanError = ref("");
const returningLoanId = ref(null);
const isRefreshing = ref(false);

const form = reactive({
  id: bookState.id,
  title: bookState.title || "",
  author: bookState.author || "",
  isbn: bookState.isbn || "",
  categoryId: bookState.category?.id || "",
  publishedYear: bookState.publishedYear ? String(bookState.publishedYear) : "",
  summary: bookState.summary || "",
  isArchived: Boolean(bookState.isArchived),
});

watch(
  () => props.book,
  (value) => {
    Object.assign(bookState, value || {});
    if (value?.copies) {
      bookState.copies = value.copies;
    }
    form.id = bookState.id;
    form.title = bookState.title || "";
    form.author = bookState.author || "";
    form.isbn = bookState.isbn || "";
    form.categoryId = bookState.category?.id || "";
    form.publishedYear = bookState.publishedYear ? String(bookState.publishedYear) : "";
    form.summary = bookState.summary || "";
    form.isArchived = Boolean(bookState.isArchived);
  }
);

watch(
  () => props.activeLoans,
  (value) => {
    activeLoanList.value = Array.isArray(value) ? [...value] : [];
  }
);

watch(
  () => props.recentLoans,
  (value) => {
    recentLoanList.value = Array.isArray(value) ? [...value] : [];
  }
);

const categories = computed(() => props.categories ?? []);

function resetForm() {
  form.title = bookState.title || "";
  form.author = bookState.author || "";
  form.isbn = bookState.isbn || "";
  form.categoryId = bookState.category?.id || "";
  form.publishedYear = bookState.publishedYear ? String(bookState.publishedYear) : "";
  form.summary = bookState.summary || "";
  form.isArchived = Boolean(bookState.isArchived);
  formError.value = "";
  formMessage.value = "";
}

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

async function saveBook() {
  if (!form.title) {
    formError.value = "Title is required.";
    return;
  }
  isSaving.value = true;
  formError.value = "";
  formMessage.value = "";
  try {
    const response = await fetch(`/library/books/${bookState.id}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify(form),
    });
    if (!response.ok) {
      const data = await response.json().catch(() => ({ error: "" }));
      throw new Error(data.error || `Request failed with status ${response.status}`);
    }
    formMessage.value = "Book updated successfully.";
    await refreshState();
  } catch (error) {
    console.error(error);
    formError.value = error.message || "Unable to update book.";
  } finally {
    isSaving.value = false;
  }
}

async function addCopy() {
  copySubmitting.value = true;
  copyError.value = "";
  copyMessage.value = "";
  try {
    const response = await fetch(`/library/books/${bookState.id}/copies`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify(copyForm),
    });
    if (!response.ok) {
      const data = await response.json().catch(() => ({ error: "" }));
      throw new Error(data.error || `Request failed with status ${response.status}`);
    }
    copyMessage.value = "Copy added successfully.";
    resetCopyForm();
    await refreshState();
  } catch (error) {
    console.error(error);
    copyError.value = error.message || "Unable to add copy.";
  } finally {
    copySubmitting.value = false;
  }
}

function resetCopyForm() {
  copyForm.barcode = "";
  copyForm.location = "";
  copyForm.status = "AVAILABLE";
  copyForm.acquiredAt = "";
}

async function returnLoan(loan) {
  returningLoanId.value = loan.id;
  loanError.value = "";
  try {
    const response = await fetch(`/library/loans/${loan.id}/return`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify({ returnedAt: new Date().toISOString() }),
    });
    if (!response.ok) {
      const data = await response.json().catch(() => ({ error: "" }));
      throw new Error(data.error || `Request failed with status ${response.status}`);
    }
    await refreshState();
  } catch (error) {
    console.error(error);
    loanError.value = error.message || "Unable to mark loan as returned.";
  } finally {
    returningLoanId.value = null;
  }
}

async function refreshState() {
  isRefreshing.value = true;
  loanError.value = "";
  try {
    const response = await fetch(`/library/books/${bookState.id}`, {
      headers: { Accept: "application/json" },
    });
    if (!response.ok) {
      throw new Error(`Request failed with status ${response.status}`);
    }
    const data = await response.json();
    Object.assign(bookState, data.book || {});
    bookState.copies = data.book?.copies ?? [];
    activeLoanList.value = Array.isArray(data.activeLoans) ? data.activeLoans : [];
    recentLoanList.value = Array.isArray(data.recentLoans) ? data.recentLoans : [];
  } catch (error) {
    console.error(error);
    loanError.value = error.message || "Unable to refresh book details.";
  } finally {
    isRefreshing.value = false;
  }
}

async function handleLoanIssued() {
  await refreshState();
}
</script>
