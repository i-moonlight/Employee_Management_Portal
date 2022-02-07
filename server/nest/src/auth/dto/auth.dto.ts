import { IsEmail, IsString, MinLength } from 'class-validator';

export class AuthDto {
  @IsString()
  name: string

  @IsEmail()
  email: string

  @MinLength(6, {
    message: 'Password must be at least 6 character long'
  })
  @IsString()
  password: string
}
