import { Module } from '@nestjs/common';
import { StatisticsService } from './statistics.service';
import { StatisticsController } from './statistics.controller';
import { PrismaService } from '../../prisma/prisma.service';
import { UserService } from '../user/user.service';

@Module({
  controllers: [StatisticsController],
  providers:   [StatisticsService, PrismaService, UserService],
  exports:     [UserService]
})
export class StatisticsModule {}
