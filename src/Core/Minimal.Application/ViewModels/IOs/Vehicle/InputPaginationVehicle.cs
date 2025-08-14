namespace Minimal.Application.ViewModels.IOs.Vehicle;

public class InputPaginationVehicle
{
    public int? Page { get; set; }
    public string? Model { get; set; }
    public string? Make { get; set; }

    public InputPaginationVehicle() { }

    public InputPaginationVehicle(int? page, string? model, string? make)
    {
        Page = page;
        Model = model;
        Make = make;
    }
}