namespace EventStore.DocumentDb
{
    using System;

    public interface IEvent
    {
        Guid EventId { get; }
    }
}