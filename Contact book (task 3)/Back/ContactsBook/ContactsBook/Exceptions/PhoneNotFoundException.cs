using System;

namespace ContactsBook.Exceptions
{
    public class PhoneNotFoundException : Exception
    {
        public PhoneNotFoundException() : base("Phone has been not found")
        {
        }
    }
}