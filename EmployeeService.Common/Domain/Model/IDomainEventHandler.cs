namespace EmployeeService.Common.Domain.Model;

public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
{
    public Guid? SubscriberId { get; }
    
    public void Handle(TDomainEvent domainEvent);
}