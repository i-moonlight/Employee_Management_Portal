import { GetStaticProps, NextPage } from 'next';
import { ProductService } from 'services/product/product.service';
import { TypePaginationProducts } from 'models/product.interface';
import Home from '@/screens/Home';

const HomePage: NextPage<TypePaginationProducts> = ({ products, length }) => {
	return <Home products={products} length={length} />
}

export const getStaticProps: GetStaticProps<TypePaginationProducts> = async () => {
	const data = await ProductService.getAllProducts({
		page: 1,
		perPage: 4
	});
	return {
		props: data
	}
}

export default HomePage;
