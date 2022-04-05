import type { FC } from 'react';
import type { TypeProductPagination } from '@/models/product.interface';
import Meta from '@/components/ui/Meta';
import Heading from '@/components/ui/Heading';
import Layout from '@/components/ui/layout/Layout';

const Home: FC<TypeProductPagination> = ({ products, length }) => {
	return (
		<Meta title='Home'>
			<Layout>
				<Heading> Hello world</Heading>
			</Layout>
		</Meta>
	);
}

export default Home;
