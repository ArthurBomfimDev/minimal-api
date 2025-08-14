using Minimal.Application.Interfaces.Repositories.Base;
using Minimal.Application.ViewModels.IOs.Vehicle;
using Minimal.Domain.Entities;

namespace Minimal.Application.Interfaces.Repositories;

public interface IVehicleRepository : IBaseRepository<Vehicle>
{
    Task<Vehicle?> GetByCodeAsync(string code);
    List<Vehicle> GetAllWithPagination(InputPaginationVehicle inputPaginationVehicle);
}