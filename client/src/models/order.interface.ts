import { CartItem } from './cart.interface';
import { User } from './user.interface';

export enum EnumOrderStatus {
	PENDING = 'PENDING',
	PAYED = 'PAYED',
	SHIPPED = 'SHIPPED',
	DELIVERED = 'DELIVERED'
}

export type TypeOrderData = {
	status?: EnumOrderStatus;
	items: {
		productId: string
		quantity: number
		price: number
	}[]
}

export interface Order {
	id: string;
	createdAt: string;
	items: CartItem[];
	status: EnumOrderStatus;
	user: User;
	total: number;
}

export interface IConfirmation {
	confirmation: {
		confirmation_url: string
	}
}
