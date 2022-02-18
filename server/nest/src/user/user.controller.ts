import { Body, Controller, Get, HttpCode, Param, Patch, Put, UsePipes, ValidationPipe } from '@nestjs/common';
import { Authorize } from '../auth/decorator/auth.decorator';
import { CurrentUser } from '../auth/decorator/user.decorator';
import { UserDto } from './dto/user.dto';
import { UserService } from './user.service';

@Controller('user')
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Get('profile')
  @Authorize()
  async getProfile(@CurrentUser('id') id: string) {
    return this.userService.getUserById(id);
  }

  @HttpCode(200)
  @Put('profile/update')
  @UsePipes(new ValidationPipe())
  @Authorize()
  async updateProfile(@CurrentUser('id') id: string, @Body() dto: UserDto) {
    return this.userService.updateProfile(id, dto);
  }

  @HttpCode(200)
  @Patch('profile/favorite/:productId')
  @Authorize()
  async toggleFavorite(@CurrentUser('id') userId: string, @Param('productId') productId: string) {
    return this.userService.toggleFavorite(userId, productId);
  }
}
