using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Response;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Product.Delete
{
    public class ProductDeleteHandler(IProductoRepository productoRepository) : IRequestHandler<ProductDeleteCommand, Result<EmptyResult>>
    {
        public async Task<Result<EmptyResult>> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await productoRepository.Delete(request.Id)
                                    .Map(_ => new EmptyResult());
            return result;
        }
    }
}
