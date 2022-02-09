import { Injectable, NotFoundException } from '@nestjs/common';
import { PrismaService } from '../../prisma/prisma.service';
import { Prisma } from '@prisma/client';
import { returnUserObject } from './user.object';

@Injectable()
export class UserService {
  constructor(private prisma: PrismaService) {
  }

  async getUserById(id: string, selectObject: Prisma.UserSelect = {}) {
    const user = await this.prisma.user.findUnique({
      where: {
        id: id
      },
      select: {
        ...returnUserObject,
        ...selectObject
      }
    });
    if (!user) throw new NotFoundException('User not found!')
    delete user.password
    return user
  }
}
