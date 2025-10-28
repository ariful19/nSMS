import "./components/sq-modal.js";
import "./components/sq-date-picker.js";
import "./components/sq-avatar-badge.js";

let registered = false;

export function registerSharedComponents() {
  if (registered) {
    return;
  }
  registered = true;
}
