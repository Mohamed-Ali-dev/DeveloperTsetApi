namespace DeveloperTestApi.DTOs
{
    public class AccountDetailDto
    {
        public string ACC_Number { get; set; }
        public decimal? Balance { get; set; }
        public List<AccountDetailDto> Children { get; set; } = [];
    }
}
