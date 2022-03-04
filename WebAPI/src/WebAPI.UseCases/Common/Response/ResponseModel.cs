namespace WebAPI.UseCases.Common.Response
{
    /// <summary>
    /// Response model.
    /// </summary>
    public class ResponseModel
    {
        public ResponseModel(ResponseCode responseCode, string responseMessage, object dataSet)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            DateSet = dataSet;
        }

        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object DateSet { get; set; }
    }

    public enum ResponseCode
    {
        Ok = 1,
        Error = 2
    }
}