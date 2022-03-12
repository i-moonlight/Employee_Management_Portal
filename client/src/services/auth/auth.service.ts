import { instance } from '@/api/api.interceptor';
import { EmailPassword, AuthResponse } from '@/store/user/user.interface';
import { getRefreshToken, saveToStorage } from './token.service';

export const AuthService = {

	async main(type: 'login' | 'register', data: EmailPassword) {
		const response = await instance<AuthResponse>({
			url: `/auth/${type}`,
			method: 'POST',
			data
		});
		if (response.data.accessToken) saveToStorage(response.data);
		return response.data;
	},

	async getNewTokens() {
		const refreshToken = getRefreshToken();
		const response = await instance<AuthResponse>({
			url: `/auth/update-tokens`,
			method: 'POST',
			data: { refreshToken }
		});
		if (response.data.accessToken) saveToStorage(response.data);
		return response;
	}
}
