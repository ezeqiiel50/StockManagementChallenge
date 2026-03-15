using MediatR;
using ROP;
using StockManager.Application.DTOs.Category;
using StockManager.Application.DTOs.Constant;

namespace StockManager.Application.UsesCases.Category.GetAll
{
    public class CategoryGetAllHandler() : IRequestHandler<CategoryGetAllQuery, Result<List<CategoryItemResponse>>>
    {
        public async Task<Result<List<CategoryItemResponse>>> Handle(CategoryGetAllQuery request, CancellationToken cancellationToken)
        {
            var lista = new List<CategoryItemResponse>
            {
                new CategoryItemResponse
                {
                    CategoriaCodigo = ItemsValor.PRODDOS,
                    CategoriaDescripcion = ItemsValor.PRODDOS,
                },
                 new CategoryItemResponse
                {
                    CategoriaCodigo = ItemsValor.PRODUNO,
                    CategoriaDescripcion = ItemsValor.PRODUNO,
                }
            };

            return await lista.OrderBy(x => x.CategoriaDescripcion)
                              .ToList()
                              .Success()
                              .Async();
        }
    }
}
