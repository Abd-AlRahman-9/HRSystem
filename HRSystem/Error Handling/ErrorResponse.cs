
namespace HRSystem.Error_Handling
{
    // Handel 400,401,404 errors and only display message and statusCode
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
                // 400 is validation error "BadRequst()"
                400 => "You've made A Bad Requst !",
                401 => "unAuthorized",
                404 => "Page Not Found",
                500 => "Serverside Error",
                _ => null
            };
        }
    }
}
