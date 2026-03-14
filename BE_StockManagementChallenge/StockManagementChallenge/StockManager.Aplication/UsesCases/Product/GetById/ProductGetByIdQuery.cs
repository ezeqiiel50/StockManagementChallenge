using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.UsesCases.Product.GetById
{
    public class ProductGetByIdQuery(int Id) : IRequest<Result<ProductItemResponse>>
    {
        public int Id { get; } = Id;
    }
}
