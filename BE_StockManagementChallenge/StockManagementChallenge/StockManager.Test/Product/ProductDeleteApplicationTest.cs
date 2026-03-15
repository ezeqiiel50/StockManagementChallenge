using FluentAssertions;
using Moq;
using ROP;
using StockManager.Application.Interfaces;
using StockManager.Application.UsesCases.Product.Delete;

namespace StockManager.Test.Product
{
    public class ProductDeleteApplicationTest
    {
        private readonly Mock<IProductoRepository> _mockProductoRepository;

        public ProductDeleteApplicationTest()
        {
            _mockProductoRepository = new Mock<IProductoRepository>();
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccess()
        {
            _mockProductoRepository.Setup(x => x.Delete(It.IsAny<int>()))
                .ReturnsAsync(Result.Success());

            var cmmd = new ProductDeleteCommand(50);
            var handler = new ProductDeleteHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(cmmd, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Delete_ShouldReturnFailed()
        {
            _mockProductoRepository.Setup(x => x.Delete(It.IsAny<int>()))
                 .ReturnsAsync(Result.Failure<ROP.Unit>("Error Test"));

            var cmmd = new ProductDeleteCommand(50);
            var handler = new ProductDeleteHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(cmmd, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }
    }
}
