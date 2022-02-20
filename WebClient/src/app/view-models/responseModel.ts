export class ResponseModel {
  public responseCode: ResponseCode = ResponseCode.NotSet;
  public responseMessage: string = '';
  public dateSet: any
}

export enum ResponseCode {
  NotSet = 0,
  OK = 1,
  Error = 2
}
