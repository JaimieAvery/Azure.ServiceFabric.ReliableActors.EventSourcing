namespace EventStore.DocumentDb
{
    using System;

    public class DocumentDbConfiguration
    {
        public DocumentDbConfiguration(Uri endpointAddress, string authorisationKey, string databaseId, string collectionId)
        {
            if (endpointAddress == null) throw new ArgumentNullException(nameof(endpointAddress));
            if (authorisationKey == null) throw new ArgumentNullException(nameof(authorisationKey));
            if (databaseId == null) throw new ArgumentNullException(nameof(databaseId));
            if (collectionId == null) throw new ArgumentNullException(nameof(collectionId));

            EndpointAddress = endpointAddress;
            AuthorisationKey = authorisationKey;
            DatabaseId = databaseId;
            CollectionId = collectionId;
        }

        public Uri EndpointAddress { get; }

        public string AuthorisationKey { get; }

        public string DatabaseId { get; }

        public string CollectionId { get; }

    }
}