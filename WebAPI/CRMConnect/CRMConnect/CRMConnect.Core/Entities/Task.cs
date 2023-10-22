namespace CRMConnect.CRMConnect.Core.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskCurrentStatus Status { get; set; }
        public int AssignedTo { get; set; } 
        public int AssociatedEntityId { get; set; } 
        /*public Account CreatedBy { get; set; }
        public Contact AssignedToEntityId { get; set; }
        public Opportunity AssignedToEntityType { get; set; }*/
    }

    public enum EntityType
    {
        Account,
        Contact,
        Opportunity,
        Deal
    }
    public enum TaskCurrentStatus
    {
        Todo,
        InProgress,
        Completed
    }

    public enum TaskPriority
    {
        Normal,
        Medium,
        Critical
    }
}
