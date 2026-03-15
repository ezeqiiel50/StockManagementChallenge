using FluentAssertions;
using Moq;
using ROP;
using StockManager.Application.DTOs.Constant;
using StockManager.Application.Interfaces;
using StockManager.Application.UsesCases.Product.GetById;

namespace StockManager.Test.Product
{
    public class ProductGetByIdApplicationTest
    {
        private readonly Mock<IProductoRepository> _mockProductoRepository;

        public ProductGetByIdApplicationTest()
        {
            _mockProductoRepository = new Mock<IProductoRepository>();
        }

        [Fact]
        public async Task GetById_ShouldReturnSuccess()
        {
            _mockProductoRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync(new Application.DTOs.Product.Response.ProductItemResponse
                    {
                        Id = 1,
                        Categoria = ItemsValor.PRODDOS,
                        Descripcion = "PRODUCTO A",
                        FechaCarga = DateTime.Now.ToString(),
                        Precio = 10
                    });

            var query = new ProductGetByIdQuery(1);
            var handler = new ProductGetByIdHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetById_ShouldReturnFailed()
        {
            _mockProductoRepository.Setup(x => x.GetById(It.IsAny<int>()))
                  .ReturnsAsync(Result.Failure<Application.DTOs.Product.Response.ProductItemResponse>("Error Test"));

            var query = new ProductGetByIdQuery(1);
            var handler = new ProductGetByIdHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }
    }
}
