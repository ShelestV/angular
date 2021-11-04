using System;

namespace ContactsBook.Exceptions
{
    public class ExistsContactWithThisPhoneException : Exception
    {
        public ExistsContactWithThisPhoneException() : base("It is existed contact with such phone number")
        {
        }
    }
}