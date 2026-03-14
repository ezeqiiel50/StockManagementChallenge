using ROP;
using StockManager.Application.DTOs.Login;

namespace StockManager.Application.Interfaces
{
    public interface IJwtService
    {
        Task<Result<string>> GenerateToken(UserData user);
    }
}
