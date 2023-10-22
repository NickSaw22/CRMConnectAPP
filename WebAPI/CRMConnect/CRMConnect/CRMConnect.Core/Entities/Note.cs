using System.ComponentModel.DataAnnotations.Schema;

namespace CRMConnect.CRMConnect.Core.Entities
{
    public class Note
    {
        public  int  Id { get; set; }
        public string NoteTitle { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int? AssociatedEntityId { get; set; }
        public EntityType AssociatedEntityType { get; set; }
    }
}
