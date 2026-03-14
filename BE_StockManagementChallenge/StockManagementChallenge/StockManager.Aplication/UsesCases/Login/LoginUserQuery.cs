using MediatR;
using ROP;
using StockManager.Application.DTOs.Login;

namespace StockManager.Application.UsesCases.Login
{
    public class LoginUserQuery(LoginRequest request) : IRequest<Result<LoginResponse>>
    {
        public string User { get; } = request.User;
        public string Password { get;  } = request.Password;
    }
}
