import type { FC } from 'react';
import { RiShoppingCartFill, RiShoppingCartLine } from 'react-icons/ri';
import { useActions } from '@/hooks/useActions';
import { useCart } from '@/hooks/useCart';
import { Product } from '@/models/product.interface';

const AddToCartButton: FC<{ product: Product }> = ({ product }) => {
	const { addToCart, removeFromCart } = useActions();
	const { items } = useCart();
	const currentElement = items?.find(cartItem => cartItem.product.id === product.id);

	return (
		<div>
			<button className='text-secondary' onClick={() => currentElement
				? removeFromCart({ id: currentElement.id })
				: addToCart({ product, quantity: 1, price: product.price })
			}>
				{currentElement ? <RiShoppingCartFill /> : <RiShoppingCartLine />}
			</button>
		</div>
	);
}

export default AddToCartButton;
