using ROP;
using StockManager.Application.DTOs.Product.Model;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.Interfaces
{
    public interface IProductoRepository
    {
        Task<Result<int>> Create(ProductCreateModel model);
        Task<Result<Unit>> Delete(int Id);
        Task<Result<ProductoResponse>> GetById(int Id);
    }
}
