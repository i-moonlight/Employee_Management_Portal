import { User } from './user.interface';

export interface Review {
	id: string;
	user: User;
	createdAt: string;
	text: string;
	rating: number;
}
