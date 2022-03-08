import { Review } from '@/models/review.interface';
import { instance } from '@/api/api.interceptor';

const REVIEWS = 'reviews';

type typeData = {
	rating: number;
	text:   string;
}

export const ReviewService = {

	async getAllCategories() {
		return instance<Review[]>({
			url: REVIEWS,
			method: 'GET'
		})
	},

	async getAverageByProduct(productId: string) {
		return instance<{ rating: number }>({
			url: `${REVIEWS}/average-by-product/${productId}`,
			method: 'GET'
		})
	},

	async createReview(productId: string, data: typeData) {
		return instance<Review>({
			url: `${REVIEWS}/leave/${productId}`,
			method: 'POST',
			data
		})
	}
}
