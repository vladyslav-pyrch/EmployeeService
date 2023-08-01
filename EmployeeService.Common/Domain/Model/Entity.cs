namespace EmployeeService.Common.Domain.Model;

public abstract class Entity<TIdentity> : IEntity<TIdentity> where TIdentity : IIdentity
{
	private readonly List<IDomainEvent> _domainEvents;
	private readonly TIdentity _identity = default!;

	protected Entity(TIdentity identity)
	{
		Identity = identity;
		_domainEvents = new List<IDomainEvent>();
	}

	public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

	protected string Source => GetType().Name;

	public TIdentity Identity
	{
		get => _identity;
		private init
		{
			ArgumentNullException.ThrowIfNull(value);

			_identity = value;
		}
	}

	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}

	protected void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}
}