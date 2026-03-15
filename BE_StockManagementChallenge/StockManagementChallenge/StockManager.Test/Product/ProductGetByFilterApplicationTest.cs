using FluentAssertions;
using Moq;
using ROP;
using StockManager.Application.DTOs.Constant;
using StockManager.Application.Interfaces;
using StockManager.Application.UsesCases.Product.GetAll;
using StockManager.Application.UsesCases.Product.GetByFilter;

namespace StockManager.Test.Product
{
    public class ProductGetByFilterApplicationTest
    {
        private readonly Mock<IProductoRepository> _mockProductoRepository;

        public ProductGetByFilterApplicationTest()
        {
            _mockProductoRepository = new Mock<IProductoRepository>();
        }

        [Fact]
        public async Task GetByFilter_ShouldReturnSuccess()
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
                        Precio = 15
                    },
                    new Application.DTOs.Product.Response.ProductItemResponse
                    {
                        Id = 12,
                        Categoria = ItemsValor.PRODUNO,
                        Descripcion = "PRODUCTO B",
                        FechaCarga = DateTime.Now.ToString(),
                        Precio = 5
                    },
                    new Application.DTOs.Product.Response.ProductItemResponse
                    {
                        Id = 13,
                        Categoria = ItemsValor.PRODUNO,
                        Descripcion = "PRODUCTO C",
                        FechaCarga = DateTime.Now.ToString(),
                        Precio = 15
                    },
                });

            var query = new ProductGetByFilterQuery(30);
            var handler = new ProductGetByFilterHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
            result.Value.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetByFilter_ShouldReturnFailed()
        {
            _mockProductoRepository.Setup(x => x.GetAll())
                 .ReturnsAsync(Result.Failure<List<Application.DTOs.Product.Response.ProductItemResponse>>("Error Test"));

            var query = new ProductGetByFilterQuery(30);
            var handler = new ProductGetByFilterHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task GetByFilter_ShouldReturnFailedNotCombinaciones()
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
                        Precio = 15
                    },
                    new Application.DTOs.Product.Response.ProductItemResponse
                    {
                        Id = 12,
                        Categoria = ItemsValor.PRODUNO,
                        Descripcion = "PRODUCTO B",
                        FechaCarga = DateTime.Now.ToString(),
                        Precio = 5
                    },
                    new Application.DTOs.Product.Response.ProductItemResponse
                    {
                        Id = 13,
                        Categoria = ItemsValor.PRODUNO,
                        Descripcion = "PRODUCTO C",
                        FechaCarga = DateTime.Now.ToString(),
                        Precio = 15
                    },
               });

            var query = new ProductGetByFilterQuery(10);
            var handler = new ProductGetByFilterHandler(_mockProductoRepository.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }
    }
}
