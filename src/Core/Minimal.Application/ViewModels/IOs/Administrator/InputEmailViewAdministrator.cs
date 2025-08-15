namespace Minimal.Application.ViewModels.IOs.Administrator;

public class InputEmailViewAdministrator
{
    public string Email { get; set; }

    public InputEmailViewAdministrator() { }

    public InputEmailViewAdministrator(string email)
    {
        Email = email;
    }
}