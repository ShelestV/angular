using System.Collections.Generic;
using System.Threading.Tasks;
using Logic.DTOs;

namespace Logic.Services.Abstract
{
    public interface IPhoneService
    {
        Task AddAsync(PhoneDTO phoneDTO);
        Task DeleteByNumberAsync(string phoneNumber);
    }
}