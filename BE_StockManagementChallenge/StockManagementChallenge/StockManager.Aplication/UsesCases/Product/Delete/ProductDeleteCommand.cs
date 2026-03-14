using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.UsesCases.Product.Delete
{
    public class ProductDeleteCommand(int Id) : IRequest<Result<EmptyResult>>
    {
        public int Id { get; } = Id;
    }
}
