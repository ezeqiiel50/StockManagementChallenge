using ROP;
using StockManager.Application.DTOs.Product.Model;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.Interfaces
{
    public interface IProductoRepository
    {
        Task<Result<int>> Create(ProductModel model);
        Task<Result<Unit>> Delete(int Id);
        Task<Result<List<ProductItemResponse>>> GetAll();
        Task<Result<ProductItemResponse>> GetById(int Id);
        Task<Result<Unit>> Update(ProductModel model, int Id);
    }
}
