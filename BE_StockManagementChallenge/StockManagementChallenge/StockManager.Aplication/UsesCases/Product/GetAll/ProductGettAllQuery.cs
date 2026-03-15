using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.UsesCases.Product.GetAll
{
    public class ProductGettAllQuery : IRequest<Result<List<ProductItemResponse>>>
    {
    }
}
