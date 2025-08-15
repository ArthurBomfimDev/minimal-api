using Minimal.Application.ViewModels.IOs.Base;
using Minimal.Domain.Enuns.Registration;

namespace Minimal.Application.ViewModels.IOs.Administrator;

public class InputCreateAdministrator : BaseInputCreate<InputCreateAdministrator>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public EnumRole Role { get; set; }

    public InputCreateAdministrator() { }

    public InputCreateAdministrator(string email, string password, EnumRole role)
    {
        Email = email;
        Password = password;
        Role = role;
    }
}