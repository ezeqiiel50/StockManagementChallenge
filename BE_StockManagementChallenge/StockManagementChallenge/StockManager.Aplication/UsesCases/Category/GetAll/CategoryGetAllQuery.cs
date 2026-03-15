using MediatR;
using ROP;
using StockManager.Application.DTOs.Category;

namespace StockManager.Application.UsesCases.Category.GetAll
{
    public class CategoryGetAllQuery() : IRequest<Result<List<CategoryItemResponse>>>
    {
    }
}
