namespace Platform.Certificate.API.Common.Helpers;

public class ResponseError : IResponseError
{
    public ResponseError(int? errorTypeCode, string message, string errorDetails = "")
    {
        ErrorCode = errorTypeCode ?? 0;
        Message = message;
        DetailMessage = errorDetails;
    }
    public ResponseError(string message, string errorDetails = "")
    {
        Message = message;
        DetailMessage = errorDetails;
    }
    public string Message { get; set; }
    public string DetailMessage { get; set; }
    public int ErrorCode { get; set; }
    public override string ToString()
    {
        return Message;
    }
}