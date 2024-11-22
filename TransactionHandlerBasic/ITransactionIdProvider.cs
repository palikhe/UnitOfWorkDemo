namespace TransactionHandlerBasic
{
    public interface ITransactionIdProvider<out T>
    {
        T TransactionHandler { get; }
    }
}
