namespace Minimal.Application.ViewModels.IOs.Administrator;

public class InputPaginationAdministrator
{
    public int Page { get; set; }

    public InputPaginationAdministrator() { }

    public InputPaginationAdministrator(int page)
    {
        Page = page;
    }
}