import { ConfigModule } from '@nestjs/config';
import { Module } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { PrismaService } from '../prisma/prisma.service';

@Module({
  imports:     [AuthModule, ConfigModule.forRoot()],
  controllers: [],
  providers:   [PrismaService],
})
export class AppModule {}
