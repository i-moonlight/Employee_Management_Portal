import { User } from './user.interface';

export interface IReview {
	id: string
	user: User
	createdAt: string
	text: string
	rating: number
}
