using Minimal.Application.ViewModels.IOs.Administrator;
using Minimal.Application.ViewModels.IOs.Authenticate;
using Minimal.Domain.Validation.Result;

namespace Minimal.Application.Interfaces.Services;

public interface IAdministratorService
{
    Task<BaseResult<OutputAdministrator>> Register(InputCreateAdministrator inputCreateAdministrator);
    Task<OutputLoginAuthenticate> Login(InputLoginAuthenticate inputLoginAuthenticate);
    Task<OutputAdministrator?> GetByEmail(InputEmailViewAdministrator inputEmailViewAdministrator);
    Task<OutputAdministrator?> GetByIdAsync(InputIdentityViewAdministrator inputIdentityViewAdministrator);
    Task<List<OutputAdministrator?>> GetAllAsync();
    List<OutputAdministrator> GetAllWithPagination(InputPaginationAdministrator inputPaginationAdministrator);
}