using Microsoft.AspNetCore.Mvc;
using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Service.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) { 
            _accountService = accountService;
        }

        [HttpGet("getAllAccounts")]
        public ActionResult<Account> GetAllAccounts()
        {   
            var result = _accountService.GetAllAccounts();
            return Ok(result);
        }

        [HttpGet("getAccount/{id}")]
        public ActionResult<Account> GetAccount(int id)
        {
            var account = _accountService.GetAccountById(id);
            if(account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost("createAccount")]
        public ActionResult<Account> CreateAccount([FromBody] Account account)
        {
            if(account == null)
            {
                return BadRequest(string.Empty);
            }
            var createdAccount = _accountService.CreateAccount(account);
            return Ok(createdAccount);
        }

        [HttpDelete("deleteAccount/{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var result = _accountService.DeleteAccount(id);

            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("updateAccount")]
        public IActionResult UpdateAccount([FromBody] Account account) 
        { 
            if (account == null)
            {
                return BadRequest();
            }

            var result = _accountService.UpdateAccount(account);
            return Ok(result);
        }
    }
}
