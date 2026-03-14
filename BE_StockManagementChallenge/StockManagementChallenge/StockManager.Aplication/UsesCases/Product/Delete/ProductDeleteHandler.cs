using MediatR;
using ROP;

namespace StockManager.Application.UsesCases.Product.Delete
{
    public class ProductDeleteHandler() : IRequestHandler<ProductDeleteCommand, Result<ROP.Unit>>
    {
        public Task<Result<ROP.Unit>> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
