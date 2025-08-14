using Minimal.Application.ViewModels.IOs.Vehicle;
using Minimal.Domain.Entities;
using Minimal.DTOs;

namespace Minimal.Application.Mappers;

public static class VehicleMapper
{
    public static VehicleDTO? ToDTO(this Vehicle? vehicle)
    {
        return vehicle != null ? new VehicleDTO(
            vehicle.Id,
            vehicle.CreatedDate,
            vehicle.ChangedDate,
            vehicle.IsActive,
            vehicle.Code,
            vehicle.Model,
            vehicle.Make,
            vehicle.Year,
            vehicle.Description) : null;
    }

    public static OutputVehicle? ToOutput(this Vehicle? vehicle)
    {
        return vehicle != null ? new OutputVehicle(
            vehicle.Id,
            vehicle.CreatedDate,
            vehicle.ChangedDate,
            vehicle.IsActive,
            vehicle.Code,
            vehicle.Model,
            vehicle.Make,
            vehicle.Year,
            vehicle.Description) : null;
    }

    public static OutputVehicle? ToOutput(this VehicleDTO? vehicle)
    {
        return vehicle != null ? new OutputVehicle(
            vehicle.Id,
            vehicle.CreatedDate,
            vehicle.ChangedDate,
            vehicle.IsActive,
            vehicle.Code,
            vehicle.Model,
            vehicle.Make,
            vehicle.Year,
            vehicle.Description) : null;
    }
}