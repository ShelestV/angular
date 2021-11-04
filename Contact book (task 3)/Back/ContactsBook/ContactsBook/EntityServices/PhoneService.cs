using ContactsBook.Entities;
using ContactsBook.EntityServices.Abstract;

namespace ContactsBook.EntityServices
{
    internal class PhoneService : IPhoneService
    {
        private readonly Phone phone;

        public PhoneService(Phone phone)
        {
            this.phone = phone;
        }

        public void SetPropertiesLike(Phone other)
        {
            SetNote(other.Note);
        }

        public void SetNote(string note)
        {
            phone.Note = note;
        }
    }
}