using DeveloperTestApi.DTOs;

namespace DeveloperTestApi.Services
{
    public interface IAccountService
    {
        List<AccountSummaryDto> GetTopLevelAccountsSummary();
        AccountDetailDto GetAccountDetails(string accNumber);
    }
}
