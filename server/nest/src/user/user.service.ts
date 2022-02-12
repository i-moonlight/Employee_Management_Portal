import { BadRequestException, Injectable, NotFoundException } from '@nestjs/common';
import { PrismaService } from '../../prisma/prisma.service';
import { Prisma } from '@prisma/client';
import { userObjectResponse } from './dto/user.response';
import { UserDto } from './dto/user.dto';
import { hash } from 'argon2';

@Injectable()
export class UserService {
  constructor(private prisma: PrismaService) {}

  async getUserById(id: string, selectObject: Prisma.UserSelect = {}) {
    const user = await this.prisma.user.findUnique({
      where: {
        id: id
      },
      select: {
        ...userObjectResponse,
        ...selectObject
      }
    });

    if (!user) throw new NotFoundException('User not found!');

    delete user.password;

    return user
  }

  async updateProfile(id: string, dto: UserDto) {
    const isSameUser = await this.prisma.user.findUnique({
      where: { email: dto.email }
    });

    if (isSameUser && id !== isSameUser.id) {
      throw new BadRequestException('Email already in use');
    }

    const user = await this.getUserById(id);

    return this.prisma.user.update({
      where: {
        id: id
      },
      data: {
        name: dto.name,
        email: dto.email,
        avatarPath: dto.avatarPath,
        phone: dto.phone,
        password: dto.password ? await hash(dto.password) : user.password
      }
    });
  }

  async toggleFavorite(userId: string, productId: string) {
    const user = await this.getUserById(userId);

    if (!user) throw new NotFoundException('User not found!');

    const isExists = user.favourites.some(p => p.id == productId);

    await this.prisma.user.update({
      where: {
        id: user.id
      },
      data: {
        favourites: {
          [isExists ? 'disconnect' : 'connect']: { id: productId }
        }
      }
    });
  }
}
