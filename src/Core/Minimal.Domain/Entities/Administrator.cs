using Minimal.Domain.Entities.Base;
using Minimal.Domain.Enuns.Registration;

namespace Minimal.Domain.Entities;

public class Administrator : BaseEntity<Administrator>
{
    public string Code { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public EnumRole Role { get; private set; }

    private Administrator() { }

    public Administrator(string code, string email, string password, EnumRole role)
    {
        Code = code;
        Email = email;
        Password = password;
        Role = role;
    }
}