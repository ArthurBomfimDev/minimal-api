namespace Minimal.Application.ViewModels.IOs.Base;

public abstract class BaseOutput<TOutuput> where TOutuput : BaseOutput<TOutuput>
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedDate { get; protected set; }
    public DateTimeOffset? ChangedDate { get; protected set; }
    public bool IsActive { get; protected set; }

    public BaseOutput() { }

    public BaseOutput(Guid id, DateTimeOffset createdDate, DateTimeOffset? changedDate, bool isActive)
    {
        Id = id;
        CreatedDate = createdDate;
        ChangedDate = changedDate;
        IsActive = isActive;
    }
}
public class BaseOutput_0 : BaseOutput<BaseOutput_0> { }