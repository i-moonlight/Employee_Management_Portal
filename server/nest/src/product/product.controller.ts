import { Body, Controller, Delete, Get, HttpCode, Param, Post, Put, Query, UsePipes, ValidationPipe } from '@nestjs/common';
import { Authorize } from '../auth/decorator/auth.decorator';
import { GetAllProductDto } from './dto/get-all.product.dto';
import { ProductDto } from './dto/product.dto';
import { ProductService } from './product.service';

@Controller('products')
export class ProductController {
  constructor(private readonly productService: ProductService) {}

  @HttpCode(200)
  @Get()
  async getAllProducts(@Query() dto: GetAllProductDto) {
    return this.productService.getAllProducts(dto);
  }

  @HttpCode(200)
  @Get(':productId')
  async getProductById(@Param('productId') productId: string) {
    return this.productService.getProductById(productId);
  }

  @HttpCode(200)
  @Get('by-category/:category')
  async getProductByCategory(@Param('category') category: string) {
    return this.productService.getProductByCategory(category);
  }

  @HttpCode(200)
  @Get('by-slug/:slug')
  async getProductBySlug(@Param('slug') slug: string) {
    return this.productService.getProductBySlug(slug);
  }
}
