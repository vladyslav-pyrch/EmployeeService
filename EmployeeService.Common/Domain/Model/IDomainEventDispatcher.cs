namespace EmployeeService.Common.Domain.Model;

public interface IDomainEventDispatcher
{
    public void Add<TDomainEvent>(TDomainEvent domainEvent)where TDomainEvent : IDomainEvent;
    
    public void Commit<TDomainEvent>() where TDomainEvent : IDomainEvent;
    
    public void Publish<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent;
    
    public void Register<TDomainEvent>(IDomainEventHandler<TDomainEvent> subscriber) where TDomainEvent : IDomainEvent;
    
}