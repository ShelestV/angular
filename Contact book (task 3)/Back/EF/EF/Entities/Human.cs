using System;
using System.Collections.Generic;

namespace EF.Entities
{
    internal sealed class Human
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        
        public ICollection<Contact> Contacts { get; set; }

        public Human()
        {
            Contacts = new HashSet<Contact>();
        }
    }
}