namespace Services
{
    public interface IAccountService
    {
        Task<int> GetBalanceAsync(int accountId);
        Task<bool> AddFunds(int accountId, int amount);
        Task<bool> TransferFundsAsync(int fromAccountId, int toAccountId, int amount);
    }
}
