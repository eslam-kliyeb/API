namespace E_Commerce.API.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int StatusCode ,  string? errorMessage = null, string? details = null)
            : base(StatusCode,errorMessage)
        {
            Details = details;
        }
    }
}
