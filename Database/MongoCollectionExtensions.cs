using MongoDB.Driver;

namespace Database
{
    public static class MongoCollectionExtensions
    {
        // ReplaceOne with optional session
        public static Task<ReplaceOneResult> ReplaceOneExAsync<TDocument>(
            this IMongoCollection<TDocument> collection,
            IClientSessionHandle? session,
            FilterDefinition<TDocument> filter,
            TDocument document,
            ReplaceOptions? options = null)
        {
            if (session != null)
            {
                return collection.ReplaceOneAsync(session, filter, document, options);
            }
            else
            {
                return collection.ReplaceOneAsync(filter, document, options);
            }
        }

        // InsertOne with optional session (no return value needed)
        public static Task InsertOneExAsync<TDocument>(
            this IMongoCollection<TDocument> collection,
            IClientSessionHandle? session,
            TDocument document,
            InsertOneOptions? options = null)
        {
            if (session != null)
            {
                return collection.InsertOneAsync(session, document, options);
            }
            else
            {
                return collection.InsertOneAsync(document, options);
            }
        }

        // InsertMany with optional session (no return value needed)
        public static Task InsertManyExAsync<TDocument>(
            this IMongoCollection<TDocument> collection,
            IClientSessionHandle? session,
            IEnumerable<TDocument> documents,
            InsertManyOptions? options = null)
        {
            if (session != null)
            {
                return collection.InsertManyAsync(session, documents, options);
            }
            else
            {
                return collection.InsertManyAsync(documents, options);
            }
        }

        // UpdateOne with optional session
        public static Task<UpdateResult> UpdateOneExAsync<TDocument>(
            this IMongoCollection<TDocument> collection,
            IClientSessionHandle? session,
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update,
            UpdateOptions? options = null)
        {
            if (session != null)
            {
                return collection.UpdateOneAsync(session, filter, update, options);
            }
            else
            {
                return collection.UpdateOneAsync(filter, update, options);
            }
        }

        // UpdateMany with optional session
        public static Task<UpdateResult> UpdateManyExAsync<TDocument>(
            this IMongoCollection<TDocument> collection,
            IClientSessionHandle? session,
            FilterDefinition<TDocument> filter,
            UpdateDefinition<TDocument> update,
            UpdateOptions? options = null)
        {
            if (session != null)
            {
                return collection.UpdateManyAsync(session, filter, update, options);
            }
            else
            {
                return collection.UpdateManyAsync(filter, update, options);
            }
        }

        // DeleteOne with optional session
        public static Task<DeleteResult> DeleteOneExAsync<TDocument>(
            this IMongoCollection<TDocument> collection,
            IClientSessionHandle? session,
            FilterDefinition<TDocument> filter,
            CancellationToken cancellationToken = default)
        {
            if (session != null)
            {
                return collection.DeleteOneAsync(session, filter, options: null, cancellationToken: cancellationToken);
            }
            else
            {
                return collection.DeleteOneAsync(filter, cancellationToken);
            }
        }

        // DeleteMany with optional session
        public static Task<DeleteResult> DeleteManyExAsync<TDocument>(
            this IMongoCollection<TDocument> collection,
            IClientSessionHandle? session,
            FilterDefinition<TDocument> filter,
            CancellationToken cancellationToken = default)
        {
            if (session != null)
            {
                return collection.DeleteManyAsync(session, filter, options: null, cancellationToken: cancellationToken);
            }
            else
            {
                return collection.DeleteManyAsync(filter, cancellationToken);
            }
        }
    }
}
