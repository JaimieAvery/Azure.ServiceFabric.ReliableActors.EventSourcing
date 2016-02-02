using System.Linq;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace EventStore.DocumentDb.EventStore
{
    using System;
    using System.Collections.Generic;

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

/*        public static explicit operator EventStream(Document doc)
        {
            return new EventStream(
                new Guid(doc.GetPropertyValue<string>("id")),
                doc.GetPropertyValue<IList<IEvent>>("Events"));
        }*/
    }
}
