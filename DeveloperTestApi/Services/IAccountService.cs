using DeveloperTestApi.DTOs;
using DeveloperTestApi.Model;

namespace DeveloperTestApi.Services
{
    public interface IAccountService
    {
        List<AccountSummaryDto> GetTopLevelAccountsSummary();
        AccountDetailDto GetAccountDetails(string accNumber);
    }
}
