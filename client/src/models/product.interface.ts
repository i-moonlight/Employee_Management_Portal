import { Category } from './category.interface';
import { Review } from './review.interface';
import { EnumProductSort } from '@/services/product/product.types';

export interface Product {
	id: string;
	name: string;
	slug: string;
	description: string;
	price: number;
	reviews: Review[];
	images: string[];
	createdAt: string;
	category: Category;
}

export interface ProductDetails {
	product: Product;
}

export type TypeProductData = {
	name: string;
	price: number;
	description?: string;
	images: string[];
	categoryId: number;
}

export type TypeProductDataFilter = {
	sort?: EnumProductSort;
	searchTerm?: string;
	page?: string | number;
	perPage?: string | number;
}

export type TypeProducts = {
	products: Product[];
}

export type TypeProductPagination = {
	products: Product[];
	length: number;
}
