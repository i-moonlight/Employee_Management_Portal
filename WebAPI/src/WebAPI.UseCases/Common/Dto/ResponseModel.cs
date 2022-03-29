#nullable enable
namespace WebAPI.UseCases.Common.Dto
{
    /// <summary>
    /// Universal response model.
    /// </summary>
    public class ResponseModel
    {
        public ResponseCode? Code { get; set; }
        public bool IsValid { get; set; }
        public string? Message { get; set; }
        public dynamic? DataSet { get; set; }
        
        public ResponseModel() {}

        public ResponseModel(ResponseCode responseCode, string responseMessage, object dataSet)
        {
            Code = responseCode;
            Message = responseMessage;
            DataSet = dataSet;
        }
        
        public ResponseModel(ResponseCode responseCode, bool isValid, string responseMessage, object dataSet)
        {
            Code = responseCode;
            IsValid = isValid;
            Message = responseMessage;
            DataSet = dataSet;
        }
    }

    public enum ResponseCode
    {
        Ok = 1,
        Error = 2
    }
}