namespace EventStore.DocumentDb
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    [JsonObject]
    public class EventStream<T> : IEnumerable<T>
    {
        public EventStream(Guid streamId, IEnumerable<T> events)
        {
            Id = streamId;
            Events = events.ToArray();
        }

        public T[] Events { get; }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Events.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
