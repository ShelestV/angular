using System;
using System.Collections.Generic;

namespace ContactsBook.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<Phone> Phones { get; set; }

        public Contact()
        {
            Phones = new List<Phone>();
        }
    }
}