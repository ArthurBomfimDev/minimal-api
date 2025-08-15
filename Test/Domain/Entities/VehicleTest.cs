using FluentAssertions;
using Minimal.Domain.Entities;
using Xunit;

namespace Test.Domain.Entities;

public class VehicleTest
{
    [Fact]
    public void Criar_ComDadosValidos_DeveRetornarSucesso()
    {
        var code = "ABC-1234";
        var model = "Corolla";
        var make = "Toyota";
        var year = 2025;
        var description = "Carro novo";

        var result = Vehicle.Create(code, model, make, year, description);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Code.Should().Be(code);
        result.listNotification.Should().BeEmpty();
    }

    [Theory]
    [InlineData("", "Model", "Make", 2025)] 
    [InlineData("ABC-1234", "", "Make", 2025)] 
    [InlineData("ABC-1234", "Model", "Make", 1940)] 
    public void Criar_ComDadosInvalidos_DeveRetornarFalha(string code, string model, string make, int year)
    {
        var result = Vehicle.Create(code, model, make, year, "Descrição");

        result.IsSuccess.Should().BeFalse();
        result.Value.Should().BeNull();
        result.listNotification.Should().NotBeEmpty();
    }

    [Fact]
    public void Atualizar_ComDadosValidos_DeveAtualizarPropriedades()
    {
        var vehicle = Vehicle.Create("ABC-1234", "Corolla", "Toyota", 2025, "Original").Value;
        var newModel = "Hilux";
        var newMake = "Toyota";

        var validationResult = vehicle.Update(newModel, newMake, "Atualizado");

        validationResult.IsSuccess.Should().BeTrue();
        vehicle.Model.Should().Be(newModel);
        vehicle.Make.Should().Be(newMake);
        vehicle.ChangedDate.Should().NotBeNull();
    }
}