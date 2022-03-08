import { getRefreshToken, saveToStorage } from './auth.helper';
import { instance } from '@/src/api/api.interceptor';
import { AuthResponse } from '@/store/user/user.interface'

export const AuthService = {
	async invoke(data: UserLogin) {
		const response = await instance<AuthResponse>({
			url: `/auth/signin`,
			method: 'POST',
			data,
		})
		if (response.data.accessToken) saveToStorage(response.data)
		return response.data
	},

	async getNewTokens() {
		const refreshToken = getRefreshToken()
		const response = await instance<AuthResponse>({
			url: `/auth/update-tokens`,
			method: 'POST',
			data: { refreshToken },
		})
		if (response.data.accessToken) saveToStorage(response.data)
		return response
	},
}
