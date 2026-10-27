const fs = require("fs");
const path = require("path");
const HTML_SAFE_LT = /</g;
const HTML_SAFE_GT = />/g;
const HTML_SAFE_SCRIPT = /<\/(script)/gi;
const LINE_SEPARATOR = /\u2028/g;
const PARAGRAPH_SEPARATOR = /\u2029/g;

function serializeState(data) {
  if (data === undefined) {
    return "{}";
  }

  const json = JSON.stringify(data, (_, value) => {
    if (typeof value === "undefined") {
      return null;
    }
    return value;
  });

  return json
    .replace(HTML_SAFE_LT, "\\u003c")
    .replace(HTML_SAFE_GT, "\\u003e")
    .replace(HTML_SAFE_SCRIPT, "\\u003c/$1")
    .replace(LINE_SEPARATOR, "\\u2028")
    .replace(PARAGRAPH_SEPARATOR, "\\u2029");
}

function createScriptTag(entryName) {
  const normalized = entryName.startsWith("/") ? entryName : `/js/${entryName}`;
  const manifestPath = path.join(__dirname, "..", "public", "js", ".vite", "manifest.json");
  const manifest = fs.existsSync(manifestPath) ? JSON.parse(fs.readFileSync(manifestPath, "utf8")) : {};
  const entry = Object.values(manifest).find((item) => item.isEntry && item.file === path.basename(normalized));
  const styles = (entry?.css || []).map((file) => `<link rel="stylesheet" href="/js/${file}" />`).join("");
  return styles + `<script type=\"module\" src=\"${normalized}\"></script>`;
}

module.exports = {
  serializeState,
  createScriptTag,
};
