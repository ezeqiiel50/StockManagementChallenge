using FluentAssertions;
using Moq;
using ROP;
using StockManager.Application.DTOs.Constant;
using StockManager.Application.DTOs.Product.Model;
using StockManager.Application.Interfaces;
using StockManager.Application.UsesCases.Product.Update;

namespace StockManager.Test.Product
{
    public class ProductUpdateApplicationTest
    {
        private readonly Mock<IProductoRepository> _mockProductoRepository;
        private readonly Mock<ICurrentUser> _mockCurrentUser;

        public ProductUpdateApplicationTest()
        {
            _mockProductoRepository = new Mock<IProductoRepository>();
            _mockCurrentUser = new Mock<ICurrentUser>();
        }

        [Fact]
        public async Task Update_ShouldReturnSuccess()
        {
            _mockCurrentUser.Setup(x => x.Id)
                .Returns(1);

            _mockProductoRepository.Setup(x => x.Update(It.IsAny<ProductModel>(), It.IsAny<int>()))
                .ReturnsAsync(Result.Success());

            var cmmd = new ProductUpdateCommand(new Application.DTOs.Product.Request.ProductRequest
            {
                Precio = 10,
                Categoria = ItemsValor.PRODDOS,
                Descripcion = "PROD 003"
            }, 10);

            var handler = new ProductUpdateHandler(_mockProductoRepository.Object, _mockCurrentUser.Object);
            var result = await handler.Handle(cmmd, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Update_ShouldReturnFailed()
        {
            _mockCurrentUser.Setup(x => x.Id)
               .Returns(1);

            _mockProductoRepository.Setup(x => x.Update(It.IsAny<ProductModel>(), It.IsAny<int>()))
                .ReturnsAsync(Result.Failure<ROP.Unit>("Error test"));

            var cmmd = new ProductUpdateCommand(new Application.DTOs.Product.Request.ProductRequest
            {
                Precio = 10,
                Categoria = ItemsValor.PRODDOS,
                Descripcion = "PROD 003"
            }, 10);

            var handler = new ProductUpdateHandler(_mockProductoRepository.Object, _mockCurrentUser.Object);
            var result = await handler.Handle(cmmd, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }
    }
}
