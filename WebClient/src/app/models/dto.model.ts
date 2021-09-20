export class Dto {
  private UserName?: string;
  private _Email?: string;
  private Password?: string;

  get Email(): string {
    return <string>this._Email;
  }
}
