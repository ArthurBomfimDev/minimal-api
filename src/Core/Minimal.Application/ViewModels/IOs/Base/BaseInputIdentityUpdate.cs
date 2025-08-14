using System.Text.Json.Serialization;

namespace Minimal.Application.ViewModels.IOs.Base;

public abstract class BaseInputIdentityUpdate<TInputUpdate> where TInputUpdate : BaseInputUpdate<TInputUpdate>
{
    public Guid Id { get; set; }
    public TInputUpdate? InputUpdate { get; set; }

    public BaseInputIdentityUpdate() { }

    [JsonConstructor]
    public BaseInputIdentityUpdate(Guid id, TInputUpdate? inputUpdate)
    {
        Id = id;
        InputUpdate = inputUpdate;
    }
}

public class BaseInputIdentityUpdate_0 : BaseInputIdentityUpdate<BaseInputUpdate_0> { }