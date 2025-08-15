using Minimal.Application.ViewModels.IOs.Administrator;
using Minimal.Domain.Entities;

namespace Minimal.Application.Mappers;

public static class AdministratorMapper
{
    public static OutputAdministrator? ToOutput(this Administrator? administrator)
    {
        return administrator != null ? new OutputAdministrator(
            administrator.Id, 
            administrator.Email, 
            administrator.Role.ToString()) : null;
    }
}