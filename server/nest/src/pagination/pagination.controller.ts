import { Controller } from '@nestjs/common';
import { PaginationService } from './pagination.service';

@Controller('pagination')
export class PaginationController {
  constructor(private readonly paginationService: PaginationService) {}
}
