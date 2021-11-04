using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactsBook.Entities
{
    public class Contact : ICloneable
    {
        public static readonly Guid defaultGuid = new Guid("00000000-0000-0000-0000-000000000000");
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<Phone> Phones { get; set; }

        public Contact()
        {
            Phones = new HashSet<Phone>();
        }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj is null) return false;
            return obj is Contact contact &&
                   (Id != defaultGuid && Id == contact.Id || 
                    Name.Equals(contact.Name) && 
                    Surname.Equals(contact.Surname) && 
                    Patronymic.Equals(contact.Patronymic) ||
                    HasEqualPhones(contact));
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public object Clone()
        {
            var newContact = new Contact
            {
                Id = Id,
                Name = Name,
                Surname = Surname,
                Patronymic = Patronymic
            };
            
            foreach (var phone in Phones)
            {
                newContact.Phones.Add((Phone)phone.Clone());
            }
            
            return newContact;
        }

        public bool HasEqualPhones(Contact contact)
        {
            return Phones.Any(phone => Enumerable.Contains(contact.Phones, phone));
        }
    }
}