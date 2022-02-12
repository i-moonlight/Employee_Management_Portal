import { Body, Controller, Get, HttpCode, Param, Patch, Put, UsePipes, ValidationPipe } from '@nestjs/common';
import { ForAuth } from 'src/auth/decorator/auth.decorator';
import { UserService } from './user.service';
import { UserDto } from './dto/user.dto';
import { CurrentUser } from '../auth/decorator/user.decorator';

@Controller('user')
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Get('profile')
  @ForAuth()
  async getProfile(@CurrentUser('id') id: string) {
    return this.userService.getUserById(id);
  }

  @HttpCode(200)
  @Put('profile/update')
  @UsePipes(new ValidationPipe())
  @ForAuth()
  async updateProfile(@CurrentUser('id') id: string, @Body() dto: UserDto) {
    return this.userService.updateProfile(id, dto);
  }

  @HttpCode(200)
  @Patch('profile/favorite/:productId')
  @ForAuth()
  async toggleFavorite(@CurrentUser('id') userId: string, @Param('productId') productId: string) {
    return this.userService.toggleFavorite(userId, productId);
  }
}
