import { IProduct, TypePaginationProducts } from '@/models/product.interface'
import { instance } from '@/api/api.interceptor'
import { PRODUCTS, typeProductData, TypeProductDataFilters } from './product.types'

export const ProductService = {

	async getAllProducts(queryData = {} as TypeProductDataFilters) {
		const { data } = await instance<TypePaginationProducts>({
			url: PRODUCTS,
			method: 'GET',
			params: queryData,
		})
		return data
	},

	async getSimilarProduct(id: string) {
		return instance<IProduct[]>({
			url: `${PRODUCTS}/similar/${id}`,
			method: 'GET',
		})
	},

	async getProductById(id: string) {
		return instance<IProduct>({
			url: `${PRODUCTS}/${id}`,
			method: 'GET',
		})
	},

	async getProductBySlug(slug: string) {
		const { data } = await instance<IProduct>({
			url: `${PRODUCTS}/by-slug/${slug}`,
			method: 'GET',
		})
		return data
	},

	async getProductByCategory(categorySlug: string) {
		return instance<IProduct[]>({
			url: `${PRODUCTS}/by-category/${categorySlug}`,
			method: 'GET',
		})
	},

	async createProduct() {
		return instance<IProduct>({
			url: PRODUCTS,
			method: 'POST',
		})
	},

	async updateProduct(id: string, data: typeProductData) {
		return instance<IProduct>({
			url: `${PRODUCTS}/${id}`,
			method: 'PUT',
			data,
		})
	},

	async deleteProduct(id: string) {
		return instance<IProduct>({
			url: `${PRODUCTS}/${id}`,
			method: 'DELETE',
		})
	},
}
