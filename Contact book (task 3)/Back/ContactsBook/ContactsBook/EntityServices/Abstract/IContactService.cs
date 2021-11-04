using System.Collections.Generic;
using ContactsBook.Entities;

namespace ContactsBook.EntityServices.Abstract
{
    internal interface IContactService
    {
        void SetPropertiesLike(Contact other);
        void SetName(string name);
        void SetSurname(string surname);
        void SetPatronymic(string patronymic);
        void AddPhone(Phone phone);
        void AddPhones(IEnumerable<Phone> phones);
        void SetPhones(IEnumerable<Phone> phones);
    }
}