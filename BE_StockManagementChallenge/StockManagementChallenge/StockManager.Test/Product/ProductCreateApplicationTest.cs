using FluentAssertions;
using Moq;
using ROP;
using StockManager.Application.DTOs.Constant;
using StockManager.Application.DTOs.Product.Model;
using StockManager.Application.DTOs.Product.Request;
using StockManager.Application.Interfaces;
using StockManager.Application.UsesCases.Product.Create;

namespace StockManager.Test.Product
{
    public class ProductCreateApplicationTest
    {
        private readonly Mock<IProductoRepository> _mockProductoRepository;
        private readonly Mock<ICurrentUser> _mockCurrentUser;

        public ProductCreateApplicationTest()
        {
            _mockCurrentUser = new Mock<ICurrentUser>();
            _mockProductoRepository = new Mock<IProductoRepository>();
        }

        [Fact]
        public async Task Create_ShouldReturnSuccess()
        {
            _mockProductoRepository.Setup(x => x.Create(It.IsAny<ProductModel>()))
                .ReturnsAsync(10);

            _mockCurrentUser.Setup(x => x.Id)
                .Returns(1);

            var cmmd = new ProductCreateCommand(new ProductRequest
            {
                Precio = 10,
                Categoria = ItemsValor.PRODDOS,
                Descripcion = "Producto B"
            });

            var handler = new ProductCreateHandler(_mockProductoRepository.Object, _mockCurrentUser.Object);
            var result = await handler.Handle(cmmd, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_ShouldReturnFailed()
        {
            _mockProductoRepository.Setup(x => x.Create(It.IsAny<ProductModel>()))
                .ReturnsAsync(Result.Failure<int>("Error Test"));

            _mockCurrentUser.Setup(x => x.Id)
                .Returns(1);

            var cmmd = new ProductCreateCommand(new ProductRequest
            {
                Precio = 10,
                Categoria = ItemsValor.PRODDOS,
                Descripcion = "Producto B"
            });

            var handler = new ProductCreateHandler(_mockProductoRepository.Object, _mockCurrentUser.Object);
            var result = await handler.Handle(cmmd, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }

    }
}
