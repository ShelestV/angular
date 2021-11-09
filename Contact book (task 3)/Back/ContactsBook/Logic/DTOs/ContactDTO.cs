using System;
using System.Collections.Generic;

namespace Logic.DTOs
{
    public class ContactDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<PhoneDTO> Phones { get; set; }

        public ContactDTO()
        {
            Phones = new List<PhoneDTO>();
        }
    }
}