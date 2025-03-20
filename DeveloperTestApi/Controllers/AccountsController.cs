using DeveloperTestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IAccountService accountService) : ControllerBase
    {
        [HttpGet("top-level")]
        public IActionResult Get()
        {
            var accountSummary = accountService.GetTopLevelAccountsSummary();
            return Ok(accountSummary);
        }
        [HttpGet("details/{accNumber}")]
        public IActionResult GetAccountDetails(string accNumber)
        {
            var leafAccounts = accountService.GetAccountDetails(accNumber);
            return Ok(leafAccounts);
        }
        
    }

}
