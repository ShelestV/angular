using System.Collections.Generic;
using ContactsBook.Entities;

namespace ContactsBook.EntityServices.Abstract
{
    internal interface IPhoneService
    {
        void SetPropertiesLike(Phone other);
        void SetNote(string note);
    }
}