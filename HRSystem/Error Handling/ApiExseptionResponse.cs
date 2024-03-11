namespace HRSystem.Error_Handling
{
    public class ApiExseptionResponse : ErrorResponse
    {
        public string Details { get; set; }
        public ApiExseptionResponse(int statusCode, string message = null, string details = null):base(statusCode,message)
        {
            Details = details;
        }
    }
}
