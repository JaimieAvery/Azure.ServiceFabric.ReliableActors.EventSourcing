namespace EventStore.DocumentDb
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEventStore<T>
    {
        Task AppendToStream(Guid streamId, IEnumerable<T> events);

        Task<EventStream<T>> ReadStream(Guid streamId);
    }
}
