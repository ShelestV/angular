using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsBook.Entities;

namespace ContactsBook.DbProviders.Abstract
{
    internal interface IContactsProviderAsync
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact> GetByPhoneNumberAsync(string phoneNumber);
        Task AddAsync(Contact contact);
        Task DeleteByIdAsync(Guid contactId);
        Task UpdateAsync(Contact contact);
    }
}