import { Prisma } from '@prisma/client';

export const returnUserPartialObject: Prisma.UserSelect = {
  id: true,
  name: true,
  email: true,
  avatarPath: true,
  password: false,
  phone: true,
}

export const returnUserObject: Prisma.UserSelect = {
  ...returnUserPartialObject,
  avatarPath: true,
  password: true
}
