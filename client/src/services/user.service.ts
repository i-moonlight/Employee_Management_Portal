import { User, FullUser } from '@/models/user.interface';
import { instance } from '@/api/api.interceptor';

const USERS = 'users/profile';

type formData = {
	email: string;
	name?: string;
	password?: string;
	avatarPath?: string;
	phone?: string;
}

export const UserService = {

	async getUserProfile() {
		return instance<FullUser>({
			url: USERS,
			method: 'GET'
		})
	},

	async updateUserProfile(data: formData) {
		return instance<User>({
			url: USERS,
			method: 'PUT',
			data
		})
	},

	async toggleFavorite(productId: string) {
		return instance<User>({
			url: `${USERS}/favorites/${productId}`,
			method: 'PATCH'
		})
	}
}
