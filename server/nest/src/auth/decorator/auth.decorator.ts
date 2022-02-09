import { UseGuards } from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';

export const ForAuth = () => UseGuards(AuthGuard('jwt'));
