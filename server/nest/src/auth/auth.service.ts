import { BadRequestException, Injectable } from '@nestjs/common';
import { CreateAuthDto } from './dto/create-auth.dto';
import { RefreshTokenDto } from './dto/refresh-token.dto';
import { PrismaService } from '../../prisma/prisma.service';
import { hash } from 'argon2';
import { JwtService } from '@nestjs/jwt';
import { User } from '@prisma/client';

@Injectable()
export class AuthService {
  constructor(
     private prisma: PrismaService,
     private jwt: JwtService) {}

  async register(dto: CreateAuthDto) {
    const existUser = await this.prisma.user.findUnique({
      where: {
        email: dto.email
      }
    })

    if (existUser) throw new BadRequestException('User already exists')

    const user = await this.prisma.user.create({
      data: {
        name: dto.name,
        email: dto.email,
        password: await hash(dto.password)
      }
    })

    const tokens = await this.issueTokens(user.id)

    // Response model dto
    return {
      response: this.returnUserFields(user),
      ...tokens
    }
  }

  private async issueTokens(userId: number) {
    const data = { id : userId }

    const accessToken = this.jwt.sign(data, {
      expiresIn: '1h',
    })

    const refreshToken = this.jwt.sign(data, {
      expiresIn: '7d',
    })

    return { accessToken, refreshToken }
  }

  private returnUserFields(user: Partial<User>) {
    return {
      id: user.id,
      email: user.email
    }
  }
}
