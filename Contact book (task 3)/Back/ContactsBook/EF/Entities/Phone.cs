using System;

namespace EF.Entities
{
    public class Phone
    {
        public string Number { get; set; }
        public string Note { get; set; }
        
        public Contact ContactNavigation { get; set; }
        public Guid ContactId { get; set; }
    }
}