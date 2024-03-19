
namespace HRSystem.Error_Handling
{
    // Handel all statusCode messages and only display message and statusCode
    public class StatusResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public StatusResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultStatusCodeMessage(statusCode);
        }

         string DefaultStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                200 => "Succsess",
                201 => "Created Successfully",
                204 => "All is done",
                // 400 is validation error "BadRequst()"
                400 => "You've made A Bad Requst !",
                401 => "unAuthorized",
                404 => "Page Not Found",
                500 => "Serverside Error",
                _ => null
            } ; 
        }
    }
}
