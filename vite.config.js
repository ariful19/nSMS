const path = require("path");
const { defineConfig } = require("vite");
const vue = require("@vitejs/plugin-vue");

module.exports = defineConfig({
  plugins: [
    vue({
      template: {
        compilerOptions: {
          isCustomElement: (tag) => tag.startsWith("sq-"),
        },
      },
    }),
  ],
  publicDir: false,
  build: {
    outDir: path.resolve(__dirname, "src/public/js"),
    emptyOutDir: false,
    sourcemap: true,
    rollupOptions: {
      input: {
        students: path.resolve(__dirname, "src/client/students/main.js"),
        teachers: path.resolve(__dirname, "src/client/teachers/main.js"),
        academic: path.resolve(__dirname, "src/client/academic/main.js"),
        fees: path.resolve(__dirname, "src/client/fees/main.js"),
        library: path.resolve(__dirname, "src/client/library/main.js"),
        communications: path.resolve(__dirname, "src/client/communications/main.js"),
      },
      output: {
        entryFileNames: "[name].js",
        chunkFileNames: "chunks/[name]-[hash].js",
        assetFileNames: "assets/[name]-[hash][extname]",
      },
    },
  },
});
