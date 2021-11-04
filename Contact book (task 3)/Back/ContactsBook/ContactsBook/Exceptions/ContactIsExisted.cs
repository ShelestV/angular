using System;

namespace ContactsBook.Exceptions
{
    public class ContactIsExisted : Exception
    {
        public ContactIsExisted() : base("Contact is already existed")
        {
        }

        public ContactIsExisted(string message) : base(message)
        {
        }

        public ContactIsExisted(string message, Exception inner) : base(message, inner)
        {
        }
    }
}