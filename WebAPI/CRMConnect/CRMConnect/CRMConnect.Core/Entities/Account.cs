using Microsoft.EntityFrameworkCore;
using System;

namespace CRMConnect.CRMConnect.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

}
