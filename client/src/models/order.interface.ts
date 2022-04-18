import { User } from './user.interface';
import { CartItem } from '@/models/cart.interface';

// export enum EnumOrderStatus {
// 	PENDING = 'PENDING',
// 	PAYED = 'PAYED',
// 	SHIPPED = 'SHIPPED',
// 	DELIVERED = 'DELIVERED'
// }

export type TypeOrderData = {
	// status?: EnumOrderStatus;
	items: {
		productId: string;
		quantity: number;
		price: number;
	}[]
}

export interface Order {
	id: string;
	createdAt: string;
	items: CartItem[];
	// status: EnumOrderStatus;
	user: User;
	total: number;
}

export interface Confirmation {
	confirmation: {
		confirmation_url: string;
	}
}
