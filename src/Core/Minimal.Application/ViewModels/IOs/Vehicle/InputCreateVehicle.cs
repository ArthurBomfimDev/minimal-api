using Minimal.Application.ViewModels.IOs.Base;
using Newtonsoft.Json;

namespace Minimal.Application.ViewModels.IOs.Vehicle;

public class InputCreateVehicle : BaseInputCreate<InputCreateVehicle>
{
    public string Code { get; set; }
    public string Model { get; set; }
    public string Make { get; set; }
    public int Year { get; set; }
    public string? Description { get; set; }

    public InputCreateVehicle() { }

    [JsonConstructor]
    public InputCreateVehicle(string code, string model, string make, int year, string? description)
    {
        Code = code;
        Model = model;
        Make = make;
        Year = year;
        Description = description;
    }
}