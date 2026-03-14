using ROP;
using StockManager.Application.DTOs.Product;

namespace StockManager.Application.Interfaces
{
    public interface IProductoRepository
    {
        Task<Result<Unit>> Delete(int Id);
        Task<Result<ProductoResponse>> GetById(int Id);
    }
}
