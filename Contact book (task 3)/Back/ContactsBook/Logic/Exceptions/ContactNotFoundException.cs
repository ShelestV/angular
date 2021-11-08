using System;

namespace Logic.Exceptions
{
    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException() : base("Contact has been not found")
        {
        }
    }
}