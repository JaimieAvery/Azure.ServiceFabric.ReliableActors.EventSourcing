using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace EventStore.DocumentDb.EventStore
{
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

        public async Task AppendToStream(Guid streamId, IEnumerable<IEvent> events)
        {
            var client = new DocumentClient(store_configuration.EndpointAddress, store_configuration.AuthorisationKey);
            await InitialiseStore(client);

            var stream = ReadStream(streamId, client);
            if (stream != null)
            {
                stream.Events.ToList().AddRange(events);
            }
            else
            {
                stream = new EventStream(streamId, events);
            }
            
            await client.ReplaceDocumentAsync($"dbs/{store_configuration.DatabaseId}/colls/{store_configuration.CollectionId}/{streamId}", stream);
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
                .CreateDocumentQuery($"dbs/{store_configuration.DatabaseId}/colls/{store_configuration.CollectionId}")
                .Where(x => x.Id == streamId.ToString("D"))
                .AsEnumerable()
                .FirstOrDefault();

            return r?.GetPropertyValue<EventStream>("EventStream");
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