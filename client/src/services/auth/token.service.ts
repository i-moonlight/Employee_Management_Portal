import { AuthResponse, Tokens } from '@/store/user/user.interface';
import Cookies from 'js-cookie';

const ACCESS_TOKEN = 'accessToken';
const REFRESH_TOKEN = 'refreshToken';

export const getAccessToken = () => {
	const accessToken = Cookies.get(ACCESS_TOKEN);
	return accessToken || null;
}

export const getRefreshToken = () => {
	const refreshToken = Cookies.get(REFRESH_TOKEN);
	return refreshToken || null;
}

export const getUserFromStorage = () => {
	return JSON.parse(localStorage.getItem('user') || '{}');
}

export const removeFromStorage = () => {
	Cookies.remove(ACCESS_TOKEN);
	Cookies.remove(REFRESH_TOKEN);
	localStorage.removeItem('user');
}

export const saveToStorage = (data: AuthResponse) => {
	saveTokensStorage(data);
	localStorage.setItem('user', JSON.stringify(data.user));
}

export const saveTokensStorage = (data: Tokens) => {
	Cookies.set(ACCESS_TOKEN, data.accessToken);
	Cookies.set(REFRESH_TOKEN, data.refreshToken);
}
