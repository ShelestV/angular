using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Providers;
using EF.Providers.Abstract;
using Logic.DTOs;
using Logic.DTOs.Services;
using Logic.Exceptions;
using Logic.Services.Abstract;

namespace Logic.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactsProviderAsync dbProvider;

        public ContactService()
        {
            dbProvider = new ContactProvider();
        }

        public async Task<IEnumerable<ContactDTO>> GetAllAsync()
        {
            var dbContacts = await dbProvider.GetAllAsync();
            var contacts = new List<ContactDTO>();
            foreach (var dbContact in dbContacts)
            {
                contacts.Add(ContactDTOService.GetContactDTOFrom(dbContact));
            }

            return contacts;
        }

        public async Task AddAsync(ContactDTO contactDTO)
        {
            if (contactDTO is null) throw new ArgumentNullException();
            
            var dbContact = ContactDTOService.GetContactWithNewGuidFromDTO(contactDTO);

            if ((await dbProvider.GetAllAsync()).Any(c =>
                contactDTO.Name.Equals(c.Name) &&
                contactDTO.Surname.Equals(c.Surname) &&
                contactDTO.Patronymic.Equals(c.Patronymic)))
            {
                throw new ContactIsExistedException();
            }

            await dbProvider.AddAsync(dbContact);
        }

        public async Task UpdateAsync(ContactDTO contactDTO)
        {
            if (contactDTO is null) throw new ArgumentNullException();
            
            if (!(await dbProvider.GetAllAsync()).Select(c => c.Id).Contains(contactDTO.Id)) 
                throw new ContactNotFoundException();
            await dbProvider.UpdateAsync(ContactDTOService.GetContactFromDTO(contactDTO));
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            if (!(await dbProvider.GetAllAsync()).Select(c => c.Id).Contains(id)) 
                throw new ContactNotFoundException();
            await dbProvider.DeleteByIdAsync(id);
        }
    }
}