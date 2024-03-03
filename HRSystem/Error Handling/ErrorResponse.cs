
namespace HRSystem.Error_Handling
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultStatusCodeMessage(statusCode);
        }

         string DefaultStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "You've made A Bad Requst !",
                401 => "unAuthorized",
                404 => "Not Found the Resource",
                500 => "Serverside Error",
                _ => null
            };
        }
    }
}
