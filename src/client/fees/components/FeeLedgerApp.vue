<template>
  <section class="stack">
    <article class="card card--interactive">
      <header class="card__header">
        <div>
          <h1 class="card__title">{{ studentName }}</h1>
          <p class="card__subtitle">
            Student number: <strong>{{ ledgerState.student?.studentNumber || "—" }}</strong>
          </p>
        </div>
        <div class="card__actions">
          <a class="btn btn--ghost" href="/fees">Back to fees</a>
        </div>
      </header>
      <div class="grid grid--three-column">
        <div class="summary">
          <h3>Charges</h3>
          <p>{{ formatCurrency(ledgerState.totals?.charges || 0) }}</p>
        </div>
        <div class="summary">
          <h3>Payments</h3>
          <p>{{ formatCurrency(ledgerState.totals?.payments || 0) }}</p>
        </div>
        <div class="summary">
          <h3>Adjustments</h3>
          <p>{{ formatCurrency(ledgerState.totals?.adjustments || 0) }}</p>
        </div>
      </div>
      <div class="summary summary--balance">
        <h3>Balance</h3>
        <p>{{ formatCurrency(ledgerState.balance || 0) }}</p>
      </div>
      <form class="form filters" @submit.prevent="applyFilters">
        <div class="form__row">
          <label class="form__group">
            <span>Start date</span>
            <input type="date" v-model="filterState.startDate" class="form__control" />
          </label>
          <label class="form__group">
            <span>End date</span>
            <input type="date" v-model="filterState.endDate" class="form__control" />
          </label>
          <label class="form__group">
            <span>Category</span>
            <select v-model="filterState.categoryId" class="form__control">
              <option value="">All categories</option>
              <option
                v-for="category in categories"
                :key="category.id"
                :value="String(category.id)"
              >
                {{ category.name }}
              </option>
            </select>
          </label>
        </div>
        <div class="form__actions">
          <button type="submit" class="btn btn--primary" :disabled="isLoading">
            {{ isLoading ? "Updating…" : "Apply filters" }}
          </button>
          <button type="button" class="btn btn--ghost" @click="resetFilters" :disabled="isLoading">
            Reset
          </button>
        </div>
      </form>
    </article>

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Record transaction</h2>
          <p class="card__hint">Capture new charges, payments, or adjustments.</p>
        </div>
      </header>

      <div class="transaction-tabs">
        <button
          v-for="tab in tabs"
          :key="tab.id"
          type="button"
          class="transaction-tabs__tab"
          :class="{ 'transaction-tabs__tab--active': activeTab === tab.id }"
          @click="switchTab(tab.id)"
        >
          {{ tab.label }}
        </button>
      </div>

      <form class="form" @submit.prevent="submitTransaction">
        <div class="transaction-items">
          <div v-for="(item, index) in lineItems" :key="index" class="transaction-item">
            <div class="form__row">
              <label class="form__group">
                <span>Category</span>
                <select v-model="item.categoryId" class="form__control">
                  <option value="">Uncategorized</option>
                  <option
                    v-for="category in categories"
                    :key="category.id"
                    :value="String(category.id)"
                  >
                    {{ category.name }}
                  </option>
                </select>
              </label>
              <label class="form__group">
                <span>Amount</span>
                <input
                  v-model="item.amount"
                  type="number"
                  step="0.01"
                  :min="activeTab === 'adjustment' ? null : 0"
                  class="form__control"
                  required
                />
              </label>
              <label class="form__group">
                <span>Date</span>
                <input v-model="item.entryDate" type="date" class="form__control" required />
              </label>
            </div>
            <label class="form__group">
              <span>Description</span>
              <input v-model="item.description" type="text" class="form__control" placeholder="Optional" />
            </label>
            <div class="transaction-item__actions">
              <button type="button" class="btn btn--link" @click="removeLineItem(index)" v-if="lineItems.length > 1">
                Remove
              </button>
            </div>
          </div>
        </div>

        <div class="form__actions">
          <button type="button" class="btn btn--ghost" @click="addLineItem">
            Add another line
          </button>
        </div>

        <label v-if="activeTab === 'payment'" class="form__group">
          <span>Payment method</span>
          <select v-model="paymentMethod" class="form__control">
            <option value="">Select method</option>
            <option value="CASH">Cash</option>
            <option value="CHECK">Check</option>
            <option value="TRANSFER">Bank transfer</option>
            <option value="MOBILE">Mobile payment</option>
            <option value="OTHER">Other</option>
          </select>
        </label>

        <div class="form__actions">
          <button type="submit" class="btn btn--primary" :disabled="isSubmitting">
            {{ submitLabel }}
          </button>
          <button type="button" class="btn btn--ghost" @click="resetForm" :disabled="isSubmitting">
            Clear form
          </button>
        </div>
      </form>

      <div v-if="alerts.length" class="alerts">
        <div v-for="(alert, index) in alerts" :key="index" :class="['alert', `alert--${alert.type}`]">
          {{ alert.message }}
          <template v-if="alert.receiptNumber">
            –
            <a :href="`/fees/receipt/${alert.receiptNumber}`" class="alert__link">View receipt</a>
          </template>
        </div>
      </div>
    </article>

    <article class="card">
      <header class="card__header">
        <div>
          <h2 class="card__title">Ledger entries</h2>
          <p class="card__hint">Ordered chronologically with running balance.</p>
        </div>
      </header>
      <div v-if="ledgerState.entries?.length" class="table-responsive">
        <table class="table">
          <thead>
            <tr>
              <th scope="col">Date</th>
              <th scope="col">Type</th>
              <th scope="col">Category</th>
              <th scope="col">Description</th>
              <th scope="col" class="table__cell--numeric">Amount</th>
              <th scope="col" class="table__cell--numeric">Balance</th>
              <th scope="col">Receipt</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="entry in ledgerState.entries" :key="entry.id">
              <td>{{ formatDate(entry.entryDate) }}</td>
              <td>{{ entry.entryType }}</td>
              <td>{{ entry.category?.name ?? "—" }}</td>
              <td>{{ entry.description || "—" }}</td>
              <td class="table__cell--numeric">
                <span :class="entry.entryType === 'PAYMENT' ? 'text--success' : ''">
                  {{ formatCurrency(entry.amount) }}
                </span>
              </td>
              <td class="table__cell--numeric">{{ formatCurrency(entry.runningBalance) }}</td>
              <td>
                <a v-if="entry.receiptNumber" :href="`/fees/receipt/${entry.receiptNumber}`" class="btn btn--link">
                  {{ entry.receiptNumber }}
                </a>
                <span v-else>—</span>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <p v-else class="empty-state__title">No ledger entries for the selected filters.</p>
    </article>
  </section>
</template>

<script setup>
import { computed, reactive, ref } from "vue";

const props = defineProps({
  ledger: { type: Object, default: () => ({ entries: [], totals: {}, student: null }) },
  categories: { type: Array, default: () => [] },
  filters: { type: Object, default: () => ({}) },
  endpoints: { type: Object, default: () => ({}) },
  csrfToken: { type: String, default: "" },
});

const ledgerState = reactive(JSON.parse(JSON.stringify(props.ledger ?? {})));
const categories = computed(() => props.categories ?? []);
const filterState = reactive({
  startDate: props.filters?.startDate || "",
  endDate: props.filters?.endDate || "",
  categoryId: props.filters?.categoryId || "",
});
const activeTab = ref("charge");
const lineItems = ref([createLineItem()]);
const paymentMethod = ref("");
const alerts = ref([]);
const isSubmitting = ref(false);
const isLoading = ref(false);

const tabs = [
  { id: "charge", label: "Charge" },
  { id: "payment", label: "Payment" },
  { id: "adjustment", label: "Adjustment" },
];

const studentName = computed(() => {
  const person = ledgerState.student?.person;
  if (!person) {
    return "Student ledger";
  }
  return `${person.firstName ?? ""} ${person.lastName ?? ""}`.trim();
});

const submitLabel = computed(() => {
  if (isSubmitting.value) {
    return "Saving…";
  }
  if (activeTab.value === "payment") {
    return "Record payment";
  }
  if (activeTab.value === "adjustment") {
    return "Record adjustment";
  }
  return "Record charge";
});

function formatCurrency(value) {
  const amount = Number.parseFloat(value ?? 0);
  return amount.toLocaleString(undefined, { style: "currency", currency: "USD" });
}

function formatDate(value) {
  const parsed = new Date(value);
  if (Number.isNaN(parsed.getTime())) {
    return "—";
  }
  return parsed.toLocaleDateString();
}

function createLineItem() {
  const today = new Date();
  return {
    categoryId: "",
    amount: "",
    entryDate: today.toISOString().slice(0, 10),
    description: "",
  };
}

function addLineItem() {
  lineItems.value.push(createLineItem());
}

function removeLineItem(index) {
  if (lineItems.value.length === 1) {
    return;
  }
  lineItems.value.splice(index, 1);
}

function resetForm() {
  lineItems.value = [createLineItem()];
  paymentMethod.value = "";
}

function resetFilters() {
  filterState.startDate = "";
  filterState.endDate = "";
  filterState.categoryId = "";
  applyFilters();
}

function switchTab(tabId) {
  activeTab.value = tabId;
  resetForm();
}

function addAlert(type, message, extra = {}) {
  alerts.value.unshift({ type, message, ...extra });
  if (alerts.value.length > 5) {
    alerts.value.pop();
  }
}

async function refreshLedger() {
  isLoading.value = true;
  try {
    const params = new URLSearchParams();
    if (filterState.startDate) {
      params.set("startDate", filterState.startDate);
    }
    if (filterState.endDate) {
      params.set("endDate", filterState.endDate);
    }
    if (filterState.categoryId) {
      params.set("categoryId", filterState.categoryId);
    }
    const response = await fetch(`${window.location.pathname}?${params.toString()}`, {
      headers: { Accept: "application/json" },
    });
    if (!response.ok) {
      throw new Error("Unable to refresh ledger");
    }
    const data = await response.json();
    Object.assign(ledgerState, data.ledger ?? {});
  } catch (error) {
    console.error(error);
    addAlert("danger", "Failed to refresh ledger. Please reload the page.");
  } finally {
    isLoading.value = false;
  }
}

function applyFilters() {
  refreshLedger();
}

function serializeLineItems() {
  return lineItems.value
    .filter((item) => item.amount)
    .map((item) => ({
      categoryId: item.categoryId || null,
      amount: item.amount,
      entryDate: item.entryDate,
      description: item.description,
    }));
}

async function submitTransaction() {
  const items = serializeLineItems();
  if (!items.length) {
    addAlert("warning", "Add at least one line with an amount before submitting.");
    return;
  }

  const endpointMap = {
    charge: props.endpoints?.createCharge,
    payment: props.endpoints?.createPayment,
    adjustment: props.endpoints?.createAdjustment,
  };
  const endpoint = endpointMap[activeTab.value];
  if (!endpoint) {
    addAlert("danger", "Missing endpoint configuration.");
    return;
  }

  const payload = { lineItems: items };
  if (activeTab.value === "payment") {
    payload.paymentMethod = paymentMethod.value || undefined;
  }

  isSubmitting.value = true;
  try {
    const response = await fetch(endpoint, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "X-CSRF-Token": props.csrfToken,
      },
      body: JSON.stringify(payload),
    });
    const data = await response.json();
    if (!response.ok) {
      throw new Error(data?.error || "Unable to record transaction");
    }
    addAlert("success", data.message || "Transaction recorded.", {
      receiptNumber: data.receiptNumber,
    });
    resetForm();
    await refreshLedger();
  } catch (error) {
    console.error(error);
    addAlert("danger", error.message || "Unable to record transaction.");
  } finally {
    isSubmitting.value = false;
  }
}
</script>

<style scoped>
.summary {
  display: grid;
  gap: 0.35rem;
  background: rgba(79, 70, 229, 0.08);
  border-radius: 1rem;
  padding: 1rem 1.25rem;
  border: 1px solid rgba(129, 140, 248, 0.25);
}

.summary--balance {
  margin-top: 1.5rem;
  background: rgba(16, 185, 129, 0.1);
  border-color: rgba(16, 185, 129, 0.3);
}

.transaction-tabs {
  display: inline-flex;
  border: 1px solid rgba(148, 163, 184, 0.35);
  border-radius: 999px;
  padding: 0.3rem;
  background: rgba(248, 250, 252, 0.95);
  margin-bottom: 1.5rem;
}

.transaction-tabs__tab {
  border: none;
  background: transparent;
  padding: 0.55rem 1.4rem;
  border-radius: 999px;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s ease, color 0.2s ease;
}

.transaction-tabs__tab--active {
  background: rgba(79, 70, 229, 0.15);
  color: #312e81;
}

.transaction-items {
  display: grid;
  gap: 1.5rem;
}

.transaction-item {
  border: 1px solid rgba(148, 163, 184, 0.25);
  border-radius: 1rem;
  padding: 1.25rem;
  background: rgba(248, 250, 252, 0.85);
  display: grid;
  gap: 1rem;
}

.transaction-item__actions {
  display: flex;
  justify-content: flex-end;
}

.alerts {
  display: grid;
  gap: 0.75rem;
  margin-top: 1.5rem;
}

.alert {
  padding: 0.75rem 1rem;
  border-radius: 0.85rem;
  font-weight: 600;
}

.alert--success {
  background: rgba(16, 185, 129, 0.16);
  color: #065f46;
}

.alert--danger {
  background: rgba(248, 113, 113, 0.18);
  color: #991b1b;
}

.alert--warning {
  background: rgba(245, 158, 11, 0.18);
  color: #92400e;
}

.alert__link {
  color: inherit;
  text-decoration: underline;
}

.text--success {
  color: #047857;
}
</style>
