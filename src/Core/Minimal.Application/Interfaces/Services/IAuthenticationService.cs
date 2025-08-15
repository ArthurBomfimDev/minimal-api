using Minimal.Domain.Entities;

namespace Minimal.Application.Interfaces.Services;

public interface IAuthenticationService
{
    string GenerateJwtToken(Administrator administrator);
}