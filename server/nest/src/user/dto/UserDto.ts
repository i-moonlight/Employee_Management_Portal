import { IsOptional, IsString } from 'class-validator';

export class UserDto {

  @IsOptional()
  @IsString()
  password?: string

  @IsOptional()
  @IsString()
  name?: string

  @IsOptional()
  @IsString()
  avatarPath?: string

  @IsOptional()
  @IsString()
  phone?: string
}
