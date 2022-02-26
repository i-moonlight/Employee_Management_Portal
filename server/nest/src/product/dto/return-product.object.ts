import { Prisma } from '@prisma/client';
import { returnCategoryObject } from '../../category/dto/return-category.object';
import { returnReviewObject } from '../../review/dto/return-review.object';

export const returnProductObject: Prisma.ProductSelect = {
  id:          true,
  name:        true,
  slug:        true,
  price:       true,
  description: true,
  images:      true,
  createdAt:   true,
  category:    { select: returnCategoryObject }
}

export const returnProductObjectComposite: Prisma.ProductSelect = {
  ...returnProductObject,
  reviews: {
    select: returnReviewObject,
    orderBy: {
      createdAt: 'desc'
    }
  }
}
