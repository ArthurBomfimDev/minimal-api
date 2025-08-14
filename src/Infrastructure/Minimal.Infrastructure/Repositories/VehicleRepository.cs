using Microsoft.EntityFrameworkCore;
using Minimal.Application.Interfaces.Repositories;
using Minimal.Application.ViewModels.IOs.Vehicle;
using Minimal.Domain.Entities;
using Minimal.Infrastructure.Persistence;
using Minimal.Infrastructure.Repositories.Base;

namespace Minimal.Infrastructure.Repositories;

public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Vehicle?> GetByCodeAsync(string code)
    {
        return await _dbSet.FirstOrDefaultAsync(v => v.Code == code);
    }

    public List<Vehicle> GetAllWithPagination(InputPaginationVehicle inputPaginationVehicle)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrEmpty(inputPaginationVehicle.Model))
            query = query.Where(v => EF.Functions.Like(v.Model.ToLower(), $"%{inputPaginationVehicle.Model.ToLower()}%"));
        else if (!string.IsNullOrEmpty(inputPaginationVehicle.Make))
            query = query.Where(v => EF.Functions.Like(v.Make.ToLower(), $"%{inputPaginationVehicle.Make.ToLower()}%"));

        int itensForPage = 10;

        if (inputPaginationVehicle.Page != null)
            query = query.Skip(((int)inputPaginationVehicle.Page - 1) * itensForPage).Take(itensForPage);

        return query.ToList();
    }
}