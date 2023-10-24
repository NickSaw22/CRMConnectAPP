using CRMConnect.CRMConnect.Business.Interfaces;
using CRMConnect.CRMConnect.Core.Entities;
using CRMConnect.CRMConnect.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Newtonsoft.Json.Linq;

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

        public async Task<bool> UploadFileContactsAsync(IFormFile file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.MissingFieldFound = null;
            config.HeaderValidated = null;

            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))

                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<Contact>();
                    foreach (var contact in records)
                    {
                        await _contactRepository.AddContactDataAsync(contact);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<JObject> GetContactsJobWiseAsync()
        {
            var contactsList = await _contactRepository.GetAllContactsDataAsync();
            if (contactsList == null || !contactsList.Any())
            {
                return JObject.FromObject(new {});
            }
            
            var profCount = contactsList
                .GroupBy(o=>o.JobTitle)
                .Select(g => new
                {
                    name = g.Key,
                    y = g.Count()
                })
                .OrderBy(sc => sc.name)
                .ToList();

            var labels = profCount.Select(sc => sc.name.ToString()).ToArray();
            var counts = profCount.Select(sc => sc.y).ToArray();

            var result = new
            {
                label = labels,
                series = counts
            };

            return JObject.FromObject(result);
        }
    }
}
