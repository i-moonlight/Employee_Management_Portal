import { Prisma } from '@prisma/client';

export const returnUserObject: Prisma.UserSelect = {
  id:         true,
  name:       true,
  email:      true,
  avatarPath: true,
  password:   false,
  phone:      true,
  favourites: true
}

// common response
export const userObjectResponse: Prisma.UserSelect = {
  ...returnUserObject,
}
