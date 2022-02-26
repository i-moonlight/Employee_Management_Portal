import { Injectable, NotFoundException } from '@nestjs/common';
import { CategoryDto } from './dto/category.dto';
import { PrismaService } from '../../prisma/prisma.service';

import Slug from '../../content/slug';
import {returnCategoryObject} from "./dto/return-category.object";

@Injectable()
export class CategoryService {
  constructor(private prismaService: PrismaService) {}

  async createCategory(dto: CategoryDto) {
    return await this.prismaService.category.create({
      data: {
        name: dto.name,
        slug: Slug(dto.name)
      }
    });
  }

  async getCategories() {
    return await this.prismaService.category.findMany({
      select: returnCategoryObject
    });
  }

  async getCategoryById(id: string) {
    const category = await this.prismaService.category.findUnique({
      where: {id: id},
      select: returnCategoryObject,
    });
    if (!category) throw new NotFoundException('Category not found!');
    return category;
  }

  async getCategoryBySlug(slug: string) {
    const category = await this.prismaService.category.findUnique({
      where: { slug: slug },
      select: returnCategoryObject,
    });
    if (!category) throw new NotFoundException('Category not found!');
    return category;
  }

  async updateCategory(id: string, dto: CategoryDto) {
    return await this.prismaService.category.update({
      where: { id: id },
      data: {
        name: dto.name,
        slug: Slug(dto.name)
      }
    });
  }

  async deleteCategory(id: string) {
    return await this.prismaService.category.delete({
      where: { id }
    })
  }
}
