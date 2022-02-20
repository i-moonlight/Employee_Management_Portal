import { Prisma } from '@prisma/client';
import { returnUserObject } from '../../user/dto/user.response';

export const returnReviewObject: Prisma.ReviewSelect = {
  id: true,
  rating: true,
  text: true,
  user: {
    select: returnUserObject
  },
  createdAt: true
}
