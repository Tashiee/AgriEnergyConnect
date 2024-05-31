/** @type {import('tailwindcss').Config} */
module.exports = {
    purge: [
        './**/*.html',
        './**/*.razor',
        './**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                primary: "#1d4c43",
                secondary: "#e6fd99",
            },
        },
    },
    plugins: [],
};
 