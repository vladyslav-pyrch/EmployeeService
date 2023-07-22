namespace EmployeeService.Common.Domain.Model;

public record DomainEvent : IDomainEvent
{
    public DomainEvent(string source, string version = "1.0", Guid? subscriberId = null)
    {
        AggregateSource = source;
        Version = version;
        OccuredOn = DateTime.UtcNow;
        SubscriberId = subscriberId;
    }
    
    public string AggregateSource { get; }
    public string Version { get; }
    public DateTime OccuredOn { get; }
    public Guid? SubscriberId { get; }
}