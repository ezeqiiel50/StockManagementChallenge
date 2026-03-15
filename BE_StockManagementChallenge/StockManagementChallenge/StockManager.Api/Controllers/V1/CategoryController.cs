using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;

namespace StockManager.Api.Controllers.V1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new StockManager.Application.UsesCases.Category.GetAll.CategoryGetAllQuery();
            var product = await _mediator.Send(query);
            return product.ToActionResult();
        }
    }
}
