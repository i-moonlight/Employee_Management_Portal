import { Injectable, NotFoundException } from '@nestjs/common';
import { Prisma } from '@prisma/client';
import { GetAllProductDto, ProductSortEnum } from './dto/get-all.product.dto';
import { PaginationService } from '../pagination/pagination.service';
import { PrismaService } from '../../prisma/prisma.service';
import { returnProductObject, returnProductObjectComposite } from './dto/return-product.object';

@Injectable()
export class ProductService {
  constructor(
     private prisma: PrismaService,
     private paginationService: PaginationService) {
  }

  async getAllProducts(dto: GetAllProductDto = {}) {
    const { searchTerm, sort } = dto;
    const prismaPriceSort: Prisma.ProductOrderByWithRelationInput[] = [];
    switch (sort) {
      case ProductSortEnum.HIGH_PRICE: prismaPriceSort.push({price: 'desc'});
      case ProductSortEnum.LOW_PRICE:  prismaPriceSort.push({price: 'asc'});
      case ProductSortEnum.OLDEST:     prismaPriceSort.push({createdAt: 'asc'});
      default: prismaPriceSort.push({createdAt: 'desc'});
    };
    const prismaSearchTermFilter: Prisma.ProductWhereInput = searchTerm ? {
      OR: [
         {
           category: {
             name: {
               contains: searchTerm,
               mode: 'insensitive'
             },
           },
         },
        {
          name: {
            contains: searchTerm,
            mode: 'insensitive'
          },
        },
        {
          description: {
            contains: searchTerm,
            mode: 'insensitive'
          },
        },
      ]
    } : {};
    const { perPage, skip } = this.paginationService.getPagination(dto);
    const products = await this.prisma.product.findMany({
      where:   prismaSearchTermFilter,
      orderBy: prismaPriceSort,
      skip,
      take:    perPage
    });
    return {
      products,
      length:  await this.prisma.product.count({
        take:  perPage,
        where: prismaSearchTermFilter
      })
    };
  }

  async getProductById(id: string) {
    const product = await this.prisma.product.findUnique({
      where: {id: id},
      select: returnProductObjectComposite
    })
    if (!product) throw new NotFoundException('Product not found!')
    return product;
  }

  async getProductByCategory(categoryName: string) {
    const product = await this.prisma.product.findMany({
      where: { category: { name: categoryName } },
      select: returnProductObject,
    });
    if (!product) throw new NotFoundException('Product not found');
    return product;
  }

  async getProductBySlug(slug: string) {
    const product = await this.prisma.product.findUnique({
      where: {slug: slug},
      select: returnProductObjectComposite
    })
    if (!product) throw new NotFoundException('Product not found!');
    return product;
  }
}
