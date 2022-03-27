import { FC, PropsWithChildren } from 'react';
import cn from 'clsx';

interface Heading {
	className?: string;
}

const Heading: FC<PropsWithChildren<Heading>> = ({ className, children }) => {
	return (
		<h1 className={cn('font-semibold text-3xl', className)}>
			{children}
		</h1>
	);
}

export default Heading;
