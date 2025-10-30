<template>
  <section class="card">
    <header class="card__header">
      <div>
        <h2 class="card__title">Issue a loan</h2>
        <p class="card__hint">Choose an available copy and borrower.</p>
      </div>
    </header>

    <form class="form" @submit.prevent="submitLoan">
      <label class="form__group">
        <span>Available copy</span>
        <select v-model="selectedCopyId" class="form__control" required>
          <option value="">Select copy</option>
          <option v-for="copy in availableCopies" :key="copy.id" :value="copy.id">
            {{ copy.barcode || copy.id }} — {{ copy.location || "No location" }}
          </option>
        </select>
      </label>

      <label class="form__group">
        <span>Borrower</span>
        <input
          v-model.trim="searchTerm"
          type="search"
          class="form__control"
          placeholder="Search students by name or number"
          @input="handleSearchInput"
        />
      </label>

      <ul v-if="searchResults.length" class="list list--compact">
        <li
          v-for="result in searchResults"
          :key="result.id"
          class="list__option"
          @click="selectStudent(result)"
        >
          <div>
            <div class="list__heading">{{ result.label }}</div>
            <div class="list__description">{{ result.studentNumber || "No number" }}</div>
          </div>
          <button type="button" class="btn btn--ghost btn--small">Select</button>
        </li>
      </ul>

      <p v-if="selectedStudent" class="alert alert--info">
        Selected: <strong>{{ selectedStudent.label }}</strong>
      </p>

      <label class="form__group">
        <span>Due date</span>
        <input v-model="dueAt" type="date" class="form__control" />
      </label>

      <label class="form__group">
        <span>Notes</span>
        <textarea v-model.trim="notes" rows="2" class="form__control" placeholder="Optional" />
      </label>

      <div v-if="error" class="alert alert--danger">{{ error }}</div>

      <div class="form__actions">
        <button type="submit" class="btn btn--primary" :disabled="isSubmitting">
          {{ isSubmitting ? "Saving…" : "Record loan" }}
        </button>
        <button type="button" class="btn btn--ghost" @click="resetForm" :disabled="isSubmitting">
          Clear
        </button>
      </div>
    </form>
  </section>
</template>

<script setup>
import { computed, ref, watch } from "vue";

const props = defineProps({
  copies: { type: Array, default: () => [] },
  csrfToken: { type: String, default: "" },
  bookId: { type: Number, default: null },
  studentSearchEndpoint: { type: String, default: "/library/api/students" },
});

const emit = defineEmits(["loan-issued"]);

const searchTerm = ref("");
const selectedCopyId = ref("");
const selectedStudent = ref(null);
const searchResults = ref([]);
const isSearching = ref(false);
const isSubmitting = ref(false);
const error = ref("");
const dueAt = ref("");
const notes = ref("");
let searchTimeout = null;

const availableCopies = computed(() => (props.copies ?? []).filter((copy) => copy.status === "AVAILABLE"));

watch(
  () => props.copies,
  () => {
    if (!availableCopies.value.find((copy) => copy.id === Number(selectedCopyId.value))) {
      selectedCopyId.value = "";
    }
  }
);

function resetForm() {
  searchTerm.value = "";
  selectedCopyId.value = "";
  selectedStudent.value = null;
  searchResults.value = [];
  dueAt.value = "";
  notes.value = "";
  error.value = "";
}

function handleSearchInput() {
  if (searchTimeout) {
    window.clearTimeout(searchTimeout);
  }
  if (!searchTerm.value) {
    searchResults.value = [];
    selectedStudent.value = null;
    return;
  }
  searchTimeout = window.setTimeout(runSearch, 250);
}

async function runSearch() {
  if (!searchTerm.value) {
    return;
  }
  isSearching.value = true;
  try {
    const params = new URLSearchParams({ term: searchTerm.value });
    const response = await fetch(`${props.studentSearchEndpoint}?${params.toString()}`);
    if (!response.ok) {
      throw new Error(`Request failed with status ${response.status}`);
    }
    const data = await response.json();
    searchResults.value = Array.isArray(data.results) ? data.results : [];
  } catch (error_) {
    console.error(error_);
    error.value = "Unable to search for students.";
  } finally {
    isSearching.value = false;
  }
}

function selectStudent(result) {
  selectedStudent.value = result;
  searchResults.value = [];
  searchTerm.value = result.label;
}

async function submitLoan() {
  if (!selectedCopyId.value || !selectedStudent.value) {
    error.value = "Select a copy and borrower before recording the loan.";
    return;
  }
  isSubmitting.value = true;
  error.value = "";
  try {
    const response = await fetch("/library/loans", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify({
        copyId: selectedCopyId.value,
        studentId: selectedStudent.value.id,
        dueAt: dueAt.value || undefined,
        bookId: props.bookId,
        notes: notes.value || undefined,
      }),
    });
    if (!response.ok) {
      const data = await response.json().catch(() => ({ error: "" }));
      throw new Error(data.error || `Request failed with status ${response.status}`);
    }
    const data = await response.json();
    emit("loan-issued", data.loan);
    resetForm();
  } catch (error_) {
    console.error(error_);
    error.value = error_.message || "Unable to record loan.";
  } finally {
    isSubmitting.value = false;
  }
}
</script>
