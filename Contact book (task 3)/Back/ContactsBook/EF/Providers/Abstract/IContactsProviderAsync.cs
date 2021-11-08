using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EF.Entities;

namespace EF.Providers.Abstract
{
    public interface IContactsProviderAsync
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task AddAsync(Contact contact);
        Task DeleteByIdAsync(Guid contactId);
        Task UpdateAsync(Contact contact);
    }
}