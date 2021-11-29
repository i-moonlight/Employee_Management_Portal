import axios from 'axios';
import { APP_BASE_URL } from '../api/App.constants';

const HomePage = {
    render: async () => {
        const response = await axios({
            url: APP_BASE_URL + 'product',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response || response.statusText !== 'OK') {
            return `<div>Error in getting data</div>`;
        }
        const products = response.data;

        return
        ` <ul class="products">
           ${products.map((product) => `
         <li>
           <div class="product">
             <a href="/#/product/${product._id}">
               <img src="${product.image}" alt="${product.name}" />
             </a>
             <div class="product-name">
               <a href="/#/product/1">${product.name}</a>
             </div>
             <div class="product-brand">${product.brand}</div>
             <div class="product-price">$${product.price}</div>
           </div>
         </li>
      `
        ).join('\n')}
      `;

    },
};
export default HomePage;
