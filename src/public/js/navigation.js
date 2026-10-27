const toggle = document.querySelector(".nav-toggle");
const navigation = document.getElementById("main-navigation");
if (toggle && navigation) {
  toggle.addEventListener("click", () => {
    const expanded = toggle.getAttribute("aria-expanded") !== "true";
    toggle.setAttribute("aria-expanded", String(expanded));
    navigation.classList.toggle("is-open", expanded);
  });
  const handleEscape = (event) => {
    if (event.key === "Escape") {
      toggle.setAttribute("aria-expanded", "false");
      navigation.classList.remove("is-open");
      toggle.focus();
    }
  };
  navigation.addEventListener("keydown", handleEscape);
  toggle.addEventListener("keydown", handleEscape);
}
