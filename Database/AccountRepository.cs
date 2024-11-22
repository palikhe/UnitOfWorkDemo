using MongoDB.Driver;
using TransactionHandlerBasic;

namespace Database
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<AccountDocument> _mongoCollection;

        private readonly ITransactionIdProvider<IClientSessionHandle?> _transactionIdProvider;

        public AccountRepository(IMongoClient mongoClient, ITransactionIdProvider<IClientSessionHandle?> transactionIdProvider)
        {
            _mongoDatabase = mongoClient.GetDatabase("ProductLocationDb");

            _mongoCollection = _mongoDatabase.GetCollection<AccountDocument>("TempAccountCollection");

            _transactionIdProvider = transactionIdProvider;
        }

        public async Task<int> GetBalanceAsync(int accountId)
        {
            var filter = Builders<AccountDocument>.Filter.Eq(nameof(AccountDocument.AccountId), accountId);
            var results = await _mongoCollection.FindAsync(filter);
            var accounts = await results.ToListAsync();

            if (accounts.Any())
            {
                return accounts.First().Balance;
            }

            return 0;
        }

        public async Task UpdateBalanceAsync(int accountId, int newBalance)
        {
            var account = new AccountDocument { AccountId = accountId, Balance = newBalance };

            // Define filter for upsert based on AccountId
            var filter = Builders<AccountDocument>.Filter.Eq(nameof(AccountDocument.AccountId), accountId);

            // Define update options for upsert (replace if exists, insert if not)
            var options = new ReplaceOptions { IsUpsert = true };

            // Use the transactionHandler
            await _mongoCollection.ReplaceOneExAsync(
                _transactionIdProvider.TransactionHandler, // Pass the handle
                filter,
                account,
                options);
        }
    }
}
