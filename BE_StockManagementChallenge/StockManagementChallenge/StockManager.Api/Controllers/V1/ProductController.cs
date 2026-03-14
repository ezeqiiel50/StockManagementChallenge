using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;
using StockManager.Application.DTOs.Product.Request;

namespace StockManagerApi.Controllers.V1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new StockManager.Application.UsesCases.Product.GetById.ProductGetByIdQuery(id);
            var product = await _mediator.Send(query);
            return product.ToActionResult();
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductRequest request)
        {
            var cmmd = new StockManager.Application.UsesCases.Product.Create.ProductCreateCommand(request);
            var product = await _mediator.Send(cmmd);
            return product.ToActionResult();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductRequest request)
        {
            var cmmd = new StockManager.Application.UsesCases.Product.Update.ProductUpdateCommand(request, id);
            var product = await _mediator.Send(cmmd);
            return product.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cmmd = new StockManager.Application.UsesCases.Product.Delete.ProductDeleteCommand(id);
            var product = await _mediator.Send(cmmd);
            return product.ToActionResult();
        }
    }
}
