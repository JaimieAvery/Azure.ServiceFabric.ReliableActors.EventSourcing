namespace EventStore.DocumentDb.EventStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;

    public class EventStore : IEventStore
    {
        private readonly StoreConfiguration store_configuration;
        private bool database_initialised;

        public EventStore(StoreConfiguration storeConfiguration)
        {
            if (storeConfiguration == null)
                throw new ArgumentNullException(nameof(storeConfiguration));

            store_configuration = storeConfiguration;
        }

        public async Task AppendToStream(Guid streamId, IList<IEvent> events)
        {
            var client = new DocumentClient(store_configuration.EndpointAddress, store_configuration.AuthorisationKey);
            await InitialiseStore(client);

            var stream = ReadStream(streamId, client);

            var updatedStream = new EventStream(stream.Id, stream.Events.Concat(events));

            await client.UpsertDocumentAsync($"dbs/{store_configuration.DatabaseId}/colls/{store_configuration.CollectionId}", updatedStream);
        }

        public async Task<EventStream> ReadStream(Guid streamId)
        {
            var client = new DocumentClient(store_configuration.EndpointAddress, store_configuration.AuthorisationKey);
            await InitialiseStore(client);

            return ReadStream(streamId, client);
        }

        private EventStream ReadStream(Guid streamId, DocumentClient client)
        {
            
            var r = client
                .CreateDocumentQuery<EventStream>(
                    $"dbs/{store_configuration.DatabaseId}/colls/{store_configuration.CollectionId}")
                .Where(x => x.Id == streamId)
                .AsEnumerable()
                .FirstOrDefault();

            return r == null ? new EventStream(streamId, new List<IEvent>()) : (EventStream)r;
        }

        private async Task InitialiseStore(DocumentClient client)
        {
            if (database_initialised)
                return;

            await InitialiseDatabase(client);
            await InitialiseCollection(client);

            database_initialised = true;
        }

        private async Task InitialiseDatabase(DocumentClient client)
        {
            var database = client.CreateDatabaseQuery().Where(db => db.Id == store_configuration.DatabaseId).AsEnumerable().FirstOrDefault();
            if (database == null)
                await client.CreateDatabaseAsync(new Database {Id = store_configuration.DatabaseId});
        }

        private async Task InitialiseCollection(DocumentClient client)
        {
            var collection = client.CreateDocumentCollectionQuery($"dbs/{store_configuration.DatabaseId}").Where(c => c.Id == store_configuration.CollectionId).AsEnumerable().FirstOrDefault();
            if (collection == null)
                await client.CreateDocumentCollectionAsync($"dbs/{store_configuration.DatabaseId}", new DocumentCollection {Id = store_configuration.CollectionId});
        }
    }
}