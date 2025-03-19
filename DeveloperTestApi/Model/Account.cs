using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeveloperTestApi.Model
{
    public class Account
    {
        public string ACC_number { get; set; }
        public decimal? Balance { get; set; }
        public string? ACC_Parent { get; set; }
        public Account ParentAccount { get; set; }
        public ICollection<Account> ChildAccounts { get; set; } = new List<Account>();
    }
}
