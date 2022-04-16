import { useRouter } from 'next/router';
import type { FC, PropsWithChildren } from 'react';
import { useAuth } from '@/hooks/useAuth';
import type { TypeComponentAuthFields } from '@/providers/auth/auth-page.types';

const CheckRole: FC<PropsWithChildren<TypeComponentAuthFields>> = ({Component: {isOnlyUser}, children}) => {

	const router = useRouter();
	const { user } = useAuth();

	// Проверка авторизации
	if (user && isOnlyUser) return <>{children}</>
	router.pathname !== '/auth' && router.replace('/auth');
	return null;
}

export default CheckRole;
