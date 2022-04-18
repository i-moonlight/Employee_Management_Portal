import cn from 'clsx';
import { FC, PropsWithChildren } from 'react';

interface HeadingName {
	className?: string;
}

const Heading: FC<PropsWithChildren<HeadingName>> = ({ className, children }) => {
	return (
		<h1 className={cn('font-semibold text-3xl', className)}>
			{children}
		</h1>
	);
}

export default Heading;
