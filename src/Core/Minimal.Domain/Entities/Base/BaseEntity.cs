namespace Minimal.Domain.Entities.Base;

public abstract class BaseEntity<TEntity> where TEntity : BaseEntity<TEntity>
{
    public Guid Id { get; protected set; }
    public DateTimeOffset CreatedDate { get; protected set; }
    public DateTimeOffset? ChangedDate { get; protected set; }
    public bool IsActive { get; protected set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTimeOffset.UtcNow;
        IsActive = true;
    }

    #region methods
    public virtual void SetChangedDate()
    {
        ChangedDate = DateTimeOffset.UtcNow;
    }

    public virtual bool Deactivate()
    {
        if (IsActive)
        {
            IsActive = false;
            SetChangedDate();

            return true;
        }

        return false;
    }

    public virtual bool Activate()
    {
        if (!IsActive)
        {
            IsActive = true;
            SetChangedDate();

            return true;
        }

        return false;
    }
    #endregion
}