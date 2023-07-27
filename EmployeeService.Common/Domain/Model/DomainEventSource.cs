using System.Diagnostics.Tracing;
using System.Text.Json;

namespace EmployeeService.Common.Domain.Model;

[EventSource(Name = "EmployeeService.Common.Domain.Model")]
public sealed class DomainEventSource : EventSource
{
    private const int DomainEventId = 1;

    public DomainEventSource() { }

    [NonEvent]
    public void Log(IDomainEvent domainEvent)
    {
        if (!IsEnabled()) 
            return;
        
        string payload = JsonSerializer.Serialize(domainEvent);
        
        Log(domainEvent.AggregateSource, domainEvent.GetType().Name, domainEvent.Version, payload);
    }

    [Event(DomainEventId, Level = EventLevel.Informational, Message = "{0}")]
    public void Log(string source, string name, string version, string payload)
    {
        if (!IsEnabled()) 
            return;
        
        WriteEvent(DomainEventId, source, name, version, payload);
    }
}