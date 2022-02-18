import { Body, Controller, Delete, Get, HttpCode, Param, Post, Put } from '@nestjs/common';
import { CategoryService } from './category.service';
import { CategoryDto } from './dto/category.dto';
import { Authorize } from '../auth/decorator/auth.decorator';

@Controller('category')
export class CategoryController {
  constructor(private readonly categoryService: CategoryService) {}

  @HttpCode(200)
  @Get('list')
  async getCategoryList() {
    return this.categoryService.getCategories();
  }

  @HttpCode(200)
  @Get(':id')
  @Authorize()
  async getCategoryById(@Param('id') categoryId: string) {
    return this.categoryService.getCategoryById(categoryId);
  }

  @HttpCode(200)
  @Get('by-slug/:slug')
  async getCategoryBySlug(@Param('slug') slug: string) {
    return this.categoryService.getCategoryBySlug(slug);
  }

  @HttpCode(200)
  @Authorize()
  @Post('')
  async createCategory(@Body() dto: CategoryDto) {
    return this.categoryService.createCategory(dto);
  }

  @HttpCode(200)
  @Authorize()
  @Put(':id')
  async updateCategory(@Param('id') categoryId: string, @Body() dto: CategoryDto) {
    return this.categoryService.updateCategory(categoryId, dto);
  }

  @HttpCode(200)
  @Authorize()
  @Delete(':id')
  async deleteCategory(@Param('id') categoryId: string) {
    return this.categoryService.deleteCategory(categoryId);
  }
}
