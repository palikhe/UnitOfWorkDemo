namespace TransactionHandlerBasic
{
    public interface ITransaction
    {
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
