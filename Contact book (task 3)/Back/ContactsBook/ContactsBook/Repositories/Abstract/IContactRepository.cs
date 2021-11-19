using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsBook.Models;
using Logic.DTOs;

namespace ContactsBook.Repositories.Abstract
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteByIdAsync(Guid id);
    }
}