<template>
  <section class="card card--form">
    <header class="card__header">
      <div>
        <h1 class="card__title">{{ isEditMode ? "Edit student" : "Add student" }}</h1>
        <p class="card__subtitle">
          Complete the required personal and enrollment information.
        </p>
      </div>
      <a class="btn btn--ghost" href="/students">Back to list</a>
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
              @blur="markTouched('firstName')"
              required
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
              @blur="markTouched('lastName')"
              required
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
        <legend class="form__legend">Enrollment details</legend>
        <div class="form__grid">
          <label class="form__field" :class="{ 'form__field--error': inlineErrors.enrollmentStatusId }">
            <span class="form__label">Enrollment status *</span>
            <select
              v-model="form.enrollmentStatusId"
              name="enrollmentStatusId"
              @blur="markTouched('enrollmentStatusId')"
              required
            >
              <option value="">Select status</option>
              <option v-for="status in lookups.statuses" :key="status.id" :value="String(status.id)">
                {{ status.name }}
              </option>
            </select>
            <span v-if="inlineErrors.enrollmentStatusId" class="form__hint">
              {{ inlineErrors.enrollmentStatusId }}
            </span>
          </label>
          <label class="form__field">
            <span class="form__label">Grade level</span>
            <select v-model="form.gradeLevelId" name="gradeLevelId">
              <option value="">Select grade</option>
              <option v-for="grade in lookups.gradeLevels" :key="grade.id" :value="String(grade.id)">
                {{ grade.name }}
              </option>
            </select>
          </label>
          <label class="form__field">
            <span class="form__label">Student number</span>
            <input v-model.trim="form.studentNumber" name="studentNumber" type="text" autocomplete="off" />
          </label>
          <label class="form__field">
            <span class="form__label">Admission date</span>
            <sq-date-picker
              name="admissionDate"
              :value="form.admissionDate"
              label=""
              @value-change="(value) => (form.admissionDate = value)"
            ></sq-date-picker>
          </label>
        </div>

        <div class="toggle">
          <label>
            <input type="checkbox" v-model="showGraduationSection" />
            <span>Track graduation planning</span>
          </label>
        </div>
        <transition name="slide">
          <div v-if="showGraduationSection" class="form__grid form__grid--secondary">
            <label class="form__field">
              <span class="form__label">Graduation date</span>
              <sq-date-picker
                name="graduationDate"
                :value="form.graduationDate"
                label=""
                @value-change="(value) => (form.graduationDate = value)"
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
        <a class="btn btn--link" href="/students">Cancel</a>
      </div>
    </form>
  </section>
</template>

<script setup>
import { computed, reactive, ref, watch } from "vue";

const props = defineProps({
  lookups: {
    type: Object,
    default: () => ({ genders: [], statuses: [], gradeLevels: [] }),
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
  studentId: { type: Number, default: null },
  csrfToken: { type: String, default: "" },
  formAction: { type: String, default: "/students/create" },
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
  enrollmentStatusId: props.values.enrollmentStatusId ? String(props.values.enrollmentStatusId) : "",
  gradeLevelId: props.values.gradeLevelId ? String(props.values.gradeLevelId) : "",
  studentNumber: props.values.studentNumber || "",
  admissionDate: props.values.admissionDate || "",
  graduationDate: props.values.graduationDate || "",
  notes: props.values.notes || "",
});

const showContactSection = ref(Boolean(form.primaryEmail || form.mobilePhone));
const showGraduationSection = ref(Boolean(form.graduationDate || form.notes));

const serverErrors = computed(() => props.errors || []);

const touched = reactive({
  firstName: false,
  lastName: false,
  enrollmentStatusId: false,
});

const inlineErrors = reactive({
  firstName: "",
  lastName: "",
  enrollmentStatusId: "",
});

const submitLabel = computed(() => (props.isEditMode ? "Save changes" : "Create student"));

function validateField(field) {
  let message = "";
  if (field === "firstName" && !form.firstName.trim()) {
    message = "First name is required.";
  }
  if (field === "lastName" && !form.lastName.trim()) {
    message = "Last name is required.";
  }
  if (field === "enrollmentStatusId" && !form.enrollmentStatusId) {
    message = "Enrollment status is required.";
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
  () => form.enrollmentStatusId,
  () => {
    if (touched.enrollmentStatusId) {
      validateField("enrollmentStatusId");
    }
  },
);

watch(showContactSection, (value) => {
  if (!value) {
    form.primaryEmail = "";
    form.mobilePhone = "";
  }
});

watch(showGraduationSection, (value) => {
  if (!value) {
    form.graduationDate = "";
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
  touched.enrollmentStatusId = true;

  const firstValid = validateField("firstName");
  const lastValid = validateField("lastName");
  const statusValid = validateField("enrollmentStatusId");

  if (!firstValid || !lastValid || !statusValid) {
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
