import { IsEnum, IsOptional, IsString } from 'class-validator';
import { PaginationDto } from 'src/pagination/pagination.dto';

export enum ProductSortEnum {
  HIGH_PRICE = 'high-price',
  LOW_PRICE  = 'low-price',
  NEWEST     = 'newest',
  OLDEST     = 'oldest'
}

export class GetAllProductDto extends PaginationDto {
  @IsOptional()
  @IsEnum(ProductSortEnum)
  sort?: ProductSortEnum;

  @IsOptional()
  @IsString()
  searchTerm?: string;
}
