import { BadRequestException, Injectable, NotFoundException, UnauthorizedException } from '@nestjs/common';
import { PrismaService } from '../../prisma/prisma.service';
import { hash, verify } from 'argon2';
import { JwtService } from '@nestjs/jwt';
import { User } from '@prisma/client';
import { AuthDto } from './dto/auth.dto';

@Injectable()
export class AuthService {
  constructor(private prisma: PrismaService, private jwt: JwtService) {}

  async register(dto: AuthDto) {
    const existUser = await this.prisma.user.findUnique({
      where: {
        email: dto.email
      }
    });

    if (existUser) throw new BadRequestException('User already exists');

    const user = await this.prisma.user.create({
      data: {
        name: dto.name,
        email: dto.email,
        password: await hash(dto.password)
      }
    });

    const tokens = await this.issueTokens(user.id);

    // Response model dto
    return {
      response: this.returnUserFields(user),
      ...tokens
    }
  }

  private async issueTokens(userId: string) {
    const data = { id : userId }

    const accessToken = this.jwt.sign(data, {
      expiresIn: '1h',
    });

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

  async login(dto: AuthDto) {
    const user = await this.validateUser(dto);
    const tokens = await this.issueTokens(user.id);
    
    return {
      user: this.returnUserFields(user),
      ...tokens
    }
  }

  async getNewTokens(refreshToken: string) {
    const verify = await this.jwt.verifyAsync(refreshToken);
    
    if (!verify) throw new UnauthorizedException('Invalid refresh token');

    const user = await this.prisma.user.findUnique({
      where: {
        id: verify.id
      }
    });

    const tokens = await this.issueTokens(user.id);

    // Response model dto
    return {
      response: this.returnUserFields(user),
      ...tokens
    }
  }

  private async validateUser(dto: AuthDto) {
    const user = await this.prisma.user.findUnique({
      where: {
        email: dto.email
      }
    });

    if (!user) throw new NotFoundException('Invalid credentials!');

    const isValid = await verify(user.password, dto.password);

    if (!isValid) throw new NotFoundException('Invalid credentials!');

    return user
  }
}
