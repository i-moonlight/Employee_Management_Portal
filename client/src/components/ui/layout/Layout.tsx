import { FC, PropsWithChildren } from 'react';
import Header from '@/components/ui/layout/Header'
import Sidebar from '@/components/ui/layout/Sidebar'


const Layout: FC<PropsWithChildren<unknown>> = ({ children }) => {
	return (
		<div>
			<Header />
			<div className='grid grid-cols-layout' style={{minHeight:'calc(100vh - 91px)'}}>
				<Sidebar />
				<main className='p-12 pb-0'>{children}</main>
			</div>
		</div>
	);
};

export default Layout;
