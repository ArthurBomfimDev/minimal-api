namespace Minimal.Application.ViewModels.IOs.Base;

public abstract class BaseInputIdentityView<TInputIndetityView> where TInputIndetityView : BaseInputIdentityView<TInputIndetityView>
{
    public Guid Id { get; set; }

    public BaseInputIdentityView() { }

    public BaseInputIdentityView(Guid id)
    {
        Id = id;
    }
}
public class BaseInputIdentityView_0 : BaseInputIdentityView<BaseInputIdentityView_0> { }