using Business.Interfaces;
using Business.Model.Users;
using Business.Services;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Tests.Unit
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IConfiguration _configurationMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            var configValues = new Dictionary<string, string>
            {
                { "JwtSettings:Key", "supersecretkey12345" },
                { "JwtSettings:Issuer", "CentralDeControle" },
                { "JwtSettings:Audience", "CentralDeControleApp" },
                { "JwtSettings:ExpiresInMinutes", "60" }
            };
            _configurationMock = new ConfigurationBuilder().AddInMemoryCollection(configValues).Build();

            _userService = new UserService(_userRepositoryMock.Object, _configurationMock);
        }

        [Fact]
        public void Authenticate_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var password = "123321";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Id = 4,
                Name = "testeamarildo",
                Email = "testeamarildo@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                IsGestor = true
            };
            _userRepositoryMock.Setup(repo => repo.FindUserByEmail("testeamarildo@gmail.com")).Returns(user);

            // Act
            var result = _userService.Authenticate("testeamarildo@gmail.com", password);

            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public void Authenticate_ShouldReturnError_WhenUserNotFound()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.FindUserByEmail("nonexistent@example.com")).Returns((User)null);

            // Act
            var result = _userService.Authenticate("nonexistent@example.com", "password");

            // Assert
            Assert.Equal(401, result.StatusCode);
            Assert.Null(result.Data);
            Assert.Equal("E-mail ou senha inválidos.", result.ErrorMessage);
        }
    }
}
