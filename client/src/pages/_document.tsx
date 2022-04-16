import { Head, Html, Main, NextScript } from 'next/document';

export default function Document() {
	return (
		<Html lang='en'>
			<Head>
				<link rel='icon' href='/favicon.svg' sizes='any' type='image/svg+xml' />
			</Head>
			<body>
			<Main />
			<NextScript />
			</body>
		</Html>
	);
}
