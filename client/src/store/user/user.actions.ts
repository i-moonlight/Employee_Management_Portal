import { createAsyncThunk } from '@reduxjs/toolkit';
import { errorCatch } from '@/api/api.helper';
import { AuthResponse, EmailPassword } from './user.interface';
import { AuthService } from '@/services/auth/auth.service';
import { removeFromStorage } from '@/services/auth/token.service'

export const register = createAsyncThunk<AuthResponse, EmailPassword>(
	'auth/signup',
	async (data, thunkApi) => {
		try {
			const response = await AuthService.main('register', data);
			return response;
		} catch (error) {
			return thunkApi.rejectWithValue(error);
		}
	}
)

export const login = createAsyncThunk<AuthResponse, EmailPassword>(
	'auth/signin',
	async (data, thunkApi) => {
		try {
			const response = await AuthService.main('login', data)
			return response
		} catch (error) {
			return thunkApi.rejectWithValue(error)
		}
	}
);

export const logout = createAsyncThunk('auth/signout', async () => {
	removeFromStorage();
});

export const checkAuth = createAsyncThunk<AuthResponse>(
	'auth/check-auth',
	async (_, thunkApi) => {
		try {
			const response = await AuthService.getNewTokens()
			return response.data
		} catch (error) {
			if (errorCatch(error) === 'jwt expired') {
				thunkApi.dispatch(logout())
			}

			return thunkApi.rejectWithValue(error)
		}
	}
);
