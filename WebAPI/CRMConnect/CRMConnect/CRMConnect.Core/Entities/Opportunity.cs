using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.CRMConnect.Core.Entities
{
    public class Opportunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        public int ContactId { get; set; }
        public Contact? Contact { get; set; }
        public OpportunityStage Stage { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }
        public int? CreatedById { get; set; }
        public UserDetails? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedById { get; set; }
        public UserDetails? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        [NotMapped]
        public bool? isStatusChanged { get; set; }
    }

    public enum OpportunityStage
    {
        Lead,
        QualifiedLead,
        Opportunity,
        ClosedWon,
        ClosedLost
    }

    public class UserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Email { get; set; }
    }
}
