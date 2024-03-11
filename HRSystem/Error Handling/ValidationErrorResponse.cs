namespace HRSystem.Error_Handling
{
    // Handel Validation Errors (ModelState) and only display message,statusCode and errors 
    //Special type of BadRequst() error
    public class ValidationErrorResponse : ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ValidationErrorResponse() : base(400) { }
    }
}




