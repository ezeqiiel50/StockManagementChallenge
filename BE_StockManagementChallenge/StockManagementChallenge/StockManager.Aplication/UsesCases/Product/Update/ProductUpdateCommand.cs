using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Request;
using StockManager.Application.DTOs.Product.Response;

namespace StockManager.Application.UsesCases.Product.Update
{
    public class ProductUpdateCommand(ProductRequest request, int Id) : IRequest<Result<EmptyResult>>
    {
        public int Id { get; } = Id;
        public string Descripcion { get; } = request.Descripcion;
        public int Precio { get; } = request.Precio;
        public string Categoria { get; } = request.Categoria;
    }
}
