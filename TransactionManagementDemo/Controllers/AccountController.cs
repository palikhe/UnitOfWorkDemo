using Microsoft.AspNetCore.Mvc;
using Services;

namespace TransactionHandlerBasic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet(Name = "GetAccountBalance")]
        public async Task<IActionResult> Get(int accountId)
        {
            return Ok(await _accountService.GetBalanceAsync(accountId));
        }

        [HttpPost]
        [Route("AddFunds")]
        public async Task<IActionResult> AddFunds(int accountId, int amount)
        {
            if (await _accountService.AddFunds(accountId, amount))
                return Ok(true);

            return Ok(false);
        }

        [HttpPost]
        [Route("TransferFunds")]
        public async Task<IActionResult> TransferFunds(int fromAccountId, int toAccountId, int amount)
        {
            if (await _accountService.TransferFundsAsync(fromAccountId, toAccountId, amount))
                return Ok(true);

            return Ok(false);
        }
    }
}
