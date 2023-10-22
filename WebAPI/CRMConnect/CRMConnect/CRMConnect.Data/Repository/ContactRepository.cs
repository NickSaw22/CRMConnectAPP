using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.DataAccess;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CRMConnect.CRMConnect.Data.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> AddContactDataAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<bool> DeleteContactDataAsync(int id)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == id);
            if (contact == null)
            {
                return false;
            }
            _context.Contacts.Remove(contact);
            return true;

        }

        public async Task<List<Contact>> GetAllContactsDataAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContactDataById(int id)
        {
            var result = await _context.Contacts.FirstOrDefaultAsync(c=> c.ContactId == id);
            return result;
        }

        public async Task<bool> UpdateContactDataAsync(Contact contact)
        {
            var existingContact = await _context.Contacts.FirstOrDefaultAsync(c=>c.ContactId == contact.ContactId);
            if(existingContact == null)
            {
                return false;
            }
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;    
            existingContact.PhoneNumber = contact.PhoneNumber;
            existingContact.DeptName = contact.DeptName;
            existingContact.EmailAddress = contact.EmailAddress;
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
