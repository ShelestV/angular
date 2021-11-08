using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Services.Abstract
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDTO>> GetAllAsync();
        Task AddAsync(ContactDTO contactDto);
        Task UpdateAsync(ContactDTO contactDTO);
        Task DeleteByIdAsync(Guid id);
    }
}