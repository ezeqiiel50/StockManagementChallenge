using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ROP;
using StockManager.Application.DTOs.Product;
using StockManager.Application.Interfaces;
using StockManager.Data.Context;
using StockManager.Data.StoredProcedures;

namespace StockManager.Data.Repositories
{
    public class ProductoRepository(ILogger<ProductoRepository> logger, AppDbContext context) : IProductoRepository
    {
        public async Task<Result<ProductoResponse>> GetById(int Id)
        {
            try
            {
                var param = new SqlParameter("@Id", Id);

                var spResult = await context.Set<ProductoSpResult>()
                                            .FromSqlRaw("EXEC sp_GetProductById @Id", param)
                                            .ToListAsync();

                var item = spResult.FirstOrDefault();
                if (item is null)
                {
                    logger.LogInformation("No se encontró el producto.");
                    return Result.NotFound<ProductoResponse>("No se encontró el producto.");
                }

                var result = new ProductoResponse
                {
                    Id = item.Id,
                    Precio = item.Price,
                    Categoria = item.Category,
                    FechaCarga = item.CreatedAt.ToString("dd/MM/yyyy")
                };
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ah ocurrido un error al obtener el producto");
                return Result.Failure<ProductoResponse>("Ah ocurrido un error al obtener el producto.");
            }
        }

        public async Task<Result<ROP.Unit>> Delete(int Id)
        {
            try
            {
                var param = new SqlParameter("@Id", Id);

                var rowsAffected = await context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteProduct @Id", param);

                if (rowsAffected == 0)
                {
                    logger.LogInformation("No se elimino el producto.");
                    return Result.Failure<ROP.Unit>("No se elimino el producto.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ha ocurrido un error al eliminar el producto");
                return Result.Failure<ROP.Unit>("Ha ocurrido un error al eliminar el producto.");
            }
        }
    }
}
