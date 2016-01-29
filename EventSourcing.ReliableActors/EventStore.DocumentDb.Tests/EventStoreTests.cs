using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.DocumentDb.EventStore;

namespace EventStore.DocumentDb.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class EventStoreTests
    {
        [Test]
        public async Task test()
        {
            var eventStore = new EventStore.EventStore(
                new StoreConfiguration(
                    new Uri("https://finance-eventstore.documents.azure.com:443/"),
                    "9LBtt/UO4zqA9U7JojNhkN7U1eJpUVxspCCFOrKKPEH3HXYxbsPRJh4J44fGbwl3yKvzbUAoGR2SMiaSkY9NKw==",
                    "Finance",
                    "Payments"));

            await eventStore.AppendToStream(Guid.NewGuid(), new List<IEvent> {new Event(Guid.NewGuid(), "Test")});

            Assert.That(true == true);
        }
    }

    public class Event : IEvent
    {
        public Event(Guid eventId, string data)
        {
            EventId = eventId;
            Data = data;
        }

        public Guid EventId { get; }

        public string Data { get; }
    }
}
