import { useRouter } from 'next/router';
import { useAuth } from '@/hooks/useAuth';

const CheckRole: ({ Component: { isOnlyUser }, children }: { Component: { isOnlyUser: any }, children: any }) =>
	(JSX.Element | null) = ({ Component: { isOnlyUser }, children }) => {

	const router = useRouter();
	const { user } = useAuth();

	// Проверка авторизации
	if (user && isOnlyUser) return <>{children}</>
	router.pathname !== '/auth' && router.replace('/auth');
	return null;
}

export default CheckRole;
