import { ConfigModule } from '@nestjs/config';
import { Module } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { PrismaService } from '../prisma/prisma.service';
import { UserModule } from './user/user.module';
import { CategoryModule } from './category/category.module';

@Module({
  imports:     [AuthModule, ConfigModule.forRoot(), UserModule, CategoryModule],
  controllers: [],
  providers:   [PrismaService],
})
export class AppModule {}
