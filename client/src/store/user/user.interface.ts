<<<<<<< HEAD
=======
import { User } from '@/models/user.interface';

>>>>>>> 7edc363 (feat(client): catalog pagination)
export interface UserState {
	email: string;
	isAdmin: boolean;
}

export interface Tokens {
	accessToken: string;
	refreshToken: string;
}

export interface InitialState {
<<<<<<< HEAD
=======
	// user: IUser | null
>>>>>>> 7edc363 (feat(client): catalog pagination)
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
