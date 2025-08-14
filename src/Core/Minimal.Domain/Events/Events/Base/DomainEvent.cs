using Minimal.Domain.Events.Interface.Base;

namespace Minimal.Domain.Events.Events.Base;

public class DomainEvent : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; init; } = DateTimeOffset.UtcNow;
}
