import { Controller, Get } from '@nestjs/common';
import { ForAuth } from 'src/auth/decorator/auth.decorator';
import { UserService } from './user.service';
import { CurrentUser } from '../auth/decorator/user.decorator';

@Controller('user')
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Get('profile')
  @ForAuth()
  async getProfile(@CurrentUser('id') id: string) {
    return this.userService.getUserById(id);
  }
}
