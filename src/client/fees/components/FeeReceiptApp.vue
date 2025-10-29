<template>
  <section class="card card--interactive">
    <header class="card__header">
      <div>
        <h1 class="card__title">Receipt {{ receipt.receiptNumber }}</h1>
        <p class="card__hint">
          {{ receipt.student?.name ?? "" }} • {{ receipt.student?.studentNumber ?? "—" }}
        </p>
      </div>
      <div class="card__actions">
        <button type="button" class="btn btn--ghost" @click="print">Print</button>
        <a class="btn" href="/fees">Back to fees</a>
      </div>
    </header>

    <div class="table-responsive" v-if="receipt.lineItems?.length">
      <table class="table">
        <thead>
          <tr>
            <th scope="col">Line</th>
            <th scope="col">Description</th>
            <th scope="col">Category</th>
            <th scope="col">Method</th>
            <th scope="col">Date</th>
            <th scope="col" class="table__cell--numeric">Amount</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in receipt.lineItems" :key="item.id ?? index">
            <td>{{ index + 1 }}</td>
            <td>{{ item.description || "—" }}</td>
            <td>{{ item.category?.name ?? "—" }}</td>
            <td>{{ item.paymentMethod || "—" }}</td>
            <td>{{ formatDate(item.entryDate) }}</td>
            <td class="table__cell--numeric">{{ formatCurrency(item.amount) }}</td>
          </tr>
        </tbody>
        <tfoot>
          <tr>
            <th scope="row" colspan="5" class="table__cell--numeric">Total</th>
            <td class="table__cell--numeric">{{ formatCurrency(receipt.total) }}</td>
          </tr>
        </tfoot>
      </table>
    </div>
    <p v-else class="empty-state__title">No line items available for this receipt.</p>
  </section>
</template>

<script setup>
const props = defineProps({
  receipt: { type: Object, default: () => ({ lineItems: [] }) },
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

function print() {
  window.print();
}
</script>
