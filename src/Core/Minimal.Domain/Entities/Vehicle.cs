using Minimal.Domain.Entities.Base;
using Minimal.Domain.Validation;
using Minimal.Domain.Validation.Result;

namespace Minimal.Domain.Entities;

public class Vehicle : BaseEntity<Vehicle>
{
    public string Code { get; private set; }
    public string Model { get; private set; }
    public string Make { get; private set; }
    public int Year { get; private set; }
    public string? Description { get; private set; }

    private Vehicle() : base() { }

    private Vehicle(string code, string model, string make, int year, string? description) : base()
    {
        Code = code;
        Model = model;
        Make = make;
        Year = year;
        Description = description;
    }

    public static BaseResult<Vehicle> Create(string code, string model, string make, int year, string? description)
    {
        var notifications = new List<Notification>();

        var codeValidation = ValidationRules.IsNull(code, nameof(code));
        if (codeValidation != null) notifications.Add(codeValidation);

        var modelValidation = ValidationRules.ValidateLength(model, nameof(model), maxLength: 100);
        if (modelValidation != null) notifications.Add(modelValidation);

        var makeValidation = ValidationRules.ValidateLength(make, nameof(make), maxLength: 50);
        if (makeValidation != null) notifications.Add(makeValidation);

        var yearValidation = ValidationRules.ValidateYear(year, nameof(year), minYear: 1950);
        if (yearValidation != null) notifications.Add(yearValidation);

        var descriptionValidation = ValidationRules.ValidateLength(description, nameof(description), maxLength: 300, isNullable: true);
        if (descriptionValidation != null) notifications.Add(descriptionValidation);

        if (notifications.Count > 0) return BaseResult<Vehicle>.Failure(notifications);

        var vehicle = new Vehicle(code, model, make, year, description);

        return BaseResult<Vehicle>.Success(vehicle);
    }

    public BaseResult<bool> Update(string model, string make, string? description)
    {
        var notifications = new List<Notification>();

        var modelValidation = ValidationRules.ValidateLength(model, nameof(model), maxLength: 100);
        if (modelValidation != null) notifications.Add(modelValidation);

        var makeValidation = ValidationRules.ValidateLength(make, nameof(make), maxLength: 50);
        if (makeValidation != null) notifications.Add(makeValidation);

        var descriptionValidation = ValidationRules.ValidateLength(description, nameof(description), maxLength: 300, isNullable: true);
        if (descriptionValidation != null) notifications.Add(descriptionValidation);

        if (notifications.Count > 0) return BaseResult<bool>.Failure(notifications);

        Model = model;
        Make = make;
        Description = description;
        SetChangedDate();

        return BaseResult<bool>.Success(true);
    }

    public BaseResult<bool> Delete()
    {
        if (!IsActive)
        {
            var notifications = new List<Notification> { Notification.Error("Este veículo já está inativo.") };
            return BaseResult<bool>.Failure(notifications);
        }

        return BaseResult<bool>.Success(true);
    }

}