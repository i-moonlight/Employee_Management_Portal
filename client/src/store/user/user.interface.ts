import { User } from '@/models/user.interface';

export interface UserState {
	email: string;
	isAdmin: boolean;
}

export interface Tokens {
	accessToken: string;
	refreshToken: string;
}

export interface InitialState {
	user: UserState | null;
	isLoading: boolean;
}

export interface EmailPassword {
	email: string;
	password: string;
}

export interface AuthResponse extends Tokens {
	user: User;
}
