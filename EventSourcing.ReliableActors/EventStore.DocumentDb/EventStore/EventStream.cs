namespace EventStore.DocumentDb.EventStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class EventStream 
    {
        public EventStream(Guid streamId, IEnumerable<IEvent> events)
        {
            Id = streamId;
            Events = events.ToArray();
        }

        public IEvent[] Events { get; }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; private set; }
    }
}
