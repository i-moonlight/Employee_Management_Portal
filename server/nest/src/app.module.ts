import { ConfigModule } from '@nestjs/config';
import { Module } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { PrismaService } from '../prisma/prisma.service';
import { UserModule } from './user/user.module';
import { CategoryModule } from './category/category.module';
import { ReviewModule } from './review/review.module';
import { StatisticsModule } from './statistics/statistics.module';
import { ProductModule } from './product/product.module';

@Module({
  imports: [
     AuthModule,
     ConfigModule.forRoot(),
     UserModule,
     CategoryModule,
     ReviewModule,
     StatisticsModule,
     ProductModule
  ],
  controllers: [],
  providers: [PrismaService],
})
export class AppModule {}
