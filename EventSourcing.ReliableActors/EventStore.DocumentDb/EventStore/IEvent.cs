namespace EventStore.DocumentDb.EventStore
{
    using System;

    public interface IEvent
    {
        Guid EventId { get; }
    }
}