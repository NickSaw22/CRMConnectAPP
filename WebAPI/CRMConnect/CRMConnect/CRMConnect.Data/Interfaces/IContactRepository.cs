using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Data.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContactsDataAsync();
        Task<Contact> GetContactDataById(int id);
        Task<bool> DeleteContactDataAsync(int id);
        Task<Contact> AddContactDataAsync(Contact contact);
        Task<bool> UpdateContactDataAsync(Contact contact);
    }
}
