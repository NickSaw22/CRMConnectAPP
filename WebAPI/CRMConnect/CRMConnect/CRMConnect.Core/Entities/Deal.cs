using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.CRMConnect.Core.Entities
{
    public class Deal
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }
        public DealStage Stage { get; set; }

        [NotMapped]
        public bool? isStatusChanged { get; set; }

    }

    public enum DealStage 
    {
        Open, Pending, ClosedWon, ClosedLost        
    }
}
