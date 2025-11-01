import { createApp } from "vue";
import { registerSharedComponents } from "../shared/registerComponents";
import NoticeCenterApp from "./components/NoticeCenterApp.vue";
import NoticeFormApp from "./components/NoticeFormApp.vue";
import NoticeDetailApp from "./components/NoticeDetailApp.vue";
import EventBoardApp from "./components/EventBoardApp.vue";
import EventFormApp from "./components/EventFormApp.vue";
import EventDetailApp from "./components/EventDetailApp.vue";
import DashboardCommunicationsApp from "./components/DashboardCommunicationsApp.vue";

registerSharedComponents();

const COMPONENTS = {
  NoticeCenterApp,
  NoticeFormApp,
  NoticeDetailApp,
  EventBoardApp,
  EventFormApp,
  EventDetailApp,
  DashboardCommunicationsApp,
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

document.querySelectorAll("[data-vue-app]").forEach((el) => mountComponent(el));
