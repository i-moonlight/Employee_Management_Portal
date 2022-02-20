import { Injectable, NotFoundException } from '@nestjs/common';
import { PrismaService } from '../../prisma/prisma.service';
import { ReviewDto } from './dto/review.dto';
import { returnReviewObject } from './dto/return-review.object';

@Injectable()
export class ReviewService {
  constructor(private prisma: PrismaService) {}

  async createReview(userId: string, dto: ReviewDto, productId: string) {
    return await this.prisma.review.create({
      data: {
        ...dto,
        product: {
          connect: { id: productId }
        },
        user: {
          connect: { id: userId }
        }
      }
    });
  }

  async getReviews() {
    return await this.prisma.review.findMany({
      orderBy: { createdAt: 'desc' },
      select:  returnReviewObject
    });
  }

  async getReview(id: string) {
    const review = await this.prisma.review.findUnique({
      where: { id: id },
      select: returnReviewObject,
    })
    if (!review) throw new NotFoundException('Review not found!')
    return review
  }

  async updateReview(id: string, dto: ReviewDto) {
    await this.getReview(id)
    return await this.prisma.review.update({
      where: { id: id },
      data:  { ...dto }
    });
  }

  async deleteReview(id: string) {
    await this.getReview(id)
    return await this.prisma.review.delete({where: {id}});
  }

  async getAverageValueByProductId(productId: string) {
    return this.prisma.review.aggregate({
      where: { productId },
      _avg:  { rating: true }
    }).then(data => data._avg)
  }
}
