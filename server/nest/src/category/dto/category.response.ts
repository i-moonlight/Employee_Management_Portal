import { Prisma } from '@prisma/client';

export const categoryObjectResponse: Prisma.CategorySelect = {
  id:   true,
  name: true,
  slug: true
}
