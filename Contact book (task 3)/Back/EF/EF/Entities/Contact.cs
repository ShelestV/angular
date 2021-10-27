using System;

namespace EF.Entities
{
    internal sealed class Contact
    {
        public Guid HumanId { get; set; }
        public Human Human { get; set; }
        
        public string PhoneNumber { get; set; }
        public Phone Phone { get; set; }
        
        public string Note { get; set; }
    }
}