import { IsEmail, IsOptional, IsString } from 'class-validator';

export class UserDto {

  @IsOptional()
  @IsString()
  name?: string

  @IsOptional()
  @IsEmail()
  email?: string

  @IsOptional()
  @IsString()
  password?: string

  @IsOptional()
  @IsString()
  avatarPath?: string

  @IsOptional()
  @IsString()
  phone?: string
}
