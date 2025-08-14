using Minimal.Application.ViewModels.IOs.Base;
using Newtonsoft.Json;

namespace Minimal.Application.ViewModels.IOs.Vehicle;

public class InputUpdateVehicle : BaseInputUpdate<InputUpdateVehicle>
{
    public string Model { get; set; }
    public string Make { get; set; }
    public string? Description { get; set; }

    public InputUpdateVehicle() { }

    [JsonConstructor]
    public InputUpdateVehicle(string model, string make, string? description)
    {
        Model = model;
        Make = make;
        Description = description;
    }
}