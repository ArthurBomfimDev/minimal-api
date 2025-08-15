using Microsoft.EntityFrameworkCore;
using Minimal.Application.Interfaces.Repositories;
using Minimal.Application.ViewModels.IOs.Administrator;
using Minimal.Domain.Entities;
using Minimal.Infrastructure.Persistence;
using Minimal.Infrastructure.Repositories.Base;

namespace Minimal.Infrastructure.Repositories;

public class AdministratorRepository : BaseRepository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(AppDbContext context) : base(context) { }

    public async Task<Administrator?> GetByEmail(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
    }
    public List<Administrator> GetAllWithPagination(InputPaginationAdministrator inputPaginationAdministrator)
    {
        var query = _dbSet.AsQueryable();

        int itensForPage = 10;

        if (inputPaginationAdministrator.Page != null)
            query = query.Skip(((int)inputPaginationAdministrator.Page - 1) * itensForPage).Take(itensForPage);

        return query.ToList();
    }
}