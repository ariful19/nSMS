import { createApp } from "vue";
import { registerSharedComponents } from "../shared/registerComponents";
import ClassManagementApp from "./components/ClassManagementApp.vue";
import ClassDetailApp from "./components/ClassDetailApp.vue";
import GradeEntryApp from "./components/GradeEntryApp.vue";
import GradeReportApp from "./components/GradeReportApp.vue";

registerSharedComponents();

const COMPONENTS = {
  ClassManagementApp,
  ClassDetailApp,
  GradeEntryApp,
  GradeReportApp,
};

function readState(stateId) {
  if (!stateId) {
    return {};
  }
  const element = document.getElementById(stateId);
  if (!element) {
    return {};
  }
  try {
    return JSON.parse(element.textContent || "{}");
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

document.querySelectorAll("[data-vue-app]").forEach((el) => {
  if (COMPONENTS[el.dataset.vueApp]) {
    mountComponent(el);
  }
});
