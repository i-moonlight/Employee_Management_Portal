export class User {
  public userId: string = '';
  public fullName: string = '';
  public email: string = '';
  public userName: string = '';
  public roles: string[] = [];

  constructor(id: string, fullName: string, email: string, userName: string, roles: string[]) {
    this.userId = id;
    this.fullName = fullName;
    this.email = email;
    this.userName = userName;
    this.roles = roles;
  }
}
