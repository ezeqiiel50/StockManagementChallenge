using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ROP;
using StockManager.Application.DTOs.Product.Model;
using StockManager.Application.DTOs.Product.Response;
using StockManager.Application.Interfaces;
using StockManager.Data.Context;
using StockManager.Data.StoredProcedures;

namespace StockManager.Data.Repositories
{
    public class ProductoRepository(ILogger<ProductoRepository> logger, AppDbContext context) : IProductoRepository
    {
        public async Task<Result<ProductItemResponse>> GetById(int Id)
        {
            try
            {
                var param = new SqlParameter("@Id", Id);

                var spResult = await context.Set<ProductSpResult>()
                                            .FromSqlRaw("EXEC sp_GetProductById @Id", param)
                                            .ToListAsync();

                var item = spResult.FirstOrDefault();
                if (item is null)
                {
                    logger.LogInformation("No se encontró el producto.");
                    return Result.NotFound<ProductItemResponse>("No se encontró el producto.");
                }

                var result = new ProductItemResponse
                {
                    Id = item.Id,
                    Descripcion = item.Description,
                    Precio = item.Price,
                    Categoria = item.Category,
                    FechaCarga = item.CreatedAt.ToString("dd/MM/yyyy")
                };
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ah ocurrido un error al obtener el producto");
                return Result.Failure<ProductItemResponse>("Ah ocurrido un error al obtener el producto.");
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

                if (rowsAffected == -1)
                {
                    logger.LogInformation("No existe un producto con ese Id.");
                    return Result.Failure<ROP.Unit>("No existe un producto con ese Id.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ha ocurrido un error al eliminar el producto");
                return Result.Failure<ROP.Unit>("Ha ocurrido un error al eliminar el producto.");
            }
        }

        public async Task<Result<int>> Create(ProductModel model)
        {
            try
            {
                var spResult = await context.Set<ProductSpResult>()
                                            .FromSqlRaw("EXEC sp_CreateProduct @Price, @Description, @Category, @CreatedBy",
                                                new SqlParameter("@Price", model.Precio),
                                                new SqlParameter("@Description", model.Descripcion),
                                                new SqlParameter("@Category", model.Categoria),
                                                new SqlParameter("@CreatedBy", model.UserId))
                                            .ToListAsync();

                var newId = spResult.FirstOrDefault()?.Id;
                if (newId == -1)
                {
                    logger.LogInformation("Ya existe un producto con la descripción {Description}.", model.Descripcion);
                    return Result.Failure<int>("Ya existe un producto con esa descripción.");
                }

                if (newId is null || newId == 0)
                {
                    logger.LogInformation("No se pudo crear el producto.");
                    return Result.Failure<int>("No se pudo crear el producto.");
                }

                return Result.Success(newId.Value);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ha ocurrido un error al crear el producto.");
                return Result.Failure<int>("Ha ocurrido un al crear el producto.");
            }
        }

        public async Task<Result<ROP.Unit>> Update(ProductModel model, int Id)
        {
            try
            {
                var spResult = await context.Set<ProductIdSpResult>()
                                            .FromSqlRaw("EXEC sp_UpdateProduct @Id, @Price, @Description, @Category, @UpdatedBy",
                                                new SqlParameter("@Id", Id),
                                                new SqlParameter("@Price", model.Precio),
                                                new SqlParameter("@Description", model.Descripcion),
                                                new SqlParameter("@Category", model.Categoria),
                                                new SqlParameter("@UpdatedBy", model.UserId))
                                            .ToListAsync();

                var newId = spResult.FirstOrDefault()?.Id;
                if (newId == -1)
                {
                    logger.LogInformation("No se encontró el producto con ese Id");
                    return Result.NotFound<ROP.Unit>("No se encontró el producto.");
                }

                if (newId == -2)
                {
                    logger.LogInformation("Ya existe un producto con la descripción");
                    return Result.Failure<ROP.Unit>("Ya existe un producto con esa descripción.");
                }

                if (newId is null || newId == 0)
                {
                    logger.LogInformation("No se pudo modificar el producto.");
                    return Result.Failure<ROP.Unit>("No se pudo modificar el producto.");
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ha ocurrido un error al modificar el producto.");
                return Result.Failure<ROP.Unit>("Ha ocurrido un al modificar el producto.");
            }
        }
    }
}