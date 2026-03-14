using MediatR;
using ROP;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Product.Delete
{
    public class ProductDeleteHandler(IProductoRepository productoRepository) : IRequestHandler<ProductDeleteCommand, Result<ROP.Unit>>
    {
        public async Task<Result<ROP.Unit>> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await productoRepository.GetById(request.Id)
                                .Bind(_ => productoRepository.Delete(request.Id));
            return result;
        }
    }
}
