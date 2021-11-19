using System.Threading.Tasks;
using ContactsBook.Models;

namespace ContactsBook.Repositories.Abstract
{
    public interface IPhoneRepository
    {
        Task AddAsync(Phone phone);
        Task DeleteByNumberAsync(string phoneNumber);
    }
}