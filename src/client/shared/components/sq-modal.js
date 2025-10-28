import { LitElement, html, css } from "lit";

class SqModal extends LitElement {
  static properties = {
    open: { type: Boolean, reflect: true },
    heading: { type: String },
  };

  static styles = css`
    :host {
      position: fixed;
      inset: 0;
      display: none;
      align-items: center;
      justify-content: center;
      z-index: 1000;
    }

    :host([open]) {
      display: flex;
    }

    .backdrop {
      position: absolute;
      inset: 0;
      background: rgba(17, 24, 39, 0.45);
    }

    .dialog {
      position: relative;
      background: #fff;
      border-radius: 0.75rem;
      box-shadow: 0 20px 50px rgba(15, 23, 42, 0.15);
      width: min(640px, calc(100vw - 2rem));
      max-height: calc(100vh - 4rem);
      overflow: hidden;
      display: flex;
      flex-direction: column;
    }

    header {
      padding: 1.25rem 1.5rem 0.75rem;
      border-bottom: 1px solid rgba(15, 23, 42, 0.08);
      display: flex;
      align-items: flex-start;
      justify-content: space-between;
      gap: 1rem;
    }

    header h2 {
      font-size: 1.25rem;
      margin: 0;
    }

    .close-btn {
      border: none;
      background: transparent;
      font-size: 1.25rem;
      cursor: pointer;
      color: #475569;
    }

    .content {
      padding: 1.25rem 1.5rem 1.5rem;
      overflow-y: auto;
      flex: 1;
    }

    footer {
      padding: 0.75rem 1.5rem 1.25rem;
      border-top: 1px solid rgba(15, 23, 42, 0.08);
      display: flex;
      justify-content: flex-end;
      gap: 0.75rem;
    }

    @media (max-width: 600px) {
      .dialog {
        width: calc(100vw - 1.5rem);
        max-height: calc(100vh - 1.5rem);
        border-radius: 0.5rem;
      }
    }
  `;

  constructor() {
    super();
    this.open = false;
    this.heading = "";
    this._handleKeydown = this._handleKeydown.bind(this);
  }

  connectedCallback() {
    super.connectedCallback();
    document.addEventListener("keydown", this._handleKeydown);
  }

  disconnectedCallback() {
    document.removeEventListener("keydown", this._handleKeydown);
    super.disconnectedCallback();
  }

  firstUpdated() {
    if (this.open) {
      this._focusDialog();
    }
  }

  updated(changed) {
    if (changed.has("open")) {
      if (this.open) {
        this._focusDialog();
        this.dispatchEvent(new CustomEvent("sq-modal-open", { bubbles: true }));
      } else {
        this.dispatchEvent(new CustomEvent("sq-modal-close", { bubbles: true }));
      }
    }
  }

  _focusDialog() {
    const dialog = this.renderRoot?.querySelector?.(".dialog");
    if (dialog) {
      dialog.focus({ preventScroll: true });
    }
  }

  _handleKeydown(event) {
    if (!this.open) {
      return;
    }
    if (event.key === "Escape") {
      this.close();
    }
  }

  _handleBackdropClick(event) {
    if (event.target.classList.contains("backdrop")) {
      this.close();
    }
  }

  show() {
    this.open = true;
  }

  close() {
    if (!this.open) {
      return;
    }
    this.open = false;
    this.dispatchEvent(new CustomEvent("close", { bubbles: true }));
  }

  render() {
    return html`
      <div class="backdrop" @click=${this._handleBackdropClick}></div>
      <div
        class="dialog"
        role="dialog"
        aria-modal="true"
        tabindex="-1"
        aria-hidden=${this.open ? "false" : "true"}
      >
        <header>
          <h2>${this.heading || html`<slot name="title"></slot>`}</h2>
          <button
            class="close-btn"
            type="button"
            @click=${() => this.close()}
            aria-label="Close dialog"
          >
            &times;
          </button>
        </header>
        <div class="content">
          <slot></slot>
        </div>
        <footer>
          <slot name="footer"></slot>
        </footer>
      </div>
    `;
  }
}

if (!customElements.get("sq-modal")) {
  customElements.define("sq-modal", SqModal);
}
