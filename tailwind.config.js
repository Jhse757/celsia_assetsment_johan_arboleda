/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Views/**/*.cshtml", // Captura todas las vistas en la carpeta Views
    "./Views/Shared/**/*.cshtml", // Captura los archivos en la carpeta Shared de Views
    "./wwwroot/**/*.js", // Captura todos los archivos JS en la carpeta wwwroot
    "./**/*.html", // Captura todos los archivos HTML en el proyecto
    "./**/*.js", // Captura todos los archivos JS en el proyecto
  ],
  darkMode: "class",
  theme: {
    extend: {
      colors: {
        dark: {
          500: "#16161d",
        },
        primary: {
          50: "#fff6ed",
          100: "#ffebd4",
          200: "#ffd3a8",
          300: "#ffb371",
          400: "#fe6611",
          500: "#fe6611",
          600: "#ef4b07",
          700: "#c63508",
          800: "#9d2c0f",
          900: "#7e2610",
          950: "#441006",
        },
      },
    },
  },
  plugins: [],
};
