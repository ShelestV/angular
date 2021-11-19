using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsBook.Models;
using ContactsBook.Repositories.Abstract;
using Logic.DTOs;
using Logic.Services;
using Logic.Services.Abstract;

namespace ContactsBook.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactService service;

        public ContactRepository(IContactService service)
        {
            this.service = service;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            var contactsDTO = await service.GetAllAsync();

            return contactsDTO.Select(Convert).ToList();
        }

        public async Task AddAsync(Contact contact)
        {
            if (contact is null) throw new ArgumentNullException();
            await service.AddAsync(Convert(contact));
        }

        public async Task UpdateAsync(Contact contact)
        {
            if (contact is null) throw new ArgumentNullException();
            await service.UpdateAsync(Convert(contact));
        }

        private Contact Convert(ContactDTO contactDTO)
        {
            var contact = new Contact
            {
                Id = contactDTO.Id,
                Name = contactDTO.Name,
                Surname = contactDTO.Surname,
                Patronymic = contactDTO.Patronymic
            };
                    
            foreach (var phoneDTO in contactDTO.Phones)
            {
                contact.Phones.Add(new Phone
                {
                    Number = phoneDTO.Number,
                    ContactId = phoneDTO.ContactId
                });
            }

            return contact;
        }

        private ContactDTO Convert(Contact contact)
        {
            var contactDTO = new ContactDTO
            {
                Id = contact.Id,
                Name = contact.Name,
                Surname = contact.Surname,
                Patronymic = contact.Patronymic
            };
                    
            foreach (var phone in contact.Phones)
            {
                contactDTO.Phones.Add(new PhoneDTO
                {
                    Number = phone.Number,
                    ContactId = phone.ContactId
                });
            }

            return contactDTO;
        }
        
        public async Task DeleteByIdAsync(Guid id)
        {
            await service.DeleteByIdAsync(id);
        }
    }
}