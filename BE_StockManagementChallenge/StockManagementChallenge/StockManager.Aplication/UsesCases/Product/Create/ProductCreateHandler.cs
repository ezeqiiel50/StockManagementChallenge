using MediatR;
using ROP;
using StockManager.Application.DTOs.Product.Model;
using StockManager.Application.DTOs.Product.Response;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Product.Create
{
    public class ProductCreateHandler(IProductoRepository productoRepository, ICurrentUser currentUser) : IRequestHandler<ProductCreateCommand, Result<ProductResponse>>
    {
        public async Task<Result<ProductResponse>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var req = new ProductModel
            {
                Categoria = request.Categoria,
                Descripcion = request.Descripcion.ToUpper(),
                Precio = request.Precio,
                UserId = currentUser.Id,
            };

            var result = await productoRepository.Create(req)
                                .Map(id => new ProductResponse { Id = id});
            return result;
        }
    }
}
