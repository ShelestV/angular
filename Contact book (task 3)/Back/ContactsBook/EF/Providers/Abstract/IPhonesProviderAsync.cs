using System.Collections.Generic;
using System.Threading.Tasks;
using EF.Entities;

namespace EF.Providers.Abstract
{
    public interface IPhonesProviderAsync
    {
        Task<IEnumerable<Phone>> GetAllAsync();
        Task AddAsync(Phone phone);
        Task DeleteByNumberAsync(string phoneNumber);
    }
}