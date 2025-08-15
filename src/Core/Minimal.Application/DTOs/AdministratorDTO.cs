using Minimal.Application.DTOs.Base;
using Minimal.Domain.Enuns.Registration;

namespace Minimal.DTOs;
public class AdministratorDTO : BaseDTO<AdministratorDTO>
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public EnumRole Role { get; private set; }

    public AdministratorDTO(Guid id, DateTimeOffset createdDate, DateTimeOffset? changedDate, bool isActive, string email, string password, EnumRole role) : base(id, createdDate, changedDate, isActive)
    {
        Email = email;
        Password = password;
        Role = role;
    }
}