import { Statistic } from '@/models/statistics.interface';
import { instance } from '@/api/api.interceptor';

export const StatisticsService = {
	async getMain() {
		return instance<Statistic[]>({
			url: `statistics/main`,
			method: 'GET'
		});
	}
}
