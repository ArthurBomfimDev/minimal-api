namespace Minimal.Application.DTOs.Base;

public abstract class BaseDTO<TDTO> where TDTO : BaseDTO<TDTO>
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedDate { get; protected set; }
    public DateTimeOffset? ChangedDate { get; protected set; }
    public bool IsActive { get; protected set; }

    protected BaseDTO(Guid id, DateTimeOffset createdDate, DateTimeOffset? changedDate, bool isActive)
    {
        Id = id;
        CreatedDate = createdDate;
        ChangedDate = changedDate;
        IsActive = isActive;
    }
}