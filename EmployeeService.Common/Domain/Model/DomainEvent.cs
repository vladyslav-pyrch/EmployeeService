namespace EmployeeService.Common.Domain.Model;

public abstract record DomainEvent : IDomainEvent
{
	protected DomainEvent(string source, string version = "1.0", Guid? subscriberId = null)
	{
		AggregateSource = source;
		Version = version;
		OccuredOn = DateTime.UtcNow;
		SubscriberId = subscriberId;
	}

	public string AggregateSource { get; set; }
	public string Version { get; set; }
	public DateTime OccuredOn { get; set; }
	public Guid? SubscriberId { get; }
}