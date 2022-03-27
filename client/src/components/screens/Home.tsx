import { FC } from 'react';
import Meta from '@/ui/Meta';
import { TypePaginationProducts } from '@/models/product.interface';
import Layout from '@/ui/layout/Layout'

const Home: FC<TypePaginationProducts> = ({ products, length }) => {
	// const { logout } = useActions();
	return (
		<Meta title='Home'>
			<Layout>
				{/*<Catalog data={{ products, length }} title='Fresh products' />*/}
			</Layout>
		</Meta>
	);
}

export default Home;
