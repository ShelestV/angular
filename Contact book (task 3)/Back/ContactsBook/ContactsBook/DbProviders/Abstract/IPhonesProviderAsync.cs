using System.Threading.Tasks;
using ContactsBook.Entities;

namespace ContactsBook.DbProviders.Abstract
{
    internal interface IPhonesProviderAsync
    {
        Task AddAsync(Phone phone);
        Task DeleteByNumberAsync(string phoneNumber);
    }
}