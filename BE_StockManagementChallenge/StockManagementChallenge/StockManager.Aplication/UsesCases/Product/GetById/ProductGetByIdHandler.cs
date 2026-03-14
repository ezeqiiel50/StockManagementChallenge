using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Response;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Product.GetById
{
    public class ProductGetByIdHandler(IProductoRepository productoRepository) : IRequestHandler<ProductGetByIdQuery, Result<ProductoResponse>>
    {
        public async Task<Result<ProductoResponse>> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await productoRepository.GetById(request.Id);
            return result;
        }
    }
}
