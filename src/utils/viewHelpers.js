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
  return `<script type=\"module\" src=\"${normalized}\"></script>`;
}

module.exports = {
  serializeState,
  createScriptTag,
};
