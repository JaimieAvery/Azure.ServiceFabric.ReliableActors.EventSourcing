using System.Collections;

namespace EventStore.DocumentDb.EventStore
{
    using System;
    using System.Collections.Generic;

    public class EventStream : IEnumerable<IEvent>
    {
        public EventStream(Guid streamId, IEnumerable<IEvent> events)
        {
            StreamId = streamId;
            Events = events;
        }

        public IEnumerable<IEvent> Events { get; }

        public Guid StreamId { get; private set; }

        public IEnumerator<IEvent> GetEnumerator()
        {
            return Events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
