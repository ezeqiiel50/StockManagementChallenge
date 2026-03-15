using MediatR;
using Microsoft.Extensions.Logging;
using ROP;
using StockManager.Application.DTOs.Login;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Login
{
    public class LoginUserHandler(ILogger<LoginUserHandler> logger, IAuthRepository authRepository,
        IJwtService jwtService) : IRequestHandler<LoginUserQuery, Result<LoginResponse>>
    {
        public Task<Result<LoginResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var result = LoginUser(request);
            return result;
        }

        private async Task<Result<LoginResponse>> LoginUser(LoginUserQuery request)
        {
            var result = await authRepository.GetUserByUserName(request.User)
                                .Bind(x => ValidarPassword(x, request))
                                .Bind(x => jwtService.GenerateToken(x))
                                .Map(x => new LoginResponse
                                {
                                    Token = x,
                                });
            return result;
        }
        private async Task<Result<UserData>> ValidarPassword(UserData user, LoginUserQuery request)
        {
            try
            {
                bool passwordCorrecta = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
                if (!passwordCorrecta)
                {
                    logger.LogWarning("Contraseña incorrecta para el usuario.");
                    return Result.Failure<UserData>("Credenciales inválidas.");
                }
                return await user.Success().Async();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ah ocurrido un error al procesar el user.");
                return Result.Failure<UserData>("Ah ocurrido un error al procesar el user.");
            }
        }
    }
}
