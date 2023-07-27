namespace EmployeeService.Common.Domain.Model;

public interface IDomainEvent
{
    public string AggregateSource { get; set; }
    
    public string Version { get; set; }
    
    public DateTime OccuredOn { get; set; }
    
    public Guid? SubscriberId { get; }
}