using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Model;
using StockManager.Application.DTOs.Product.Response;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Product.Update
{
    public class ProductUpdateHandler(IProductoRepository productoRepository, ICurrentUser currentUser) : IRequestHandler<ProductUpdateCommand, Result<EmptyResult>>
    {
        public async Task<Result<EmptyResult>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var req = new ProductModel
            {
                Categoria = request.Categoria,
                Descripcion = request.Descripcion,
                Precio = request.Precio,
                UserId = currentUser.Id
            };

            var result = await productoRepository.Update(req, request.Id)
                                .Map(x => new EmptyResult());
            return result;
        }
    }
}
