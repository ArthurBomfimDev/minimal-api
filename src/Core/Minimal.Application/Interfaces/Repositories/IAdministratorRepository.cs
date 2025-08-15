using Minimal.Application.Interfaces.Repositories.Base;
using Minimal.Application.ViewModels.IOs.Administrator;
using Minimal.Domain.Entities;

namespace Minimal.Application.Interfaces.Repositories;

public interface IAdministratorRepository : IBaseRepository<Administrator>
{
    Task<Administrator?> GetByEmail(string email);
    List<Administrator> GetAllWithPagination(InputPaginationAdministrator inputPaginationAdministrator);
}