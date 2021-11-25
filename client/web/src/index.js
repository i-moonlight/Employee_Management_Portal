import HomePage from './pages/HomePage.js';
import ProductPage from './pages/ProductPage.js';
import { parseRequestUrl } from './utils.js';
import ErrorPage from './pages/ErrorPage.js';

const routes = {
    '/': HomePage,
    '/product/:id': ProductPage,
};

const router = async () => {
    const request = parseRequestUrl();
    const parseUrl =
        (request.resource ? `/${request.resource}` : '/') +
        (request.id ? '/:id' : '') +
        (request.verb ? `/${request.verb}` : '');
    const page = routes[parseUrl] ? routes[parseUrl] : ErrorPage;

    const main = document.getElementById('main-container');
    main.innerHTML = await page.render();
};

window.addEventListener('load', router);
window.addEventListener('hashchange', router);
