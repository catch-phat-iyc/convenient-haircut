namespace Admin.Haircut.Business.Models.Rule
{
    public class RuleResponse
    {
        public long IdEmployee { get; set; }
        public long IdRule { get; set; }
        public long Id { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
        public string RuleName { get; set; }
    }
}
