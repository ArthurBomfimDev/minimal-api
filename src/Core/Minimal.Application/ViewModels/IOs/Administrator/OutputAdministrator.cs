namespace Minimal.Application.ViewModels.IOs.Administrator;

public class OutputAdministrator
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Role { get; private set; }

    public OutputAdministrator()
    {
        
    }

    public OutputAdministrator(Guid id, string email, string role)
    {
        Id = id;
        Email = email;
        Role = role;
    }
}