import { LitElement, html, css } from "lit";

function initialsFromName(name = "") {
  return name
    .split(/\s+/)
    .filter(Boolean)
    .slice(0, 2)
    .map((segment) => segment[0].toUpperCase())
    .join("");
}

class SqAvatarBadge extends LitElement {
  static properties = {
    name: { type: String },
    subtitle: { type: String },
    image: { type: String },
    color: { type: String },
  };

  static styles = css`
    :host {
      display: inline-flex;
      align-items: center;
      gap: 0.75rem;
      font-family: inherit;
    }

    .avatar {
      width: 3rem;
      height: 3rem;
      border-radius: 9999px;
      display: grid;
      place-items: center;
      color: #fff;
      font-weight: 600;
      font-size: 1.1rem;
      overflow: hidden;
      background: var(--sq-avatar-bg, #2563eb);
    }

    .avatar img {
      width: 100%;
      height: 100%;
      object-fit: cover;
    }

    .meta {
      display: flex;
      flex-direction: column;
      gap: 0.25rem;
    }

    .meta strong {
      font-size: 1rem;
      color: #0f172a;
    }

    .meta span {
      font-size: 0.875rem;
      color: #475569;
    }
  `;

  constructor() {
    super();
    this.name = "";
    this.subtitle = "";
    this.image = "";
    this.color = "#2563eb";
  }

  render() {
    const fallback = initialsFromName(this.name || this.subtitle || "?");
    const style = `--sq-avatar-bg: ${this.color}`;
    return html`
      <span class="avatar" style=${style} aria-hidden="true">
        ${this.image
          ? html`<img src="${this.image}" alt="${this.name}" />`
          : fallback}
      </span>
      <span class="meta">
        <strong>${this.name}</strong>
        ${this.subtitle ? html`<span>${this.subtitle}</span>` : null}
      </span>
    `;
  }
}

if (!customElements.get("sq-avatar-badge")) {
  customElements.define("sq-avatar-badge", SqAvatarBadge);
}
