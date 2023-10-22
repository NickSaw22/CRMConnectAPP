using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.CRMConnect.Core.Entities
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public OpportunityStage Stage { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }
    }

    public enum OpportunityStage
    {
        Lead,
        QualifiedLead,
        Opportunity,
        ClosedWon,
        ClosedLost
    }
}
