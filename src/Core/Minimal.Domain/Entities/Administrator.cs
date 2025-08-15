using Minimal.Domain.Entities.Base;
using Minimal.Domain.Enuns.Registration;
using Minimal.Domain.Validation;
using Minimal.Domain.Validation.Result;

namespace Minimal.Domain.Entities;

public class Administrator : BaseEntity<Administrator>
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }

    private Administrator() { }

    public Administrator(string email, string password, string role) : base()
    {
        Email = email;
        Password = password;
        Role = role;
    }

    public static BaseResult<Administrator> Create(string email, string password, EnumRole role, string encryptedPassword)
    {
        var notifications = new List<Notification>();

        var emailValidation = ValidationRules.IsEmail(email, "Email", 320);
        if (emailValidation != null) notifications.Add(emailValidation);

        var passwordValidation = ValidationRules.ValidateLength(password, "Senha", maxLength: 30, minLength: 6);
        if (passwordValidation != null) notifications.Add(passwordValidation);

        var roleValidate = ValidationRules.ValidateEnum<EnumRole>(role, "Cargo");
        if (roleValidate != null) notifications.Add(roleValidate);

        if (notifications.Count > 0) return BaseResult<Administrator>.Failure(notifications);

        var administrator = new Administrator(email, encryptedPassword, role.ToString());
        return BaseResult<Administrator>.Success(administrator);
    }
}