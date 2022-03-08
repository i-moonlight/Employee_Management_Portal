import { Body, Controller, Delete, Get, HttpCode, Param, Post, Put, UsePipes, ValidationPipe } from '@nestjs/common'
import { Authorize } from '../auth/decorator/auth.decorator'
import { CurrentUser } from '../auth/decorator/user.decorator'
import { ReviewDto } from './dto/review.dto'
import { ReviewService } from './review.service'

@Controller('review')
export class ReviewController {
  constructor(private readonly reviewService: ReviewService) {}

  @HttpCode(200)
  @UsePipes(new ValidationPipe())
  @Post('create/:productId')
  @Authorize()
  async createReview(
     @CurrentUser('id') id: string,
     @Body() dto: ReviewDto,
     @Param('productId') productId: string)
  {
    return this.reviewService.createReview(id, dto, productId);
  }

  @HttpCode(200)
  @UsePipes(new ValidationPipe())
  @Get('')
  async getReviewList() {
    return this.reviewService.getReviews();
  }

  @HttpCode(200)
  @Get(':id')
  @Authorize()
  async getReviewById(@Param('id') id: string) {
    return this.reviewService.getReview(id);
  }

  @Get('average-by-product/:productId')
  async getAverageByProduct(@Param('productId') productId: string) {
    return this.reviewService.getAverageValueByProductId(productId)
  }

  @HttpCode(200)
  @Put(':id')
  async updateReview(@Param('id') id: string, @Body() dto: ReviewDto) {
    return this.reviewService.updateReview(id, dto);
  }

  @HttpCode(200)
  @Delete(':id')
  async deleteReview(@Param('id') id: string) {
    return this.reviewService.deleteReview(id);
  }
}
