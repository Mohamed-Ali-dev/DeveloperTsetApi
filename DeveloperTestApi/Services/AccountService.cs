using DeveloperTestApi.Data;
using DeveloperTestApi.DTOs;
using DeveloperTestApi.Model;

namespace DeveloperTestApi.Services
{
    public class AccountService(AppDbContext context) : IAccountService
    {
        public List<AccountSummaryDto> GetTopLevelAccountsSummary()
        {
            var allAccounts = context.Accounts.ToList();
            var accountDict = allAccounts.ToDictionary(a => a.ACC_Number);
            var balanceDict = new Dictionary<string, decimal>();
            var accountSummaryDto = allAccounts
                .Where(a => a.ACC_Parent == null)
                .Select(a => new AccountSummaryDto
                {
                    TopLevelAccount = a.ACC_Number,
                    TotalBalance = CalculateTotalBalance(accountDict, balanceDict, a.ACC_Number)
                }).ToList();
            return accountSummaryDto;
        }
        public AccountDetailDto GetAccountDetails(string accNumber)
        {
            var allAccounts = context.Accounts.ToList();
            var accountDict = allAccounts.ToDictionary(a => a.ACC_Number.Trim());
            var accountTree = BuildAccountTree(accountDict, accNumber.Trim());
            return accountTree;
        }
        private AccountDetailDto BuildAccountTree(Dictionary<string, Account> accountDict, string acc_number)
        {
            // If the account is not found, return null.
            if (!accountDict.TryGetValue(acc_number, out var account))
                return null;

            // Create a DTO for the current account.
            var dto = new AccountDetailDto
            {
                ACC_Number = account.ACC_Number.Trim(),
                Balance = account.Balance
            };

            // Find all direct children for this account.
            var children = accountDict.Values
                .Where(a => (a.ACC_Parent ?? string.Empty).Trim() == acc_number)
                .ToList();

            // For each child, recursively build its tree and add it to the current DTO.
            foreach (var child in children)
            {
                var childDto = BuildAccountTree(accountDict, child.ACC_Number.Trim());
                if (childDto != null)
                    dto.Children.Add(childDto);
            }

            return dto;
        }
        //private List<Account> GetLeafAccounts(
        //   Dictionary<string, Account> accountDict, string acc_number)
        //{
        //    if (!accountDict.TryGetValue(acc_number, out var account))
        //        return new List<Account>();

        //    var children = accountDict.Values
        //        .Where(a => (a.ACC_Parent ?? string.Empty).Trim() == acc_number)
        //        .ToList();

        //    if (children.Count == 0)
        //    {
        //        var leafList = new List<Account> { account };
        //        return leafList;
        //    }


        //    var result = new List<Account>();
        //    foreach (var child in children)
        //    {
        //        result.AddRange(GetLeafAccounts(accountDict, child.ACC_Number.Trim()));
        //    }

        //    return result;
        //}
        private decimal CalculateTotalBalance(Dictionary<string, Account> accountDict, Dictionary<string, decimal> balanceDict, string acc_number)
        {
            if (balanceDict.TryGetValue(acc_number, out var cachedValue))
            {
                return cachedValue;
            }

            if (!accountDict.TryGetValue(acc_number, out var account))
            {
                
                return 0;
            }

            var children = accountDict.Values
                .Where(a => a.ACC_Parent== acc_number)
                .ToList();

            if (children.Count == 0)
            {
                var balance = account.Balance ?? 0;
                balanceDict[acc_number] = balance;
                return balance;
            }

            var total = children.Sum(child => CalculateTotalBalance(accountDict, balanceDict, child.ACC_Number));
            balanceDict[acc_number] = total;
            return total;

        }
    }
}
