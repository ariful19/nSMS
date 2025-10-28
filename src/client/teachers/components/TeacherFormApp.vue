<template>
  <section class="card card--form">
    <header class="card__header">
      <div>
        <h1 class="card__title">
          {{ isEditMode ? "Edit teacher or staff member" : "Add teacher or staff member" }}
        </h1>
        <p class="card__subtitle">Capture employment and contact information for the team member.</p>
      </div>
      <a class="btn btn--ghost" href="/teachers">Back to list</a>
    </header>

    <div v-if="serverErrors.length" class="alert alert--danger" role="alert">
      <p><strong>We ran into a few issues:</strong></p>
      <ul>
        <li v-for="error in serverErrors" :key="error">{{ error }}</li>
      </ul>
    </div>

    <form ref="formRef" method="post" :action="formAction" class="form" @submit.prevent="handleSubmit">
      <input type="hidden" name="_csrf" :value="csrfToken" />

      <fieldset class="form__fieldset">
        <legend class="form__legend">Personal information</legend>
        <div class="form__grid">
          <label class="form__field" :class="{ 'form__field--error': inlineErrors.firstName }">
            <span class="form__label">First name *</span>
            <input
              v-model.trim="form.firstName"
              name="firstName"
              type="text"
              autocomplete="given-name"
              required
              @blur="markTouched('firstName')"
            />
            <span v-if="inlineErrors.firstName" class="form__hint">{{ inlineErrors.firstName }}</span>
          </label>
          <label class="form__field">
            <span class="form__label">Middle name</span>
            <input v-model.trim="form.middleName" name="middleName" type="text" autocomplete="additional-name" />
          </label>
          <label class="form__field" :class="{ 'form__field--error': inlineErrors.lastName }">
            <span class="form__label">Last name *</span>
            <input
              v-model.trim="form.lastName"
              name="lastName"
              type="text"
              autocomplete="family-name"
              required
              @blur="markTouched('lastName')"
            />
            <span v-if="inlineErrors.lastName" class="form__hint">{{ inlineErrors.lastName }}</span>
          </label>
          <label class="form__field">
            <span class="form__label">Preferred name</span>
            <input v-model.trim="form.preferredName" name="preferredName" type="text" autocomplete="nickname" />
          </label>
          <label class="form__field">
            <span class="form__label">Date of birth</span>
            <sq-date-picker
              name="dateOfBirth"
              :value="form.dateOfBirth"
              label=""
              @value-change="(value) => (form.dateOfBirth = value)"
            ></sq-date-picker>
          </label>
          <label class="form__field">
            <span class="form__label">Gender</span>
            <select v-model="form.genderId" name="genderId">
              <option value="">Select gender</option>
              <option v-for="gender in lookups.genders" :key="gender.id" :value="String(gender.id)">
                {{ gender.name }}
              </option>
            </select>
          </label>
        </div>

        <div class="toggle">
          <label>
            <input type="checkbox" v-model="showContactSection" />
            <span>Collect contact details</span>
          </label>
        </div>
        <transition name="slide">
          <div v-if="showContactSection" class="form__grid form__grid--secondary">
            <label class="form__field">
              <span class="form__label">Primary email</span>
              <input v-model.trim="form.primaryEmail" name="primaryEmail" type="email" autocomplete="email" />
            </label>
            <label class="form__field">
              <span class="form__label">Mobile phone</span>
              <input v-model.trim="form.mobilePhone" name="mobilePhone" type="tel" autocomplete="tel" />
            </label>
          </div>
        </transition>
      </fieldset>

      <fieldset class="form__fieldset">
        <legend class="form__legend">Employment details</legend>
        <div class="form__grid">
          <label class="form__field" :class="{ 'form__field--error': inlineErrors.staffTypeId }">
            <span class="form__label">Staff type *</span>
            <select v-model="form.staffTypeId" name="staffTypeId" required @blur="markTouched('staffTypeId')">
              <option value="">Select staff type</option>
              <option v-for="type in lookups.staffTypes" :key="type.id" :value="String(type.id)">
                {{ type.name }}
              </option>
            </select>
            <span v-if="inlineErrors.staffTypeId" class="form__hint">{{ inlineErrors.staffTypeId }}</span>
          </label>
          <label class="form__field" :class="{ 'form__field--error': inlineErrors.employmentStatusId }">
            <span class="form__label">Employment status *</span>
            <select
              v-model="form.employmentStatusId"
              name="employmentStatusId"
              required
              @blur="markTouched('employmentStatusId')"
            >
              <option value="">Select employment status</option>
              <option v-for="status in lookups.employmentStatuses" :key="status.id" :value="String(status.id)">
                {{ status.name }}
              </option>
            </select>
            <span v-if="inlineErrors.employmentStatusId" class="form__hint">
              {{ inlineErrors.employmentStatusId }}
            </span>
          </label>
          <label class="form__field">
            <span class="form__label">Employee number</span>
            <input v-model.trim="form.employeeNumber" name="employeeNumber" type="text" autocomplete="off" />
          </label>
          <label class="form__field">
            <span class="form__label">Primary subject</span>
            <input v-model.trim="form.primarySubject" name="primarySubject" type="text" autocomplete="off" />
          </label>
        </div>

        <div class="toggle">
          <label>
            <input type="checkbox" v-model="showContractSection" />
            <span>Track contract timeline</span>
          </label>
        </div>
        <transition name="slide">
          <div v-if="showContractSection" class="form__grid form__grid--secondary">
            <label class="form__field">
              <span class="form__label">Hire date</span>
              <sq-date-picker
                name="hireDate"
                :value="form.hireDate"
                label=""
                @value-change="(value) => (form.hireDate = value)"
              ></sq-date-picker>
            </label>
            <label class="form__field">
              <span class="form__label">Contract end date</span>
              <sq-date-picker
                name="contractEndDate"
                :value="form.contractEndDate"
                label=""
                @value-change="(value) => (form.contractEndDate = value)"
              ></sq-date-picker>
            </label>
            <label class="form__field form__field--full">
              <span class="form__label">Notes</span>
              <textarea v-model.trim="form.notes" name="notes" rows="4"></textarea>
            </label>
          </div>
        </transition>
      </fieldset>

      <div class="form__actions">
        <button type="submit" class="btn btn--primary">{{ submitLabel }}</button>
        <a class="btn btn--link" href="/teachers">Cancel</a>
      </div>
    </form>
  </section>
</template>

<script setup>
import { computed, reactive, ref, watch } from "vue";

const props = defineProps({
  lookups: {
    type: Object,
    default: () => ({ genders: [], staffTypes: [], employmentStatuses: [] }),
  },
  values: {
    type: Object,
    default: () => ({}),
  },
  errors: {
    type: Array,
    default: () => [],
  },
  isEditMode: { type: Boolean, default: false },
  teacherId: { type: Number, default: null },
  csrfToken: { type: String, default: "" },
  formAction: { type: String, default: "/teachers/create" },
});

const formRef = ref(null);

const form = reactive({
  firstName: props.values.firstName || "",
  middleName: props.values.middleName || "",
  lastName: props.values.lastName || "",
  preferredName: props.values.preferredName || "",
  dateOfBirth: props.values.dateOfBirth || "",
  genderId: props.values.genderId ? String(props.values.genderId) : "",
  primaryEmail: props.values.primaryEmail || "",
  mobilePhone: props.values.mobilePhone || "",
  staffTypeId: props.values.staffTypeId ? String(props.values.staffTypeId) : "",
  employmentStatusId: props.values.employmentStatusId ? String(props.values.employmentStatusId) : "",
  employeeNumber: props.values.employeeNumber || "",
  primarySubject: props.values.primarySubject || "",
  hireDate: props.values.hireDate || "",
  contractEndDate: props.values.contractEndDate || "",
  notes: props.values.notes || "",
});

const showContactSection = ref(Boolean(form.primaryEmail || form.mobilePhone));
const showContractSection = ref(Boolean(form.hireDate || form.contractEndDate || form.notes));

const serverErrors = computed(() => props.errors || []);
const submitLabel = computed(() => (props.isEditMode ? "Save changes" : "Create record"));

const touched = reactive({
  firstName: false,
  lastName: false,
  staffTypeId: false,
  employmentStatusId: false,
});

const inlineErrors = reactive({
  firstName: "",
  lastName: "",
  staffTypeId: "",
  employmentStatusId: "",
});

function validateField(field) {
  let message = "";
  if (field === "firstName" && !form.firstName.trim()) {
    message = "First name is required.";
  }
  if (field === "lastName" && !form.lastName.trim()) {
    message = "Last name is required.";
  }
  if (field === "staffTypeId" && !form.staffTypeId) {
    message = "Staff type is required.";
  }
  if (field === "employmentStatusId" && !form.employmentStatusId) {
    message = "Employment status is required.";
  }
  inlineErrors[field] = message;
  return !message;
}

function markTouched(field) {
  touched[field] = true;
  validateField(field);
}

watch(
  () => form.firstName,
  () => {
    if (touched.firstName) {
      validateField("firstName");
    }
  },
);

watch(
  () => form.lastName,
  () => {
    if (touched.lastName) {
      validateField("lastName");
    }
  },
);

watch(
  () => form.staffTypeId,
  () => {
    if (touched.staffTypeId) {
      validateField("staffTypeId");
    }
  },
);

watch(
  () => form.employmentStatusId,
  () => {
    if (touched.employmentStatusId) {
      validateField("employmentStatusId");
    }
  },
);

watch(showContactSection, (value) => {
  if (!value) {
    form.primaryEmail = "";
    form.mobilePhone = "";
  }
});

watch(showContractSection, (value) => {
  if (!value) {
    form.hireDate = "";
    form.contractEndDate = "";
    form.notes = "";
  }
});

function focusFirstError() {
  const field = Object.keys(inlineErrors).find((key) => inlineErrors[key]);
  if (!field) {
    return;
  }
  const el = formRef.value?.querySelector?.(`[name="${field}"]`);
  if (el) {
    el.focus();
  }
}

function handleSubmit() {
  touched.firstName = true;
  touched.lastName = true;
  touched.staffTypeId = true;
  touched.employmentStatusId = true;

  const validFirst = validateField("firstName");
  const validLast = validateField("lastName");
  const validStaffType = validateField("staffTypeId");
  const validEmployment = validateField("employmentStatusId");

  if (!validFirst || !validLast || !validStaffType || !validEmployment) {
    focusFirstError();
    return;
  }

  formRef.value?.submit();
}
</script>

<style scoped>
.card--form {
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
  padding: 1.5rem;
}

.card__header {
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
  gap: 1rem;
}

.alert {
  border-radius: 0.75rem;
  padding: 1rem 1.25rem;
}

.form__fieldset {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.form__grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(14rem, 1fr));
  gap: 1rem;
}

.form__grid--secondary {
  grid-template-columns: repeat(auto-fit, minmax(12rem, 1fr));
}

.form__field {
  display: flex;
  flex-direction: column;
  gap: 0.4rem;
}

.form__field input,
.form__field select,
.form__field textarea {
  border: 1px solid rgba(15, 23, 42, 0.2);
  border-radius: 0.5rem;
  padding: 0.5rem 0.65rem;
  font: inherit;
}

.form__field--error input,
.form__field--error select {
  border-color: #dc2626;
  box-shadow: 0 0 0 2px rgba(220, 38, 38, 0.1);
}

.form__hint {
  color: #dc2626;
  font-size: 0.8rem;
}

.toggle {
  margin-top: 0.25rem;
}

.toggle label {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 0.9rem;
}

.form__actions {
  display: flex;
  gap: 0.75rem;
  justify-content: flex-end;
}

.slide-enter-active,
.slide-leave-active {
  transition: all 0.2s ease;
}

.slide-enter-from,
.slide-leave-to {
  opacity: 0;
  transform: translateY(-0.5rem);
}
</style>
