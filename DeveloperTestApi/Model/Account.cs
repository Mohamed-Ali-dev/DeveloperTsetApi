using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DeveloperTestApi.Model
{
    public class Account
    {
        public string ACC_Number { get; set; }
        public decimal? Balance { get; set; }
        public string? ACC_Parent { get; set; }
        [JsonIgnore]
        public Account ParentAccount { get; set; }
        [JsonIgnore]
        public ICollection<Account> ChildAccounts { get; set; } = new List<Account>();
    }
}
