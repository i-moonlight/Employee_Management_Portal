import HomePage from './pages/HomePage.js';

const router = () => {
    const main = document.getElementById('main-container');
    main.innerHTML = HomePage.render();
};
window.addEventListener('load', router);
