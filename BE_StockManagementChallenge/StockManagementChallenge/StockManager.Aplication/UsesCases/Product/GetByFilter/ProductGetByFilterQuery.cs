using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.UsesCases.Product.GetByFilter
{
    public class ProductGetByFilterQuery(int Monto) : IRequest<Result<List<ProductItemResponse>>>
    {
        public int Monto { get; } = Monto;
    }
}
