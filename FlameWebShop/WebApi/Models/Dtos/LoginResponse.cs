namespace WebApi.Models.Dtos
{
    public class LoginResponse
    {
        public string role { get; set; }
        public string token { get; set; }
        public string? Email { get; set; }
    }
}
