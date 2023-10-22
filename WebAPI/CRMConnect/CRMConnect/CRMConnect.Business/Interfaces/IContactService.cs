﻿using CRMConnect.CRMConnect.Core.Entities;

namespace CRMConnect.CRMConnect.Business.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactAsync(int id);
        Task<bool> DeleteContactAsync(int id);
        Task<Contact> AddContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact);
    }
}
