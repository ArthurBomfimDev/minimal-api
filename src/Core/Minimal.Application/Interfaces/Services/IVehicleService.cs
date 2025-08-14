using Minimal.Application.ViewModels.IOs.Vehicle;
using Minimal.Domain.Validation.Result;

namespace Minimal.Application.Interfaces.Services;

public interface IVehicleService
{
    Task<OutputVehicle?> GetByIdAsync(InputIdentityViewVehicle inputIdentityViewVehicle);
    Task<List<OutputVehicle?>> GetAllAsync();
    List<OutputVehicle> GetAllWithPagination(InputPaginationVehicle inputPaginationVehicle);
    Task<BaseResult<OutputVehicle>> Create(InputCreateVehicle inputCreateVehicle);
    Task<BaseResult<bool>> Update(InputIdentityUpdateVehicle inputIdentityUpdateVehicle);
    Task<BaseResult<bool>> Delete(InputIdentityDeleteVehicle inputIdentityDeleteVehicle);
}