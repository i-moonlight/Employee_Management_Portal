import { UseGuards } from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';

export const Authorize = () => UseGuards(AuthGuard('jwt'));
