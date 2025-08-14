namespace Minimal.Domain.Events.Interface.Base;

public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}