import { createApp } from "vue";
import TeacherListApp from "./components/TeacherListApp.vue";
import TeacherFormApp from "./components/TeacherFormApp.vue";
import TeacherDetailApp from "./components/TeacherDetailApp.vue";
import { registerSharedComponents } from "../shared/registerComponents";

registerSharedComponents();

const COMPONENTS = {
  TeacherListApp,
  TeacherFormApp,
  TeacherDetailApp,
};

function readState(stateId) {
  if (!stateId) {
    return {};
  }
  const el = document.getElementById(stateId);
  if (!el) {
    return {};
  }
  try {
    return JSON.parse(el.textContent || "{}");
  } catch (error) {
    console.warn(`Unable to parse state for ${stateId}`, error);
    return {};
  }
}

function mountComponent(el) {
  const componentName = el.dataset.vueApp;
  const Component = COMPONENTS[componentName];
  if (!Component) {
    return;
  }
  const props = readState(el.dataset.stateId);
  createApp(Component, props).mount(el);
}

document
  .querySelectorAll('[data-vue-app^="Teacher"]')
  .forEach((el) => mountComponent(el));
