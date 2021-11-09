using System;
using System.Collections.Generic;
using EF.Entities;
using Logic.DTOs.Services.Abstract;

namespace Logic.DTOs.Services
{
    internal class ContactDTOService : IContactDTOService
    {
        private readonly ContactDTO contact;

        private ContactDTOService(ContactDTO contact)
        {
            this.contact = contact;
        }

        public void SetName(string name)
        {
            contact.Name = name;
        }

        public void SetSurname(string surname)
        {
            contact.Surname = surname;
        }

        public void SetPatronymic(string patronymic)
        {
            contact.Patronymic = patronymic;
        }

        public void SetPhones(IEnumerable<Phone> phones)
        {
            foreach (var phone in phones)
            {
                contact.Phones.Add(new PhoneDTO
                {
                    Number = phone.Number,
                    ContactId = phone.ContactId
                });
            }    
        }
        
        public void SetPhones(IEnumerable<PhoneDTO> phones)
        {
            foreach (var phone in phones)
            {
                contact.Phones.Add(phone);
            }
        }

        public void SetLike(Contact contact)
        {
            SetName(contact.Name);
            SetSurname(contact.Surname);
            SetPatronymic(contact.Patronymic);
            SetPhones(contact.Phones);
        }
        
        public void SetLike(ContactDTO contactDTO)
        {
            SetName(contactDTO.Name);
            SetSurname(contactDTO.Surname);
            SetPatronymic(contactDTO.Patronymic);
            SetPhones(contactDTO.Phones);
        }

        public static ContactDTO GetContactDTOFrom(Contact contact)
        {
            var service = new ContactDTOService(new ContactDTO {Id = contact.Id});
            service.SetName(contact.Name);
            service.SetSurname(contact.Surname);
            service.SetPatronymic(contact.Patronymic);
            service.SetPhones(contact.Phones);
            return service.contact;
        }

        public static Contact GetContactFromDTO(ContactDTO contactDTO)
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

        public static Contact GetContactWithNewGuidFromDTO(ContactDTO contactDTO)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
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
    }
}