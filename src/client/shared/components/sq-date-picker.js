import { LitElement, html, css } from "lit";
import { ifDefined } from "lit/directives/if-defined.js";

class SqDatePicker extends LitElement {
  static properties = {
    name: { type: String },
    label: { type: String },
    value: { type: String },
    min: { type: String },
    max: { type: String },
    required: { type: Boolean, reflect: true },
    disabled: { type: Boolean, reflect: true },
  };

  static styles = css`
    :host {
      display: block;
    }

    label {
      display: flex;
      flex-direction: column;
      gap: 0.375rem;
      font-size: 0.9rem;
      color: #0f172a;
    }

    .control {
      display: inline-flex;
      align-items: center;
      gap: 0.35rem;
      border: 1px solid rgba(15, 23, 42, 0.2);
      padding: 0.45rem 0.65rem;
      border-radius: 0.5rem;
      background: #fff;
      transition: border-color 0.2s ease, box-shadow 0.2s ease;
    }

    .control:focus-within {
      border-color: #2563eb;
      box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.15);
    }

    input[type="date"] {
      border: none;
      outline: none;
      font: inherit;
      background: transparent;
      padding: 0;
      width: 100%;
      color: inherit;
    }

    input[type="date"]:disabled {
      color: #94a3b8;
      cursor: not-allowed;
    }

    .calendar-icon {
      color: #2563eb;
      font-size: 1rem;
    }
  `;

  constructor() {
    super();
    this.name = "";
    this.label = "";
    this.value = "";
    this.min = undefined;
    this.max = undefined;
    this.required = false;
    this.disabled = false;
  }

  createRenderRoot() {
    return this;
  }

  _handleInput(event) {
    this.value = event.target.value;
    this.dispatchEvent(
      new CustomEvent("value-change", {
        detail: this.value,
        bubbles: true,
        composed: true,
      }),
    );
  }

  render() {
    return html`
      <label>
        ${this.label ? html`<span>${this.label}</span>` : null}
        <span class="control">
          <span class="calendar-icon" aria-hidden="true">📅</span>
          <input
            type="date"
            name=${ifDefined(this.name || undefined)}
            .value=${this.value || ""}
            min=${ifDefined(this.min)}
            max=${ifDefined(this.max)}
            ?required=${this.required}
            ?disabled=${this.disabled}
            @input=${this._handleInput}
          />
        </span>
      </label>
    `;
  }
}

if (!customElements.get("sq-date-picker")) {
  customElements.define("sq-date-picker", SqDatePicker);
}
