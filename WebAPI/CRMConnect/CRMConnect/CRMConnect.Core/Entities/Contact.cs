using System.Collections.Specialized;

namespace CRMConnect.CRMConnect.Core.Entities
{
    public class Contact
    {
        public int? ContactId { get; set; }
        public string FirstName { get; set; }
        public String LastName { get; set; }
        public string JobTitle { get; set; }
        public string DeptName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string SocialProfile { get; set; }
    }
}
