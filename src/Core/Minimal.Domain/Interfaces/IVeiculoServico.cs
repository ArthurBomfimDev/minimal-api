using Minimal.Domain.Entities;

namespace Minimal.Domain.Interfaces;

public interface IVehicleServico
{
    List<Vehicle> Todos(int? pagina = 1, string? nome = null, string? marca = null);
    Vehicle? BuscaPorId(int id);
    void Incluir(Vehicle vehicle);
    void Atualizar(Vehicle vehicle);
    void Apagar(Vehicle vehicle);
}