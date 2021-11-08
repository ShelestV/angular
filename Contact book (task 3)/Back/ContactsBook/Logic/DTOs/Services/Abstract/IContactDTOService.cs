using System.Collections.Generic;
using EF.Entities;

namespace Logic.DTOs.Services.Abstract
{
    internal interface IContactDTOService
    {
        void SetName(string name);
        void SetSurname(string surname);
        void SetPatronymic(string patronymic);
        void SetPhones(IEnumerable<Phone> phones);
        void SetPhones(IEnumerable<PhoneDTO> phones);

        void SetLike(Contact contact);
        void SetLike(ContactDTO contactDTO);
    }
}