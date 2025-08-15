using FluentAssertions;
using Minimal.Application.Interfaces.Repositories;
using Minimal.Application.Interfaces.Services;
using Minimal.Application.Interfaces.UnitOfWork;
using Minimal.Application.Services;
using Minimal.Application.ViewModels.IOs.Authenticate;
using Minimal.Domain.Entities;
using Minimal.Domain.Enuns.Registration;
using Moq;
using Xunit;

namespace Test.Domain.Entities;

public class AdministradorServiceTest
{
    private readonly Mock<IAdministratorRepository> _adminRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPasswordEncryptorService> _passwordEncryptorServiceMock;
    private readonly Mock<IAuthenticationService> _authServiceMock;
    private readonly AdministratorService _adminService;

    public AdministradorServiceTest()
    {
        _adminRepositoryMock = new Mock<IAdministratorRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _passwordEncryptorServiceMock = new Mock<IPasswordEncryptorService>();
        _authServiceMock = new Mock<IAuthenticationService>();

        _adminService = new AdministratorService(
            _unitOfWorkMock.Object,
            _adminRepositoryMock.Object,
            _passwordEncryptorServiceMock.Object,
            _authServiceMock.Object);
    }

    [Fact]
    public async Task Login_Com_Credenciais_Validas_Deve_Retornar_Resultado_De_Login()
    {
        var input = new InputLoginAuthenticate("admin@test.com", "password123");

        var passwordHash = _passwordEncryptorServiceMock.Object.HashPassword(input.Password);
        var admin = Administrator.Create(input.Email, input.Password, EnumRole.Administrador, passwordHash).Value;

        _adminRepositoryMock.Setup(r => r.GetByEmail(input.Email))
            .ReturnsAsync(admin);

        _passwordEncryptorServiceMock.Setup(h => h.VerifyPassword(input.Password, passwordHash)).Returns(true);
        _authServiceMock.Setup(a => a.GenerateJwtToken(admin)).Returns("jwt_token");

        var result = await _adminService.Login(input);

        result.Should().NotBeNull();
        result.Token.Should().Be("jwt_token");
        result.Administrator.Email.Should().Be(input.Email);
    }

    [Fact]
    public async Task Login_Com_Credenciais_Invalidas_Deve_Lancar_Excecao_De_Acesso_Nao_Autorizado()
    {
        var input = new InputLoginAuthenticate("admin@test.com", "wrong_password");

        var passwordHash = _passwordEncryptorServiceMock.Object.HashPassword("rightPassword");
        var admin = Administrator.Create(input.Email, "rightPassword", EnumRole.Administrador, passwordHash).Value;

        _adminRepositoryMock.Setup(r => r.GetByEmail(input.Email))
            .ReturnsAsync(admin);

        _passwordEncryptorServiceMock.Setup(h => h.VerifyPassword(input.Password, passwordHash)).Returns(false);

        Func<Task> act = async () => await _adminService.Login(input);

        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}