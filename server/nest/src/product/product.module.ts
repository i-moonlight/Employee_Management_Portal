import { Module } from '@nestjs/common';
import { CategoryService } from '../category/category.service';
import { PaginationService } from '../pagination/pagination.service';
import { ProductService } from './product.service';
import { ProductController } from './product.controller';
import { PrismaService } from '../../prisma/prisma.service';

@Module({
  controllers: [ProductController],
  providers:   [
     ProductService,
     PrismaService,
     PaginationService,
     CategoryService
  ]
})
export class ProductModule {}
