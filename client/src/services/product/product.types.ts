import { IPagination } from '@/models/pagination.interface'

export const PRODUCTS = 'products'

export type typeProductData = {
	name:         string
	price:        number
	description?: string
	images:       string[]
	categoryId:   number
}

type SortData = {
	sort?:       EnumProductSort | string
	searchTerm?: string
	ratings:     string
	minPrice?:   string
	maxPrice?:   string
	categoryId?: string
}

export type TypeProductDataFilters = SortData & IPagination

export type TypeParamsFilters = {
	searchParams: TypeProductDataFilters
}

export enum EnumProductSort {
	HIGH_PRICE = 'height-price',
	LOW_PRICE  = 'low-price',
	NEWEST     = 'newest',
	OLDEST     = 'oldest'
}
