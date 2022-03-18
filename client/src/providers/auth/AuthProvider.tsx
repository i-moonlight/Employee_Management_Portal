import { useActions } from '@/hooks/useActions';
import { useAuth } from '@/hooks/useAuth';
import { TypeComponentAuthFields } from '@/models/auth';
import { getAccessToken, getRefreshToken } from '@/services/auth/token.service';
import dynamic from 'next/dynamic';
import { usePathname } from 'next/navigation';
import { FC, PropsWithChildren, useEffect } from 'react';

// Чтобы динамически загружать компонент на стороне клиента, можно использовать ssr опцию отключения рендеринга на сервере.
// Это полезно, если внешняя зависимость или компонент полагается на API браузера, такие как window.
const DynamicCheckRole = dynamic(() => import('./CheckRole'), {
	ssr: false
});

const AuthProvider: FC<PropsWithChildren<TypeComponentAuthFields>> =
	({ children, Component: { isOnlyUser } }) => {

		const pathname = usePathname();
		const { user } = useAuth();
		const { checkAuth, logout } = useActions();

		// Проверка токена доступа при первой загрузке страницы
		// Если токен существует, проверяем его валидность
		useEffect(() => {
			const accessToken = getAccessToken();
			if (accessToken) checkAuth();
		}, []);

		// При каждом изменении страницы проверяем токен обновления
		// Если токен не существует, выкидываем пользователя из приложения
		useEffect(() => {
			const refreshToken = getRefreshToken();
			if (!refreshToken && user) logout();
		}, [pathname]);

		return isOnlyUser
			? <DynamicCheckRole Component={{ isOnlyUser }}>{children}</DynamicCheckRole>
			: (<>{children}</>)
	}

export default AuthProvider;
