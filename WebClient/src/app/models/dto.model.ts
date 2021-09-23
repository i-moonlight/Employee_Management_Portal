export class Dto {
  private UserName?: string;
  private _Email?: string;
  private Password?: string;
  private _ResetPasswordUrl?: string;

  get Email(): string {
    return <string>this._Email;
  }

  get ResetPasswordUrl(): string {
    return <string>this._ResetPasswordUrl;
  }
}
