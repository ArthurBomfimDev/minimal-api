
using Minimal.Application.DTOs.Base;

namespace Minimal.DTOs;
public class VehicleDTO : BaseDTO<VehicleDTO>
{
    public string Code { get; private set; }
    public string Model { get; private set; }
    public string Make { get; private set; }
    public int Year { get; private set; }
    public string? Description { get; private set; }

    public VehicleDTO(Guid id, DateTimeOffset createdDate, DateTimeOffset? changedDate, bool isActive, string code, string model, string make, int year, string? description) : base(id, createdDate, changedDate, isActive)
    {
        Code = code;
        Model = model;
        Make = make;
        Year = year;
        Description = description;
    }
}