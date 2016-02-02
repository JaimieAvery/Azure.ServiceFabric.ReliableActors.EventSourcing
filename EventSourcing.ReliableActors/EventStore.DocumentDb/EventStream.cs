using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EventStore.DocumentDb
{
    [JsonObject]
    public class EventStream : IEnumerable<IEvent>
    {
        public EventStream(Guid streamId, IEnumerable<IEvent> events)
        {
            Id = streamId;
            Events = events.ToArray();
        }

        public IEvent[] Events { get; }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; private set; }

        public IEnumerator<IEvent> GetEnumerator()
        {
            return Events.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
