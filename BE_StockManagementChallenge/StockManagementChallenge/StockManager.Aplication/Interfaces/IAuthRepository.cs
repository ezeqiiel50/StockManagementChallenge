using ROP;
using StockManager.Application.DTOs.Login;

namespace StockManager.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<Result<UserData>> GetUserByUswerName(string userName);
    }
}
