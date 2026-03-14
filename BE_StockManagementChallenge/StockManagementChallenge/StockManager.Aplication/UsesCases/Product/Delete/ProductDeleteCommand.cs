using MediatR;
using ROP;

namespace StockManager.Application.UsesCases.Product.Delete
{
    public class ProductDeleteCommand(int Id) : IRequest<Result<ROP.Unit>>
    {
        public int Id { get; } = Id;
    }
}
