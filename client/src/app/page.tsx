import Link from 'next/link'
import { FC } from 'react'
import Footer from '@/layout/Footer'
import Header from '@/layout/Header'
import { Home } from '@/components/pages/Home'
import Main from '@/layout/Main'

const HomePage: FC = () => {
	return (
		<>
			<Header>
				<Link href='/recipes'>Open Recipes</Link>
			</Header>
			<Main>
				<Home />
			</Main>
			<Footer />
		</>
	)
}

export default HomePage
