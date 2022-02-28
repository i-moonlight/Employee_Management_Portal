import { ConfigModule } from '@nestjs/config';
import { Module } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { CategoryModule } from './category/category.module';
import { PaginationModule } from './pagination/pagination.module';
import { PrismaService } from '../prisma/prisma.service';
import { ProductModule } from './product/product.module';
import { ReviewModule } from './review/review.module';
import { StatisticsModule } from './statistics/statistics.module';
import { UserModule } from './user/user.module';

@Module({
  imports: [
     AuthModule,
     ConfigModule.forRoot(),
     UserModule,
     CategoryModule,
     ReviewModule,
     StatisticsModule,
     PaginationModule,
     ProductModule
  ],
  controllers: [],
  providers: [PrismaService],
})
export class AppModule {}
