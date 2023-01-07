namespace EcommerceAPI.Application.Response
{
    public class ResponseApi
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public ResponseApi (bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
