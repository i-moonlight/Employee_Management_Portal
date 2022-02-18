import { Module } from '@nestjs/common';
import { CategoryService } from './category.service';
import { CategoryController } from './category.controller';
import { PrismaService } from '../../prisma/prisma.service';

@Module({
  controllers: [CategoryController],
  providers:   [CategoryService, PrismaService]
})
export class CategoryModule {}
