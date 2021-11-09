using System;

namespace Logic.Exceptions
{
    public class ContactIsExistedException : Exception
    {
        public ContactIsExistedException() : base("Contact is existed")
        {
        }
    }
}