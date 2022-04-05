import type { GetStaticProps, NextPage } from 'next';
import type { TypeProductPagination } from '@/models/product.interface';
import { ProductService } from '@/services/product/product.service';
import Home from '@/pages';

const HomePage: NextPage<TypeProductPagination> = ({ products, length }) => {
	return <Home products={products} length={length} />
}

export const getStaticProps: GetStaticProps<TypeProductPagination> = async () => {
	const data = await ProductService.getAllProducts({
		page: 1,
		perPage: 4
	});
	return {
		props: data
	}
}

export default HomePage;
