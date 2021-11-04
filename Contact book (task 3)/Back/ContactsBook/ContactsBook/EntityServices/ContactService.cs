using System.Collections.Generic;
using System.Linq;
using ContactsBook.Entities;
using ContactsBook.EntityServices.Abstract;

namespace ContactsBook.EntityServices
{
    internal class ContactService : IContactService
    {
        private readonly Contact contact;

        public ContactService(Contact contact)
        {
            this.contact = contact;
        }

        public void SetPropertiesLike(Contact other)
        {
            SetName(other.Name);
            SetSurname(other.Surname);
            SetPatronymic(other.Patronymic);
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

        public void AddPhone(Phone phone)
        {
            contact.Phones.Add(phone);
        }
        
        public void AddPhones(IEnumerable<Phone> phones)
        {
            foreach (var phone in phones)
            {
                contact.Phones.Add(phone);
            }
        }

        public void SetPhones(IEnumerable<Phone> phones)
        {
            contact.Phones = phones.ToList();
        }
    }
}