export class Response {
  public ResponseCode: ResponseCode = ResponseCode.NotSet;
  public ResponseMessage: string;
  public DateSet: any
}

export enum ResponseCode {
  NotSet = 0,
  OK = 1,
  Error = 2
}
