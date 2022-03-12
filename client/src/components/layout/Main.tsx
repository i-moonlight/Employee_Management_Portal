import cn from 'clsx'
import { FC, PropsWithChildren } from 'react'

interface IMain {
	className?: string
}

const Main: FC<PropsWithChildren<IMain>> = ({ children, className }) => {
	return <main className={cn('py-14 sm:py-21', className)}>
		{children}
	</main>
}

export default Main
