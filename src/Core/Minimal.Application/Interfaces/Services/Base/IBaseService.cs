using Minimal.Application.DTOs.Base;
using Minimal.Application.Interfaces.Repositories.Base;
using Minimal.Application.ViewModels.IOs.Base;
using Minimal.Domain.Entities.Base;

namespace Minimal.Application.Interfaces.Services.Base;

public interface IBaseService<TEntity, TIRepository, TValidateService, TDTO, TInputCreate, TInputUpdate, TInputIdentityUpdate, TInputIdentityDelete, TInputIdentityView, TOutput>
    where TEntity : BaseEntity<TEntity>
    where TIRepository : IBaseRepository<TEntity>
    where TDTO : BaseDTO<TDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentityUpdate : BaseInputIdentityUpdate<TInputUpdate>
    where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
    where TInputIdentityView : BaseInputIdentityView<TInputIdentityView>
    where TOutput : BaseOutput<TOutput>
{
    Task<TOutput?> GetByIdAsync(TInputIdentityView inputIdentityView);
    Task<List<TOutput?>> GetAllAsync();
}