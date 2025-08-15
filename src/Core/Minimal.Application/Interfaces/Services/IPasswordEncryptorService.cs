namespace Minimal.Application.Interfaces.Services;

public interface IPasswordEncryptorService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}