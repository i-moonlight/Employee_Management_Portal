import { Controller, Get } from '@nestjs/common';
import { Authorize } from '../auth/decorator/auth.decorator';
import { CurrentUser } from '../auth/decorator/user.decorator';
import { StatisticsService } from './statistics.service';

@Controller('statistics')
export class StatisticsController {
  constructor(private readonly statistics: StatisticsService) {}

  @Get('main')
  getMainStatistics() {
    return this.statistics.getMainStatistics();
  }

  @Get('user-statistics')
  @Authorize()
  getUserStatistics(@CurrentUser('id') id: string) {
    return this.statistics.getUserStatistics(id);
  }
}
