using FluentAssertions;
using Minimal.Application.Interfaces.Repositories;
using Minimal.Application.Interfaces.UnitOfWork;
using Minimal.Application.Services;
using Minimal.Application.ViewModels.IOs.Vehicle;
using Minimal.Domain.Entities;
using Moq;
using Xunit;

namespace Test.Domain.Services;

public class VehicleServiceTest
{
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly VehicleService _vehicleService;

    public VehicleServiceTest()
    {
        _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _vehicleService = new VehicleService(
            _vehicleRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Create_Com_Dados_Validos_Deve_Chamar_Repositorio_E_UnitOfWork()
    {
        var input = new InputCreateVehicle("FUSION2020", "Fusion", "Ford", 2020, "Carro Impecavél");

        _vehicleRepositoryMock
            .Setup(r => r.GetByCodeAsync(input.Code))
            .ReturnsAsync((Vehicle)null);

        var vehicleId = await _vehicleService.Create(input);

        vehicleId.Should().NotBeNull();
        _vehicleRepositoryMock.Verify(r => r.Create(It.IsAny<Vehicle>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Create_Quando_Codigo_Ja_Existe_Deve_Retornar_BadRequest()
    {
        var input = new InputCreateVehicle("EXIST-CODE", "Fusion", "Ford", 2020, "Descrição");
        var existingVehicle = Vehicle.Create(input.Code, "outro", "outro", 2019, null).Value;

        _vehicleRepositoryMock
            .Setup(r => r.GetByCodeAsync(input.Code))
            .ReturnsAsync(existingVehicle);

        var result = await _vehicleService.Create(input);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.listNotification.Should().NotBeNull().And.HaveCount(1);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
    }
}