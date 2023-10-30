namespace CRMConnect.CRMConnect.Core.Entities
{
    public class LogStatus
    {
        public int id { get; set; }
        public string AssociatedEntity { get; set; }
        public int EntityId { get; set; }
        public int StatusFrom { get; set; }
        public int StatusTo  { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
