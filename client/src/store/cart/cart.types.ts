import { CartItem } from '@/models/cart.interface';

export interface CartInitialState {
	items: CartItem[];
}

export interface AddToCartPayload extends Omit<CartItem, 'id'> {
}

export interface ChangeQuantityPayload extends Pick<CartItem, 'id'> {
	type: 'minus' | 'plus';
}

export type TypeSize = 'SHORT' | 'TALL' | 'GRAND' | 'VENTI';

export interface ChangeSizePayload extends Pick<CartItem, 'id'> {
	size: TypeSize;
}
