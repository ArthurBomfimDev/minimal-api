using Minimal.Application.Interfaces.Repositories;
using Minimal.Application.Interfaces.Services;
using Minimal.Application.Interfaces.UnitOfWork;
using Minimal.Application.Mappers;
using Minimal.Application.ViewModels.IOs.Vehicle;
using Minimal.Domain.Entities;
using Minimal.Domain.Validation.Result;

namespace Minimal.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VehicleService(IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
    {
        _vehicleRepository = vehicleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResult<OutputVehicle>> Create(InputCreateVehicle inputCreateVehicle)
    {
        var existingVehicle = await _vehicleRepository.GetByCodeAsync(inputCreateVehicle.Code);

        if (existingVehicle != null)
            return BaseResult<OutputVehicle>.Failure(Notification.Error("Já existe um veículo com este código."));

        var vehicleResult = Vehicle.Create(inputCreateVehicle.Code, inputCreateVehicle.Model, inputCreateVehicle.Make, inputCreateVehicle.Year, inputCreateVehicle.Description);

        if (!vehicleResult.IsSuccess)
            return BaseResult<OutputVehicle>.Failure(vehicleResult.listNotification);

        await _vehicleRepository.Create(vehicleResult.Value!);
        await _unitOfWork.SaveChangesAsync();

        return BaseResult<OutputVehicle>.Success(vehicleResult.Value.ToOutput()!);
    }

    public async Task<BaseResult<bool>> Update(InputIdentityUpdateVehicle inputIdentityUpdateVehicle)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(inputIdentityUpdateVehicle.Id);
        if (vehicle == null)
        {
            return BaseResult<bool>.Failure(Notification.Error($"Veículo com ID {inputIdentityUpdateVehicle.Id} não encontrado."));
        }

        var validationResult = vehicle.Update(inputIdentityUpdateVehicle.InputUpdate!.Model, inputIdentityUpdateVehicle.InputUpdate!.Make, inputIdentityUpdateVehicle.InputUpdate!.Description);
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        _vehicleRepository.Update(vehicle);
        await _unitOfWork.SaveChangesAsync();

        return BaseResult<bool>.Success(true, Notification.Success($"Veículo com ID {inputIdentityUpdateVehicle.Id} atualizado com sucesso."));
    }

    public async Task<BaseResult<bool>> Delete(InputIdentityDeleteVehicle inputIdentityDeleteVehicle)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(inputIdentityDeleteVehicle.Id);
        if (vehicle == null)
        {
            return BaseResult<bool>.Failure(Notification.Error($"Veículo com ID {inputIdentityDeleteVehicle.Id} não encontrado."));
        }

        var validationResult = vehicle.Delete();
        if (!validationResult.IsSuccess)
        {
            return validationResult;
        }

        await _unitOfWork.SaveChangesAsync();

        return BaseResult<bool>.Success(true, Notification.Success($"Veículo com ID {inputIdentityDeleteVehicle.Id} foi deletado com sucesso."));
    }

    public async Task<List<OutputVehicle?>> GetAllAsync()
    {
        var getAll = await _vehicleRepository.GetAllAsync();
        return getAll.Select(x => x.ToOutput()).ToList() ?? [];
    }

    public async Task<OutputVehicle?> GetByIdAsync(InputIdentityViewVehicle inputIdentityViewVehicle)
    {
        var getById = await _vehicleRepository.GetByIdAsync(inputIdentityViewVehicle.Id);
        return getById.ToOutput();
    }

    public List<OutputVehicle> GetAllWithPagination(InputPaginationVehicle inputPaginationVehicle)
    {
        var getAllWithPagination = _vehicleRepository.GetAllWithPagination(inputPaginationVehicle);
        return getAllWithPagination.Select(x => x.ToOutput()!).ToList() ?? [];
    }
}