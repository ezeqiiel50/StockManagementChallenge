using MediatR;
using ROP;
using StockManager.Application.DTOs.Constant;
using StockManager.Application.DTOs.Product.Response;
using StockManager.Application.Interfaces;

namespace StockManager.Application.UsesCases.Product.GetByFilter
{
    public class ProductGetByFilterHandler(IProductoRepository productoRepository) : IRequestHandler<ProductGetByFilterQuery, Result<List<ProductItemResponse>>>
    {
        public async Task<Result<List<ProductItemResponse>>> Handle(ProductGetByFilterQuery request, CancellationToken cancellationToken)
        {
            var result = await productoRepository.GetAll()
                                .Bind(x => ProcesarLista(x, request.Monto));

            return result;
        }

        private async Task<Result<List<ProductItemResponse>>> ProcesarLista(List<ProductItemResponse> listProducts, int monto)
        {
            var productsCategoriaUno = listProducts.Where(x => x.Categoria == ItemsValor.PRODUNO)
                                                   .OrderByDescending(x => x.Precio)
                                                   .ToList();

            var productsCategoriaDos = listProducts.Where(x => x.Categoria == ItemsValor.PRODDOS)
                                                   .OrderByDescending(x => x.Precio)
                                                   .ToList();

            List<ProductItemResponse> combinaciones = [];
            var mejorTotal = 0;
            var encontrado = false;

            foreach (var prodCatUno in productsCategoriaUno)
            {
                foreach (var prodCatDos in productsCategoriaDos)
                {
                    var total = prodCatUno.Precio + prodCatDos.Precio;
                    if (total <= monto && total > mejorTotal) 
                    {
                        mejorTotal = total;
                        combinaciones = [prodCatUno, prodCatDos];
                    }
                    if (total == monto) 
                    {
                        encontrado = true;
                        break;
                    }
                }
                if (encontrado) break;
            }
            var result = combinaciones.Count > 0
                       ? combinaciones
                       : Result.NotFound<List<ProductItemResponse>>("No pudimos encontrar productos que se ajusten a tu presupuesto.");

            return await Task.FromResult(result);
        }
    }
}
