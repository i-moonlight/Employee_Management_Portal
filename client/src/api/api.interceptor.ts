import axios, { CreateAxiosDefaults, AxiosError } from 'axios';
import { AuthService } from '@/services/auth/auth.service';
import { getAccessToken, removeFromStorage } from '@/services/auth/token.service';
import { errorCatch, getContentType } from './api.helper';

const axiosOptions: CreateAxiosDefaults = {
	baseURL: process.env.SERVER_URL,
	headers: getContentType()
}

export const instance = axios.create(axiosOptions);

instance.interceptors.request.use(async config => {
	const accessToken = getAccessToken();
	if (config.headers && accessToken) config.headers.Authorization = `Bearer ${accessToken}`;
	return config;
});

AxiosError<{ __isRetry?: boolean }>;

instance.interceptors.response.use(
	config => config,
	async err => {
		const originalRequest= err.config;

		if (
			(err?.response?.status === 401 ||
				errorCatch(err) === 'jwt expired' ||
				errorCatch(err) === 'jwt must be provided') &&
			err.config &&
			!err.config.__isRetry
		) {
			originalRequest.__isRetry = true;
			try {
				await AuthService.getNewTokens();
				return instance.request(originalRequest);
			} catch (error) {
				if (errorCatch(error) == 'jwt expired') {
					removeFromStorage();
				}
			}
		}

		throw err;
	}
);
