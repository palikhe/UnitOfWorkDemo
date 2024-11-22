using TransactionHandlerBasic;
using Database;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransaction _transaction;
        public AccountService(IAccountRepository accountRepository, ITransaction transaction)
        {
            _accountRepository = accountRepository;
            _transaction = transaction;
        }

        public async Task<int> GetBalanceAsync(int accountId)
        {
            return await _accountRepository.GetBalanceAsync(accountId);
        }

        public async Task<bool> AddFunds(int accountId, int amount)
        {
            var accountBalance = await _accountRepository.GetBalanceAsync(accountId);
            await _accountRepository.UpdateBalanceAsync(accountId, accountBalance + amount);

            return true;
        }

        public async Task<bool> TransferFundsAsync(int fromAccountId, int toAccountId, int amount)
        {
            try
            {
                await _transaction.StartTransactionAsync();

                var fromAccountBalance = await _accountRepository.GetBalanceAsync(fromAccountId);

                // Check balance before updating the balance of fromAccountId
                if (amount > fromAccountBalance)
                {
                    throw new ArgumentException("You cannot transfer more than you have");
                }

                await _accountRepository.UpdateBalanceAsync(fromAccountId, fromAccountBalance - amount);

                // For testing purpose - throwing the exception when transferring more than $20 and the from account update should get rolled back
                if (amount > 20)
                {
                    throw new InvalidOperationException();
                }

                var toAccountBalance = await _accountRepository.GetBalanceAsync(toAccountId);
                await _accountRepository.UpdateBalanceAsync(toAccountId, toAccountBalance + amount);

                await _transaction.CommitTransactionAsync();

                return true;
            }
            catch
            {
                await _transaction.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
