using System.Collections.Generic;
using EF.Entities.Enums;

namespace EF.Entities
{
    internal sealed class Phone
    {
        public string Number { get; set; }
        public Operator Opeator { get; set; }
        
        public ICollection<Contact> Contacts { get; set; }

        public Phone()
        {
            Contacts = new HashSet<Contact>();
        }
    }
}