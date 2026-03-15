using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ROP;
using StockManager.Application.DTOs.Constant;
using StockManager.Application.DTOs.Login;
using StockManager.Application.DTOs.Product.Request;
using StockManager.Application.Interfaces;
using StockManager.Application.UsesCases.Login;
using StockManager.Application.UsesCases.Product.Create;

namespace StockManager.Test.Login
{
    public class LoginUserAplicationTest
    {
        private readonly Mock<IAuthRepository> _mockAuthRepository;
        private readonly Mock<ILogger<LoginUserHandler>> _mockLogger;
        private readonly Mock<IJwtService> _mockJwtService;

        public LoginUserAplicationTest()
        {
            _mockAuthRepository = new Mock<IAuthRepository>();
            _mockJwtService = new Mock<IJwtService>();
            _mockLogger = new Mock<ILogger<LoginUserHandler>>();
        }

        [Fact]
        public async Task Login_ShouldReturnSuccess()
        {
            _mockAuthRepository.Setup(x => x.GetUserByUserName(It.IsAny<string>()))
                .ReturnsAsync(new UserData
                {
                    Id = 1,
                    PasswordHash = "$2a$11$T8llk7dtPFNb7c44ZZYWmOJyWsTBvqdzobUnfLFuzt1Wf9zbRz9oq",
                    UserName = "Tester",
                });

            _mockJwtService.Setup(x => x.GenerateToken(It.IsAny<UserData>()))
                .ReturnsAsync("mdfjndjfnnskljfsg41fg5j4df1j5h1cf4h4hj4gh54j2g45dff2f");

            var query = new LoginUserQuery(new LoginRequest
            {
                User = "tester",
                Password = "tester123"
            });

            var handler = new LoginUserHandler(_mockLogger.Object, _mockAuthRepository.Object, _mockJwtService.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnFailed()
        {
            _mockAuthRepository.Setup(x => x.GetUserByUserName(It.IsAny<string>()))
                .ReturnsAsync(new UserData
                {
                    Id = 1,
                    PasswordHash = "$s$451324df5df1s1",
                    UserName = "Tester",
                });

            _mockJwtService.Setup(x => x.GenerateToken(It.IsAny<UserData>()))
                .ReturnsAsync(Result.Failure<string>("Error test"));

            var query = new LoginUserQuery(new LoginRequest
            {
                User = "tester",
                Password = "tester123"
            });

            var handler = new LoginUserHandler(_mockLogger.Object, _mockAuthRepository.Object, _mockJwtService.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }

        [Fact]
        public async Task Login_ShouldReturnFailed_WithPasswordError()
        {
            _mockAuthRepository.Setup(x => x.GetUserByUserName(It.IsAny<string>()))
                .ReturnsAsync(new UserData
                {
                    Id = 1,
                    PasswordHash = "$2a$11$T8llk7dtPFNb7c44ZZYWmOJyWsTBvqdzobUnfLFuzt1Wf9zbRz9oq",
                    UserName = "Tester",
                });

            _mockJwtService.Setup(x => x.GenerateToken(It.IsAny<UserData>()))
                .ReturnsAsync("mdfjndjfnnskljfsg41fg5j4df1j5h1cf4h4hj4gh54j2g45dff2f");

            var query = new LoginUserQuery(new LoginRequest
            {
                User = "tester",
                Password = "tester12365"
            });

            var handler = new LoginUserHandler(_mockLogger.Object, _mockAuthRepository.Object, _mockJwtService.Object);
            var result = await handler.Handle(query, default);

            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeNullOrEmpty();
            result.Value.Should().BeNull();
        }
    }
}
