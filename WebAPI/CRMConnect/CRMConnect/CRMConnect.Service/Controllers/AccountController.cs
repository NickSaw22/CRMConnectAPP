using Microsoft.AspNetCore.Mvc;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Service.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) { 
            _accountService = accountService;
        }

        [HttpGet("getAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {   
            var result = await _accountService.GetAllAccountsAsync();
            return Ok(result);
        }

        [HttpGet("getAccount/{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if(account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost("createAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            if(account == null)
            {
                return BadRequest(string.Empty);
            }
            var createdAccount = await _accountService.CreateAccountAsync(account);
            return Ok(createdAccount);
        }

        [HttpDelete("deleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);

            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("updateAccount")]
        public async Task<IActionResult> UpdateAccount([FromBody] Account account) 
        { 
            if (account == null)
            {
                return BadRequest();
            }

            var result = await _accountService.UpdateAccountAsync(account);
            return Ok(result);
        }
    }
}
