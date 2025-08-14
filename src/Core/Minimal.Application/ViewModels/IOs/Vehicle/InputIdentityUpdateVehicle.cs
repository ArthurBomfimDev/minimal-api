using Minimal.Application.ViewModels.IOs.Base;

namespace Minimal.Application.ViewModels.IOs.Vehicle;

public class InputIdentityUpdateVehicle(Guid id, InputUpdateVehicle? inputUpdate) : BaseInputIdentityUpdate<InputUpdateVehicle>(id, inputUpdate) { }