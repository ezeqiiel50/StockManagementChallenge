using MediatR;
using Microsoft.AspNetCore.Mvc;
using ROP.APIExtensions;
using StockManager.Application.DTOs.Login;

namespace StockManager.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginRequest request)
        {
            var query = new StockManager.Application.UsesCases.Login.LoginUserQuery(request);
            var product = await _mediator.Send(query);
            return product.ToActionResult();
        }
    }
}
