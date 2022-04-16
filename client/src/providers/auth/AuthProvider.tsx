import { useEffect, PropsWithChildren, FC } from 'react';
import dynamic from 'next/dynamic';
import { useRouter } from 'next/router';
import { useActions } from '@/hooks/useActions';
import { useAuth } from '@/hooks/useAuth';
import { getAccessToken, getRefreshToken } from '@/services/auth/token.service';
import type { TypeComponentAuthFields } from '@/providers/auth/auth-page.types';

// Чтобы динамически загружать компонент на стороне клиента,
// можно использовать ssr опцию отключения рендеринга на сервере.
// Это полезно, если внешняя зависимость или компонент полагается на API браузера, такие как window.
const DynamicCheckRole = dynamic(() => import('./CheckRole'), { ssr: false });

const AuthProvider: FC<PropsWithChildren<TypeComponentAuthFields>> = ({ Component: { isOnlyUser }, children, }) => {

	const pathname = useRouter();
	const { user } = useAuth();
	const { checkAuth, logout } = useActions();

	// Проверка токена доступа при первой загрузке страницы
	// Если токен существует, проверяем его валидность
	useEffect(() => {
		const accessToken = getAccessToken();
		if (accessToken) checkAuth();
	}, []); // eslint-disable-line react-hooks/exhaustive-deps

	// При каждом изменении страницы проверяем токен обновления
	// Если токен не существует, выкидываем пользователя из приложения
	useEffect(() => {
		const refreshToken = getRefreshToken();
		if (!refreshToken && user) logout();
	}, [pathname]); // eslint-disable-line react-hooks/exhaustive-deps

	return isOnlyUser // если страница требует авторизации
		? <DynamicCheckRole Component={{ isOnlyUser }}>{children}</DynamicCheckRole> // то делаем проверку на клиенте
		: (<>{children}</>)
}

export default AuthProvider;
