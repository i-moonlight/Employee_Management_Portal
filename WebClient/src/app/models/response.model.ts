export class Response {
  public ResponseCode: ResponseCode = ResponseCode.NotSet;
  public IsValid!: boolean; // Flag
  public Message!: string;
  public DateSet: any
}

export enum ResponseCode {
  NotSet = 0,
  OK = 1,
  Error = 2
}
