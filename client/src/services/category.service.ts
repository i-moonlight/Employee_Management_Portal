import { Category } from '@/models/category.interface';
import { instance } from '@/api/api.interceptor';

const CATEGORIES = 'categories'

export const CategoryService = {
	async getAllCategories() {
		return instance<Category[]>({
			url: CATEGORIES,
			method: 'GET'
		})
	},

	async getCategoryById(id: string) {
		return instance<Category>({
			url: `${CATEGORIES}/${id}`,
			method: 'GET'
		})
	},

	async getCategoryBySlug(slug: string) {
		return instance<Category>({
			url: `${CATEGORIES}/by-slug/${slug}`,
			method: 'GET'
		})
	},

	async createCategory() {
		return instance<Category>({
			url: CATEGORIES,
			method: 'POST'
		})
	},

	async updateCategory(id: string, name: string) {
		return instance<Category>({
			url: `${CATEGORIES}/${id}`,
			method: 'PUT',
			data: { name }
		})
	},

	async deleteCategory(id: string) {
		return instance<Category>({
			url: `${CATEGORIES}/${id}`,
			method: 'DELETE'
		})
	}
}
