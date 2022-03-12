import { TypePaginationProducts } from '@/models/product.interface'
import { SITE_DESCRIPTION } from '@/constans/app.constants'

export const Home = ({ products, count }: TypePaginationProducts) => {
	return (
		<>
			<h2>{SITE_DESCRIPTION}</h2>
		</>
	)
}
