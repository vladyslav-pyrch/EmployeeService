namespace EmployeeService.Common.Domain.Model;

public interface IDomainEvent
{
    public string AggregateSource { get; }
    
    public string Version { get; }
    
    public DateTime OccuredOn { get; }
    
    public Guid? SubscriberId { get; }
}