using MediatR;
using ROP;
using StockManager.Application.DTOs.Product;

namespace StockManager.Application.UsesCases.Product.GetById
{
    public class ProductGetByIdQuery(int Id) : IRequest<Result<ProductoResponse>>
    {
        public int Id { get; } = Id;
    }
}
