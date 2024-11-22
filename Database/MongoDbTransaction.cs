using MongoDB.Driver;
using TransactionHandlerBasic;

namespace Database
{
    public class MongoDbTransaction : ITransaction, ITransactionIdProvider<IClientSessionHandle?>, IDisposable
    {
        protected readonly IMongoClient _mongoClient;

        public IClientSessionHandle? TransactionHandler { get; private set; }

        public MongoDbTransaction(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task StartTransactionAsync()
        {
            if (TransactionHandler == null)
            {
                TransactionHandler = await _mongoClient.StartSessionAsync();
                TransactionHandler.StartTransaction();
            }
        }

        public async Task CommitTransactionAsync()
        {
            if (TransactionHandler == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await TransactionHandler.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (TransactionHandler == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await TransactionHandler.AbortTransactionAsync();
        }

        public void Dispose() => TransactionHandler?.Dispose();
    }
}
