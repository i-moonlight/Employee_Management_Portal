import Head from 'next/head';
import { useRouter } from 'next/router';
import { FC, PropsWithChildren } from 'react';

interface Seo {
	title: string;
	description?: string;
	image?: string;
}

export const titleMerge = (title: string) => `${title} | E-commerce`;

const Meta: FC<PropsWithChildren<Seo>> = ({ title, description, image, children }) => {

	const { asPath } = useRouter();
	const currentUrl = `${process.env.APP_URL}${asPath}`;

	return (
		<>
			<Head>
				<title itemProp='headline'>{titleMerge(title)}</title>
				{description
					? (<>
						<meta itemProp='description' name='description' content={description} />
						<link rel='canonical' href={currentUrl} />
						<meta property='og:locale' content='en' />
						<meta property='og:title' content={titleMerge(title)} />
						<meta property='og:url' content={currentUrl} />
						<meta property='og:image' content={image || '/favicon.svg'} />
						{/* <meta property='og:site_name' content='sitename'/> */}
						<meta property='og:description' content={description} />
					</>)
					: (<meta property='robots' content='noindex, nofolow' />)
				}
			</Head>
			{children}
		</>
	);
}

export default Meta;
