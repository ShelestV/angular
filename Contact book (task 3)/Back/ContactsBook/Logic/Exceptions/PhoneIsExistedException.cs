using System;

namespace Logic.Exceptions
{
    public class PhoneIsExistedException : Exception
    {
        public PhoneIsExistedException() : base("Phone is existed")
        {
        }
    }
}