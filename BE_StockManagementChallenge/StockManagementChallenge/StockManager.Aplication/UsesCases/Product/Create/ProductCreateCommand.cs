using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Request;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.UsesCases.Product.Create
{
    public class ProductCreateCommand(ProductCreateRequest request) : IRequest<Result<ProductCreateResponse>>
    {
        public string Descripcion { get; } = request.Descripcion;
        public int Precio { get; } = request.Precio;
        public string Categoria { get; } = request.Categoria;
    }
}
