using FluentAssertions;
using Minimal.Application.Interfaces.Services;
using Minimal.Domain.Entities;
using Minimal.Domain.Enuns.Registration;
using Moq;
using Xunit;

namespace Test.Domain.Entities;

public class AdministradorTest
{
    private readonly Mock<IPasswordEncryptorService> _passwordEncryptorServiceMock;

    public AdministradorTest()
    {
        _passwordEncryptorServiceMock = new Mock<IPasswordEncryptorService>();
        _passwordEncryptorServiceMock.Setup(h => h.HashPassword(It.IsAny<string>())).Returns("encryptorPassword");
    }

    [Fact]
    public void Create_WithValidData_ShouldReturnSuccess()
    {
        var email = "test@test.com";
        var password = "password123";
        var role = EnumRole.Administrador;

        var result = Administrator.Create(email, password, role, _passwordEncryptorServiceMock.Object.HashPassword("password123"));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Email.Should().Be(email);
        result.Value.Password.Should().Be("encryptorPassword");
        result.Value.Role.Should().Be(role.ToString());
    }

    [Theory]
    [InlineData("invalid-email", "password123", 1)] // Email inválido
    [InlineData("test@test.com", "123", 1)]       // Senha curta
    [InlineData("test@test.com", "password123", 3)] // Role inválida
    public void Create_WithInvalidData_ShouldReturnFailure(string email, string password, int role)
    {
        var result = Administrator.Create(email, password, (EnumRole)role, _passwordEncryptorServiceMock.Object.HashPassword("password"));

        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.listNotification.Should().NotBeEmpty();
    }
}