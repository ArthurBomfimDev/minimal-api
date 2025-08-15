using Newtonsoft.Json;

namespace Minimal.Application.ViewModels.IOs.Authenticate;

public class InputLoginAuthenticate
{
    public string Email { get; set; }
    public string Password { get; set; }

    public InputLoginAuthenticate() { }


    [JsonConstructor]
    public InputLoginAuthenticate(string email, string password)
    {
        Email = email;
        Password = password;
    }
}