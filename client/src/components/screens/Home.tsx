import type { FC } from 'react';
import type { TypeProductPagination } from '@/models/product.interface';
import Catalog from '@/components/ui/catalog/CatalogPagination';
import Layout from '@/components/ui/layout/Layout';
import Meta from '@/components/ui/Meta';

const Home: FC<TypeProductPagination> = ({ products, length }) => {
	return (
		<Meta title='Home'>
			<Layout>
				<Catalog
					data={{products, length}}
					title='Fresh products'
				/>
			</Layout>
		</Meta>
	);
}

export default Home;
