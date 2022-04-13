import { useRouter } from 'next/router';
import { useQuery } from '@tanstack/react-query';
import { errorCatch } from '@/api/api.helper';
import { FullUser } from '@/models/user.interface';
import { UserService } from '@/services/user.service';
import { useAuth } from './useAuth';

export const useProfile = () => {
	const { user } = useAuth();
	const router = useRouter();
	const { data } = useQuery(['get profile'], () => UserService.getUserProfile(), {
		select: ({ data }) => data,
		onError: (error) => {
			if (errorCatch(error) === 'Unauthorized') router.push('/auth');
			else console.log(errorCatch(error));
		},
		enabled: !!user
	});
	console.log(data);
	return { profile: data || ({} as FullUser) };
}
