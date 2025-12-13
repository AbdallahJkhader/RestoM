// Navbar functionality
document.addEventListener('DOMContentLoaded', function () {
    // 1. Theme Toggle Logic
    const navbarContainer = document.querySelector('.main-navbar-container');
    const menu = document.querySelector('.main-navbar-menu');

    // Create toggle button
    const themeBtn = document.createElement('button');
    themeBtn.className = 'theme-toggle';
    themeBtn.innerHTML = '<i class="fas fa-moon"></i>';
    themeBtn.style.cssText = `
        background: none;
        border: none;
        color: white;
        font-size: 1.2rem;
        cursor: pointer;
        margin-left: 15px;
        padding: 5px;
        transition: transform 0.3s;
    `;

    // Load saved theme
    const savedTheme = localStorage.getItem('theme') || 'light';
    document.documentElement.setAttribute('data-theme', savedTheme);
    updateIcon(savedTheme);

    themeBtn.addEventListener('click', () => {
        const currentTheme = document.documentElement.getAttribute('data-theme');
        const newTheme = currentTheme === 'light' ? 'dark' : 'light';

        document.documentElement.setAttribute('data-theme', newTheme);
        localStorage.setItem('theme', newTheme);
        updateIcon(newTheme);
    });

    function updateIcon(theme) {
        themeBtn.innerHTML = theme === 'light' ? '<i class="fas fa-moon"></i>' : '<i class="fas fa-sun"></i>';
    }

    // Insert button before menu
    if (navbarContainer && menu) {
        navbarContainer.insertBefore(themeBtn, menu);
    }

    // 2. Active Page Highlight
    const currentPage = window.location.pathname.split('/').pop().toLowerCase() || 'index.html';
    const navLinks = document.querySelectorAll('.main-navbar-link');

    navLinks.forEach(link => {
        const href = link.getAttribute('href').toLowerCase();
        if (href === currentPage) {
            link.classList.add('active');
        } else if (currentPage === '' && href === 'index.html') {
            link.classList.add('active');
        }
    });

    // 3. Mobile Menu
    const navToggle = document.querySelector('.main-navbar-toggle');
    if (navToggle && menu) {
        navToggle.addEventListener('click', () => {
            menu.classList.toggle('active');
        });
    }
});
