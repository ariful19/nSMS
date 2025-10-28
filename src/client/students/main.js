import { createApp } from "vue";
import StudentListApp from "./components/StudentListApp.vue";
import StudentFormApp from "./components/StudentFormApp.vue";
import StudentDetailApp from "./components/StudentDetailApp.vue";
import { registerSharedComponents } from "../shared/registerComponents";

registerSharedComponents();

const COMPONENTS = {
  StudentListApp,
  StudentFormApp,
  StudentDetailApp,
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
  .querySelectorAll('[data-vue-app^="Student"]')
  .forEach((el) => mountComponent(el));
