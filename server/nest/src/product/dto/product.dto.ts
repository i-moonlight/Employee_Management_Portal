import { ArrayMinSize, IsNumber, IsOptional, IsString } from 'class-validator';
import { Prisma } from '@prisma/client';

export class ProductDto implements Prisma.ProductUpdateInput {
  @IsString()
  name: string

  @IsNumber()
  price: number

  @IsString()
  @IsOptional()
  description: string

  @IsString({each: true})
  @ArrayMinSize(1)
  images: string[]

  @IsString()
  categoryId: string
}
