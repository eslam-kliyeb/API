namespace E_Commerce.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode {  get; set; }
        public string? ErrorMessage { get; set; }
        public ApiResponse(int statusCode, string? errorMassage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMassage?? GetErrorMessageForStatusCode(statusCode);
        }
        private string? GetErrorMessageForStatusCode(int statusCode)
        => statusCode switch
        {
            500 => "Internal Server Error",
            404 => "No Found",
            401 => "UnAuthorized",
            400 => "BadRequest",
            _ => ""
        };
        
    }
}
