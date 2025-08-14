using System.Text.Json.Serialization;

namespace Minimal.Application.ViewModels.IOs.Base;

public abstract class BaseInputIdentityDelete<TInputIdentityDelete> where TInputIdentityDelete : BaseInputIdentityDelete<TInputIdentityDelete>
{
    public Guid Id { get; set; }

    [JsonConstructor]
    public BaseInputIdentityDelete(Guid id)
    {
        Id = id;
    }
}
public class BaseInputIdentityDelete_0(Guid id) : BaseInputIdentityDelete<BaseInputIdentityDelete_0>(id) { }