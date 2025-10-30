import { createApp } from "vue";
import { registerSharedComponents } from "../shared/registerComponents";
import LibraryDashboardApp from "./components/LibraryDashboardApp.vue";
import LibraryBookApp from "./components/LibraryBookApp.vue";
import LibraryLoanApp from "./components/LibraryLoanApp.vue";

registerSharedComponents();

const COMPONENTS = {
  LibraryDashboardApp,
  LibraryBookApp,
  LibraryLoanApp,
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

Array.from(document.querySelectorAll('[data-vue-app^="Library"]')).forEach((el) =>
  mountComponent(el)
);
