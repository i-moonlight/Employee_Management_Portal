import { Category } from './category.interface';
import { Review } from './review.interface';

export interface IProduct {
	id: string;
	name: string;
	slug: string
	description: string;
	price: number;
	reviews: Review[];
	images: string[];
	category: Category;
	createdAt: string;
}

export interface ProductDetails {
	product: Product;
}

export type TypeProducts = {
	products: Product[];
}

export type TypePaginationProducts = {
	count: number;
	products: Product[];
}
