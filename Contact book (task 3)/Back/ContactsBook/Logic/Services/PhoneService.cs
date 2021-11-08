using System;
using System.Linq;
using System.Threading.Tasks;
using EF.Entities;
using EF.Providers;
using EF.Providers.Abstract;
using Logic.DTOs;
using Logic.Exceptions;
using Logic.Services.Abstract;

namespace Logic.Services
{
    public class PhoneService : IPhoneService
    {
        private IPhonesProviderAsync dbProvider;
        
        public PhoneService()
        {
            dbProvider = new PhoneProvider();
        }
        
        public async Task AddAsync(PhoneDTO phoneDTO)
        {
            if (phoneDTO is null) throw new ArgumentNullException();
            
            if ((await dbProvider.GetAllAsync()).Any(p => phoneDTO.Number.Equals(p.Number)))
                throw new PhoneIsExistedException();
            
            var dbPhone = new Phone
            {
                Number = phoneDTO.Number, 
                ContactId = phoneDTO.ContactId
            };
            
            await dbProvider.AddAsync(dbPhone);
        }

        public async Task DeleteByNumberAsync(string phoneNumber)
        {
            if (phoneNumber is null) throw new ArgumentNullException();
            
            if ((await dbProvider.GetAllAsync()).Any(p => phoneNumber.Equals(p.Number)))
                throw new PhoneNotFoundException();
            await dbProvider.DeleteByNumberAsync(phoneNumber);
        }
    }
}