import Image from 'next/image';
import Link from 'next/link';
import { AiOutlineHeart } from 'react-icons/ai';
import type { FC, PropsWithChildren } from 'react';

const Header: FC<PropsWithChildren<unknown>> = () => {
	return (
		<div className='relative bg-secondary w-full py-6 px-6 grid grid-cols-header z-50'>
			<Link href={'/'}>
				<Image
					priority
					width={180}
					height={37}
					src='/amazon-logo.png'
					alt='Amazon'
				/>
			</Link>
			{/*<Search />*/}
			<div className='flex items-center justify-end lg:gap-6 gap-2'>
				<Link href='/favorites' className='text-white'>
					<AiOutlineHeart size={28} />
				</Link>
			</div>
		</div>
	);
}

export default Header;
