using Microsoft.Data.SqlClient;
using TransactionHandlerBasic;

namespace Database
{
    public class SqlServerTransaction : ITransaction, ITransactionIdProvider<SqlTransaction?>, IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection? _connection;
        private SqlTransaction? _transaction;

        public SqlTransaction? TransactionHandler => _transaction;

        public SqlServerTransaction(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task StartTransactionAsync()
        {
            if (_transaction != null)
                throw new InvalidOperationException("A transaction is already in progress.");

            _connection = new SqlConnection(_connectionString);
            await _connection.OpenAsync();
            _transaction = _connection.BeginTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");

            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
