import { Order } from './order.interface';
import { Product } from './product.interface';

export interface User {
	id: string;
	email: string;
	name: string;
	avatarPath: string;
	phone: string;
	isAdmin: boolean;
}

export interface FullUser extends User {
	favorites: Product[];
	orders: Order[];
}
