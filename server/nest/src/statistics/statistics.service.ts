import { Injectable } from '@nestjs/common';
import { PrismaService } from '../../prisma/prisma.service';
import { UserService } from '../user/user.service';

@Injectable()
export class StatisticsService {
  constructor(
     private prisma: PrismaService,
     private userService: UserService) {}

  async getMainStatistics() {
    const ordersCount  = await this.prisma.order.count();
    const reviewsCount = await this.prisma.review.count();
    const usersCount   = await this.prisma.user.count();
    const ordersInfo   = await this.prisma.order.aggregate({
      _sum: {
        totalAmount: true
      },
      _avg: {
        totalAmount: true
      },
      _min: {
        totalAmount: true
      },
      _max: {
        totalAmount: true
      }
    });
    return [
      {
        name: 'Users count',
        value: usersCount
      },
      {
        name: 'Reviews count',
        value: reviewsCount
      },
      {
        name: 'Orders count',
        value: ordersCount
      },
      {
        name: 'Total amount',
        value: ordersInfo._sum.totalAmount ? ordersInfo._sum.totalAmount : '-'
      },
      {
        name: 'Average order amount',
        value: ordersInfo._avg.totalAmount ? ordersInfo._avg.totalAmount : '-'
      },
      {
        name: 'Minimal order amount',
        value: ordersInfo._min.totalAmount ? ordersInfo._min.totalAmount : '-'
      },
      {
        name: 'Maximum order amount',
        value: ordersInfo._max.totalAmount ? ordersInfo._max.totalAmount : '-'
      },
    ]
  }

  async getUserStatistics(userId: string) {
    const user = await this.userService.getUserById(userId, {
      orders: {
        select: {
          orderItems: true
        }
      },
      reviews: true
    })
    return [
      {
        name: 'Orders',
        value: user.orders.length
      },
      {
        name: 'Reviews',
        value: user.reviews.length
      },
    ]
  }
}
