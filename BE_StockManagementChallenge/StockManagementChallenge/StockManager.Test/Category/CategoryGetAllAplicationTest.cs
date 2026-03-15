using FluentAssertions;
using StockManager.Application.UsesCases.Category.GetAll;

namespace StockManager.Test.Category
{
    public class CategoryGetAllAplicationTest
    {

        [Fact]
        public async Task Create_ShouldReturnSuccess()
        {
            var query = new CategoryGetAllQuery();
            var handler = new CategoryGetAllHandler();
            var result = await handler.Handle(query, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
        }
    }
}
