import { FC } from 'react';
import Meta from '@/ui/Meta';
import CatalogPagination from '@/ui/catalog/CatalogPagination';
import { useActions } from '@/hooks/useActions';
import { useAuth } from '@/hooks/useAuth';
import { TypePaginationProducts } from '@/models/product.interface';
import Layout from '@/layout/Layout'

const Home: FC<TypePaginationProducts> = ({ products, length }) => {
	const { user } = useAuth();
	const { logout } = useActions();

	return (
		<Meta title='Home'>
			<Layout>
				{!!user && <button onClick={() => logout()}></button>}
				<CatalogPagination
					title='Freshed products'
					data={{ products, length }}
				/>
			</Layout>
		</Meta>
	);
}

export default Home;
