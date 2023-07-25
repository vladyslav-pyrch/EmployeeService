namespace EmployeeService.Common.Domain.Model;

public abstract class Entity<TIdentity> : IEntity<TIdentity> where TIdentity : IIdentity
{
    private readonly TIdentity _identity = default!;

    private readonly List<IDomainEvent> _domainEvents;

    protected Entity(TIdentity identity)
    {
        Identity = identity;
        _domainEvents = new List<IDomainEvent>();
    }

    public TIdentity Identity
    {
        get => _identity;
        private init
        {
            ArgumentNullException.ThrowIfNull(value);

            _identity = value;
        }
    }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();
    
    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    
}