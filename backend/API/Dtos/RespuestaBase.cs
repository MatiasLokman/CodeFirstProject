namespace API.Dtos
{
    public class RespuestaBase
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
