namespace StockManager.Application.DTOs.Login
{
    public class LoginRequest
    {
        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
