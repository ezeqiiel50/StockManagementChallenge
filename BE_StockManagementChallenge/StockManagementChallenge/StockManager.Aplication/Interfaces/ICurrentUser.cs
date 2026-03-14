namespace StockManager.Application.Interfaces
{
    public interface ICurrentUser
    {
        int Id { get; }
        string UserName { get; }
    }
}
