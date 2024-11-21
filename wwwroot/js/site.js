const darkModeToggle = document.getElementById('darkmode');

darkModeToggle.addEventListener('change', () => {
    const htmlElement = document.documentElement;
    const newTheme = darkModeToggle.checked ? 'dark' : 'light'; 
    htmlElement.setAttribute('data-bs-theme', newTheme);
});