using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ROP;
using StockManager.Application.DTOs.Login;
using StockManager.Application.Interfaces;
using StockManager.Data.Context;
using StockManager.Data.StoredProcedures;

namespace StockManager.Data.Repositories
{
    public class AuthRepository(ILogger<AuthRepository> logger, AppDbContext context) : IAuthRepository
    {
        public async Task<Result<UserData>> GetUserByUswerName(string userName)
        {
            try
            {
                var param = new SqlParameter("@Username", userName);

                var spResult = await context.Set<UserSpResult>()
                                            .FromSqlRaw("EXEC sp_GetUserByUsername @Username", param)
                                            .ToListAsync();

                var item = spResult.FirstOrDefault();
                if (item is null)
                {
                    logger.LogInformation("No se encontró el userName.");
                    return Result.NotFound<UserData>("Credenciales inválidas.");
                }

                var result = new UserData
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    PasswordHash = item.PasswordHash,
                };
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ah ocurrido un error al obtener el user");
                return Result.Failure<UserData>("Credenciales inválidas.");
            }
        }
    }
}