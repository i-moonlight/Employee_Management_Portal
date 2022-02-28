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

  @HttpCode(200)
  @Get('similar/:productId')
  async getSimilarProduct(@Param('productId') id: string) {
    return this.productService.getSimilarProduct(id);
  }

  @HttpCode(201)
  @Post()
  async createProduct(@Body() dto: ProductDto) {
    return this.productService.createProduct(dto);
  }

  @HttpCode(200)
  @UsePipes(new ValidationPipe())
  @Put(':id')
  async updateProduct(@Param('id') id: string, @Body() dto: ProductDto) {
    return this.productService.updateProduct(id, dto);
  }

  @HttpCode(200)
  @Authorize()
  @Delete(':id')
  async deleteProduct(@Param('id') id: string) {
    return this.productService.deleteProduct(id);
  }
}
