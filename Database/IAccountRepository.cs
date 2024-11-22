namespace Database
{
    public interface IAccountRepository
    {
        Task<int> GetBalanceAsync(int accountId);

        Task UpdateBalanceAsync(int accountId, int newBalance);
    }
}
