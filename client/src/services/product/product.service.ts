import { Product, TypeProductPagination } from '@/models/product.interface';
import { instance } from '@/api/api.interceptor';
import { PRODUCTS, typeProductData } from './product.types';

export const ProductService = {

	async getAllProducts(queryData?: { perPage: number; page: number }) {
		const { data } = await instance<TypeProductPagination>({
			url: PRODUCTS,
			method: 'GET',
			params: queryData
		});
		return data
	},

	async getSimilarProduct(id: string) {
		return instance<Product[]>({
			url: `${PRODUCTS}/similar/${id}`,
			method: 'GET'
		})
	},

	async getProductById(id: string) {
		return instance<Product>({
			url: `${PRODUCTS}/${id}`,
			method: 'GET'
		})
	},

	async getProductBySlug(slug: string) {
		const { data } = await instance<Product>({
			url: `${PRODUCTS}/by-slug/${slug}`,
			method: 'GET'
		})
		return data
	},

	async getProductByCategory(categorySlug: string) {
		return instance<Product[]>({
			url: `${PRODUCTS}/by-category/${categorySlug}`,
			method: 'GET'
		})
	},

	async createProduct() {
		return instance<Product>({
			url: PRODUCTS,
			method: 'POST'
		})
	},

	async updateProduct(id: string, data: typeProductData) {
		return instance<Product>({
			url: `${PRODUCTS}/${id}`,
			method: 'PUT',
			data
		})
	},

	async deleteProduct(id: string) {
		return instance<Product>({
			url: `${PRODUCTS}/${id}`,
			method: 'DELETE'
		})
	}
}
