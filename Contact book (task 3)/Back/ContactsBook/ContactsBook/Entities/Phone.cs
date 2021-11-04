using System;

namespace ContactsBook.Entities
{
    public class Phone : ICloneable
    {
        public string Number { get; set; }
        public string Note { get; set; }
        
        public Contact ContactNavigation { get; set; }
        public Guid ContactId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == this) return true;
            if (obj is null) return false;
            return obj is Phone phone &&
                   Number.Equals(phone.Number);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public object Clone()
        {
            return new Phone
            {
                Number = Number,
                Note = Note
            };
        }
    }
}