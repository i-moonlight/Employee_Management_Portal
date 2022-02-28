import { BadRequestException, Injectable, InternalServerErrorException, NotFoundException } from '@nestjs/common';
import { Prisma } from '@prisma/client';
import { CategoryService } from '../category/category.service';
import { GetAllProductDto, ProductSortEnum } from './dto/get-all.product.dto';
import { PaginationService } from '../pagination/pagination.service';
import { PrismaService } from '../../prisma/prisma.service';
import { ProductDto } from './dto/product.dto';
import { returnProductObject, returnProductObjectComposite } from './dto/return-product.object';
import generateSlug from '../../content/slug';

@Injectable()
export class ProductService {
  constructor(
     private prisma: PrismaService,
     private paginationService: PaginationService,
     private categoryService: CategoryService) {
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
      where:  {id: id},
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
      where:  {slug: slug},
      select: returnProductObjectComposite
    })
    if (!product) throw new NotFoundException('Product not found!');
    return product;
  }

  async getSimilarProduct(id: string) {
    const currentProduct = await this.getProductById(id);
    return await this.prisma.product.findMany({
      where: {
        category: {name: currentProduct.category.name},
        NOT:      {id: currentProduct.id}
      },
      orderBy: {createdAt: 'desc'},
      select:  returnProductObjectComposite
    })
  }

  async createProduct(dto: ProductDto) {
    try {
      const {name, price, description, images, categoryId} = dto;
      return await this.prisma.product.create({
        data: {
          name:        name,
          slug:        generateSlug(name),
          price:       price,
          description: description,
          images:      images,
          category:    {connect: {id: categoryId}}
        }
      })
    } catch (error) {
      if (error.code === 'P2002') {
        throw new BadRequestException('Product with this name already exists!');
      } else if (error.code === 'P2025') {
        throw new BadRequestException(`Category with id ${dto.categoryId} not found!`);
      }
      throw new InternalServerErrorException(`Something went wrong! ${error.message}`);
    }
  }

  async updateProduct(id: string, dto: ProductDto) {
    try {
      const {name, price, description, images, categoryId} = dto;
      const product = await this.getProductById(id);
      if (categoryId) await this.categoryService.getCategoryById(categoryId);
      return await this.prisma.product.update({
        where: { id: id },
        data: {
          name:        name        ? name               : product.name,
          slug:        name        ? generateSlug(name) : product.slug,
          price:       price       ? price              : product.price,
          description: description ? description        : product.description,
          images:      images      ? images             : product.images,
          categoryId:  categoryId  ? categoryId         : product.categoryId
        }
      })
    } catch (error) {
      if (error.code === 'P2025') {
        throw new BadRequestException(`${error.meta.cause}`);
      }
      throw new InternalServerErrorException(`categoryId is undefined`);
    }
  }

  async deleteProduct(id: string) {
    await this.getProductById(id);
    return await this.prisma.product.delete({where: {id}});
  }
}
