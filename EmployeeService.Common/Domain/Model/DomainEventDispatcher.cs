using System.Collections.Concurrent;

namespace EmployeeService.Common.Domain.Model;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private ConcurrentBag<IDomainEvent> _stagedEvents = new();

    private ConcurrentDictionary<Subscriber, object>? _subscribers;

    private readonly DomainEventSource _eventSource;

    public DomainEventDispatcher(DomainEventSource domainEventSource)
    {
        _eventSource = domainEventSource;
        Publishing = false;
    }

    private bool Publishing { get; set; }

    private ConcurrentDictionary<Subscriber, object> Subscribers
    {
        get => _subscribers ??= new ConcurrentDictionary<Subscriber, object>();
        set => _subscribers = value;
    }

    public void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent =>
        _stagedEvents.Add(domainEvent);

    public void Commit<TDomainEvent>() where TDomainEvent : IDomainEvent
    {
        IEnumerable<TDomainEvent> stagedEvents = from stagedEvent in _stagedEvents
            where stagedEvent.GetType() == typeof(TDomainEvent)
            select (TDomainEvent)stagedEvent;

        foreach (var stagedEvent in stagedEvents)
            Publish(stagedEvent);

        ConcurrentBag<IDomainEvent> newBag = new();
        Interlocked.Exchange(ref _stagedEvents, newBag);
    }

    public void Publish<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
    {
        if (!HasSubscribers() || domainEvent == null)
            return;
        
        try
        {
            StartPublishing();

            Type domainEventType = domainEvent.GetType();
            IEnumerable<IDomainEventHandler<TDomainEvent>> subscribersToThisEvent = Subscribers
                .Where(pair => IsSubscribedToEvent(domainEvent, pair, domainEventType))
                .Select(pair => (IDomainEventHandler<TDomainEvent>)pair.Value);

            foreach (var subscriber in subscribersToThisEvent)
                subscriber.Handle(domainEvent);
        }
        finally
        {
            _eventSource.Log(domainEvent);

            StopPublishing();
        }
    }

    public void Register<TDomainEvent>(IDomainEventHandler<TDomainEvent> subscriber) where TDomainEvent : IDomainEvent
    {
        if (!Publishing && !Registered(subscriber))
            Subscribers.GetOrAdd(new Subscriber
            {
                SubscribedToType = typeof(TDomainEvent), SubscriberId = subscriber.SubscriberId
            }, subscriber);
    }

    public void Register<TDomainEvent>(Action<TDomainEvent> handle, Guid? subscriberId = null)
        where TDomainEvent : IDomainEvent
    {
        IDomainEventHandler<TDomainEvent> subscriber = new DomainEventHandler<TDomainEvent>(handle, subscriberId);
        
        Register(subscriber);
    }

    private bool Registered<TDomainEvent>(IDomainEventHandler<TDomainEvent> subscriber)
        where TDomainEvent : IDomainEvent => 
        Subscribers.Any(pair => pair.Key.SubscribedToType == typeof(TDomainEvent) &&
                                        pair.Key.SubscriberId == subscriber.SubscriberId);

    private static bool IsSubscribedToEvent<TDomainEvent>(TDomainEvent domainEvent,
        KeyValuePair<Subscriber, object> pair, Type domainEventType)
        where TDomainEvent : IDomainEvent
    {
        if (domainEvent.SubscriberId != null && domainEvent.SubscriberId != pair.Key.SubscriberId)
            return false;
        if (domainEventType == pair.Key.SubscribedToType)
            return true;
        if (domainEventType.IsSubclassOf(pair.Key.SubscribedToType))
            return true;

        return pair.Key.SubscribedToType.IsInterface &&
               domainEventType.GetInterfaces().Contains(pair.Key.SubscribedToType);
    }

    private bool HasSubscribers() => !Subscribers.IsEmpty;

    private void StartPublishing() => Publishing = true;

    private void StopPublishing() => Publishing = false;

    private struct Subscriber
    {
        public Guid? SubscriberId { get; set; }
        
        public Type SubscribedToType { get; set; } 
    }
    
    private class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly Action<TDomainEvent> _handle;
        
        public DomainEventHandler(Action<TDomainEvent> handle, Guid? subscriberId)
        {
            _handle += handle;
            SubscriberId = subscriberId;
        }
        
        public Guid? SubscriberId { get; }
        
        public void Handle(TDomainEvent domainEvent) => _handle(domainEvent);
    }
}