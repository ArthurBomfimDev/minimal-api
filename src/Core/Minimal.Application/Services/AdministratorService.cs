using Minimal.Application.Interfaces.Repositories;
using Minimal.Application.Interfaces.Services;
using Minimal.Application.Interfaces.UnitOfWork;
using Minimal.Application.Mappers;
using Minimal.Application.ViewModels.IOs.Administrator;
using Minimal.Application.ViewModels.IOs.Authenticate;
using Minimal.Domain.Entities;
using Minimal.Domain.Validation.Result;

namespace Minimal.Application.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IPasswordEncryptorService _encryptor;
    private readonly IAuthenticationService _authenticationService;

    public AdministratorService(IUnitOfWork unitOfWork, IAdministratorRepository administratorRepository, IPasswordEncryptorService encryptor, IAuthenticationService authenticationService)
    {
        _unitOfWork = unitOfWork;
        _administratorRepository = administratorRepository;
        _encryptor = encryptor;
        _authenticationService = authenticationService;
    }

    public async Task<OutputLoginAuthenticate> Login(InputLoginAuthenticate inputLoginAuthenticate)
    {
        var admin = await _administratorRepository.GetByEmail(inputLoginAuthenticate.Email);

        if (admin == null || (admin!= null && !_encryptor.VerifyPassword(inputLoginAuthenticate.Password, admin.Password)))
        {
            throw new UnauthorizedAccessException("Email ou senha inválidos.");
        }

        var token = _authenticationService.GenerateJwtToken(admin);
        var outputAdministrator = admin.ToOutput();

        return new OutputLoginAuthenticate(token, outputAdministrator);
    }

    public async Task<BaseResult<OutputAdministrator>> Register(InputCreateAdministrator inputCreateAdministrator)
    {
        var existingAdministrator = await _administratorRepository.GetByEmail(inputCreateAdministrator.Email);

        if (existingAdministrator != null)
            return BaseResult<OutputAdministrator>.Failure(Notification.Error("O Email digitado já está em uso!"));

        var passwordEncryptor = _encryptor.HashPassword(inputCreateAdministrator.Password);
        var adiministratorResult = Administrator.Create(inputCreateAdministrator.Email, inputCreateAdministrator.Password, inputCreateAdministrator.Role, passwordEncryptor);

        if (!adiministratorResult.IsSuccess)
            return BaseResult<OutputAdministrator>.Failure(adiministratorResult.listNotification);

        await _administratorRepository.Create(adiministratorResult.Value!);
        await _unitOfWork.SaveChangesAsync();

        return BaseResult<OutputAdministrator>.Success(adiministratorResult.Value.ToOutput()!, Notification.Success("Conta criada com sucesso!"));
    }

    public async Task<List<OutputAdministrator?>> GetAllAsync()
    {
        var getAll = await _administratorRepository.GetAllAsync();
        return getAll.Select(x => x.ToOutput()).ToList() ?? [];
    }

    public async Task<OutputAdministrator?> GetByIdAsync(InputIdentityViewAdministrator inputIdentityViewAdministrator)
    {
        var getById = await _administratorRepository.GetByIdAsync(inputIdentityViewAdministrator.Id);
        return getById.ToOutput();
    }

    public async Task<OutputAdministrator?> GetByEmail(InputEmailViewAdministrator inputEmailViewAdministrator)
    {
        var getById = await _administratorRepository.GetByEmail(inputEmailViewAdministrator.Email);
        return getById.ToOutput();
    }

    public List<OutputAdministrator> GetAllWithPagination(InputPaginationAdministrator inputPaginationAdministrator)
    {
        var getAllWithPagination = _administratorRepository.GetAllWithPagination(inputPaginationAdministrator);
        return getAllWithPagination.Select(x => x.ToOutput()!).ToList() ?? [];
    }
}