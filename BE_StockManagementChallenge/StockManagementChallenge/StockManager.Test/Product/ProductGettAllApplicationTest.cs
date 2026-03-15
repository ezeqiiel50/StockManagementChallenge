using FluentAssertions;
using Moq;
using ROP;
using StockManager.Application.DTOs.Constant;
using StockManager.Application.Interfaces;
using StockManager.Application.UsesCases.Product.GetAll;

namespace StockManager.Test.Product
{
    public class ProductGettAllApplicationTest
    {
        private readonly Mock<IProductoRepository> _mockProductoRepository;

        public ProductGettAllApplicationTest()
        {
            _mockProductoRepository = new Mock<IProductoRepository>();
        }

        [Fact]
        public async Task GetAll_ShouldReturnSuccess()
        {
            _mockProductoRepository.Setup(x => x.GetAll())
                .ReturnsAsync(new List<Application.DTOs.Product.Response.ProductItemResponse>
                {
                    new Application.DTOs.Product.Response.ProductItemResponse
                    {
                        Id = 1,
                        Categoria = ItemsValor.PRODDOS,
                        Descripcion = "PRODUCTO A",
                        FechaCarga = DateTime.Now.ToString(),
                        Precio = 10
                    }
                });

            var query = new ProductGettAllQuery();
            var handler = new ProductGettAllHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAll_ShouldReturnFailed()
        {
            _mockProductoRepository.Setup(x => x.GetAll())
                 .ReturnsAsync(Result.Failure<List<Application.DTOs.Product.Response.ProductItemResponse>>("Error Test"));

            var query = new ProductGettAllQuery();
            var handler = new ProductGettAllHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }
    }
}
