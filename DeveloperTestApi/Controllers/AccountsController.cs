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
            if (accountSummary == null || accountSummary.Count == 0)
            {
                return NotFound("No top-level accounts found.");
            }
            return Ok(accountSummary);
        }
        [HttpGet("details/{accNumber}")]
        public IActionResult GetAccountDetails(string accNumber)
        {
            if (string.IsNullOrWhiteSpace(accNumber))
            {
                return BadRequest("Account number must be provided.");
            }
            var accountTree = accountService.GetAccountDetails(accNumber);
            if (accountTree == null)
            {
                return NotFound($"Account with number '{accNumber}' not found.");
            }
            return Ok(accountTree);
        }
        
    }

}
