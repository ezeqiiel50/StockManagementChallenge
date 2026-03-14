namespace StockManager.Data.StoredProcedures
{
    public class UserSpResult
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
