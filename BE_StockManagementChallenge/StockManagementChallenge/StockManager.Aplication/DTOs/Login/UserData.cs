namespace StockManager.Application.DTOs.Login
{
    public class UserData
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
