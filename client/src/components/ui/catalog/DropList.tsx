import type { Dispatch, FC, SetStateAction } from 'react';
import { EnumProductSort } from '@/services/product/product.types';

interface SortDropdown {
	sortType: EnumProductSort;
	setSortType: Dispatch<SetStateAction<EnumProductSort>>;
}

const DropList: FC<SortDropdown> = ({ sortType, setSortType }) => {
	return (
		<div className='w-48 ml-auto mb-6 relative border border-gray text-gray-800 bg-white shadow-lg'>
			<select
				className='appearance-none w-full  py-1 px-2 bg-white'
				name='whatever'
				id='frm-whatever'
				value={sortType}
				onChange={e => setSortType(e.target.value as any)}
			>

				<option value='' disabled>Please choose&hellip;</option>

				{(
					Object.keys(EnumProductSort) as Array<keyof typeof EnumProductSort>).map(key =>
					(<option key={key} value={EnumProductSort[key]}>
						{EnumProductSort[key]}
					</option>)
				)}
			</select>

			<div
				className='pointer-events-none absolute right-0 top-0 bottom-0 flex items-center px-2 text-gray-700 border-l'>
				<svg
					className='h-4 w-4'
					xmlns='http://www.w3.org/2000/svg'
					viewBox='0 0 20 20'
				>
					<path d='M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z' />
				</svg>
			</div>
		</div>
	);
}

export default DropList;
