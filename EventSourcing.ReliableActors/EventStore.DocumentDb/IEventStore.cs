using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventStore.DocumentDb
{
    public interface IEventStore
    {
        Task AppendToStream(Guid streamId, IEnumerable<IEvent> events);

        Task<EventStream> ReadStream(Guid streamId);
    }
}
