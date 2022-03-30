import { useRouter } from 'next/router'
import { FC, PropsWithChildren } from 'react'
import Header from './header/Header'
import ProfileSidebar from './sidebar/ProfileSidebar'
import Sidebar from './sidebar/Sidebar'

const profileUrls = ['/profile', '/favorites', '/orders']

interface Layout {
	showSidebar?: boolean
}

const Layout: FC<PropsWithChildren<Layout>> = ({ children, showSidebar = true }) => {
	const { asPath } = useRouter()

	return (
		<div>
			<Header />
			{showSidebar ? (
				<div
					className='grid h-screen'
					style={{ gridTemplateColumns: '1fr 4fr' }}
				>
					{profileUrls.includes(asPath) ? <ProfileSidebar /> : <Sidebar />}
					<main className='p-12'>{children}</main>
				</div>
			) : (
				<div className='grid h-screen'>
					<main className='p-12'>{children}</main>
				</div>
			)}
		</div>
	)
}

export default Layout
