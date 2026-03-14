using StockManager.Application.Interfaces;
using System.Security.Claims;

namespace StockManager.Api.Service
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
    {
        private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
        public int Id => int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");

        public string UserName => User?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    }
}
