using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.DocumentDb.EventStore;
using Newtonsoft.Json;

namespace EventStore.DocumentDb.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class EventStoreTests
    {
        private EventStore.EventStore event_store;

        [SetUp]
        public void Setup()
        {
            event_store = new EventStore.EventStore(
                new StoreConfiguration(
                    new Uri("https://finance-eventstore.documents.azure.com:443/"),
                    "9LBtt/UO4zqA9U7JojNhkN7U1eJpUVxspCCFOrKKPEH3HXYxbsPRJh4J44fGbwl3yKvzbUAoGR2SMiaSkY9NKw==",
                    "Finance",
                    "Payments"));

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Binder = new TypeNameSerialisationBinder("EventStore.DocumentDb.Tests.{0}, EventStore.DocumentDb.Tests"),
                TypeNameHandling = TypeNameHandling.Auto
            };
        }

        [Test]
        public async Task Create_New_Stream()
        {
            var id = Guid.NewGuid();

            await event_store.AppendToStream(id, new List<IEvent> {new Event(Guid.NewGuid(), "Test")});
        }

        [Test]
        public async Task Update_Existing_Stream()
        {
            var id = Guid.NewGuid();

            await event_store.AppendToStream(id, new List<IEvent> {new Event(Guid.NewGuid(), "Test")});

            await event_store.AppendToStream(id, new List<IEvent> {new SomeOtherEvent(Guid.NewGuid(), DateTime.Now, 5473)});
        }

        [Test]
        public async Task Read_Stream()
        {
            var id = Guid.NewGuid();

            await event_store.AppendToStream(
                id,
                new List<IEvent>
                {
                    new Event(Guid.NewGuid(), "Test"),
                    new SomeOtherEvent(Guid.NewGuid(), DateTime.Now, new Random().Next())
                });

            var stream = await event_store.ReadStream(id);

            Assert.That(stream.Events.Length, Is.EqualTo(2));
            Assert.That(stream.Id == id);
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

    public class SomeOtherEvent : IEvent
    {
        public SomeOtherEvent(Guid eventId, DateTime date, int value)
        {
            EventId = eventId;
            Date = date;
            Value = value;
        }

        public Guid EventId { get; }

        public DateTime Date { get; }

        public int Value { get; }
    }
}
