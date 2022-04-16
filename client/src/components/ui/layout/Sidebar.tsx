import clsx from 'clsx';
import Link from 'next/link';
import { useRouter } from 'next/router';
import type { FC } from 'react';
import { FiLogOut } from 'react-icons/fi';
import { useQuery } from '@tanstack/react-query';
import { useActions } from '@/hooks/useActions';
import { useAuth } from '@/hooks/useAuth';
import { CategoryService } from '@/services/category.service';
import Loader from '@/components/ui/Loader';

const Sidebar: FC = () => {
	const { data, isLoading } = useQuery(
		['get categories'],
		() => CategoryService.getAllCategories(),
		{ select: ({ data }) => data }
	);

	const { asPath } = useRouter();
	const { user } = useAuth();
	const { logout } = useActions();

	return (
		<aside className='bg-secondary flex flex-col justify-between h-full'>
			<div> {isLoading
				? (<Loader />)
				: data
					? (<>
						<div className='text-xl text-white mt-4 mb-6 px-10'>Categories:</div>
						<ul>
							{data.map(category => (
								<li key={category.id}>
									<Link
										title={category.slug}
										className={clsx(
											'block text-md my-5 px-10 hover:text-primary transition-colors duration-200 whitespace-nowrap text-ellipsis overflow-hidden',
											asPath === `/category/${category.slug}`
												? 'text-primary'
												: 'text-white'
										)}
										href={`/category/${category.slug}`}
									>
										{category.slug}
									</Link>
								</li>
							))}
						</ul>
					</>)
					: (<div>Category not found!</div>)}
			</div>
			{!!user && (
				<button
					onClick={() => logout()}
					className='text-white hover:text-primary transition-colors duration-200 flex items-center ml-10 mb-8 py-2'>
					<FiLogOut />
					<span className='ml-2'>Logout</span>
				</button>
			)}
		</aside>
	);
}

export default Sidebar;
