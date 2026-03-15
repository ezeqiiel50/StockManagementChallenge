using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Response;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Product.GetAll
{
    public class ProductGettAllHandler(IProductoRepository productoRepository) : IRequestHandler<ProductGettAllQuery, Result<List<ProductItemResponse>>>
    {
        public async Task<Result<List<ProductItemResponse>>> Handle(ProductGettAllQuery request, CancellationToken cancellationToken)
        {
            var result = await productoRepository.GetAll();
            return result;
        }
    }
}
