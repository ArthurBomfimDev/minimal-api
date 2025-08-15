using Minimal.Application.ViewModels.IOs.Administrator;

namespace Minimal.Application.ViewModels.IOs.Authenticate;

public class OutputLoginAuthenticate
{
    public string Token { get; private set; }
    public OutputAdministrator Administrator { get; private set; }

    public OutputLoginAuthenticate() { }

    public OutputLoginAuthenticate(string token, OutputAdministrator administrator)
    {
        Token = token;
        Administrator = administrator;
    }
}