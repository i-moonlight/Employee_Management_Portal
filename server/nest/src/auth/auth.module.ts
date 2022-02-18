import { ConfigModule, ConfigService } from '@nestjs/config';
import { Module } from '@nestjs/common';
import { JwtModule } from '@nestjs/jwt';
import { AuthService } from './auth.service';
import { AuthController } from './auth.controller';
import { PrismaService } from '../../prisma/prisma.service';
import { getJwtConfig } from '../jwt/jwt.config';
import { JwtStrategy } from '../jwt/jwt-strategy';

@Module({
  controllers: [AuthController],
  providers:   [AuthService, PrismaService, JwtStrategy],
  imports:     [ConfigModule, JwtModule.registerAsync({
      imports:    [ConfigModule],
      inject:     [ConfigService],
      useFactory: getJwtConfig
     })
  ]
})
export class AuthModule {}
