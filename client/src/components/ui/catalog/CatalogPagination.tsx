import { FC, useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { ProductService } from '@/services/product/product.service';
import { EnumProductSort } from '@/services/product/product.types';
import { TypePaginationProducts } from '@/models/product.interface';

interface Catalog {
	data: TypePaginationProducts;
	title?: string;
}

const Catalog: FC<Catalog> = ({ data, title }) => {
	const [sortType, setSortType] = useState<EnumProductSort>(EnumProductSort.NEWEST);
	const [page, setPage] = useState<number>(1);

	const { data: response, isLoading } = useQuery(['product', sortType, page], () =>
		ProductService.getAllProducts({
			page,
			perPage: 4,
			sort: sortType
		})
	);

	return (
		<section>
			<div>There are no products</div>
		</section>
	);
}

export default Catalog;
