using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;

namespace CRMConnect.CRMConnect.Business.Implementaions
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<Contact> AddContactAsync(Contact contact)
        {
            return _contactRepository.AddContactDataAsync(contact);
        }

        public Task<bool> DeleteContactAsync(int id)
        {
            return _contactRepository.DeleteContactDataAsync(id);
        }

        public Task<List<Contact>> GetAllContactsAsync()
        {
            return _contactRepository.GetAllContactsDataAsync();
        }

        public async Task<Contact> GetContactAsync(int id)
        {
            return await _contactRepository.GetContactDataById(id);
        }

        public async Task<bool> UpdateContactAsync(Contact contact)
        {
            return await _contactRepository.UpdateContactDataAsync(contact);
        }
    }
}
