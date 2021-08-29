namespace WebAPI.Authentication.Models
{
    public class Response
    {
        public Response(ResponseCode responseCode, string responseMessage, object dataSet)
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
