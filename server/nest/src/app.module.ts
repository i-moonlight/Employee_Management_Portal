import { ConfigModule } from '@nestjs/config';
import { Module } from '@nestjs/common';
import { AuthModule } from './auth/auth.module';
import { PrismaService } from '../prisma/prisma.service';
import { UserModule } from './user/user.module';

@Module({
  imports:     [AuthModule, ConfigModule.forRoot(), UserModule],
  controllers: [],
  providers:   [PrismaService],
})
export class AppModule {}
