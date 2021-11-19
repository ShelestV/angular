using System;
using System.Threading.Tasks;
using ContactsBook.Models;
using ContactsBook.Repositories.Abstract;
using Logic.DTOs;
using Logic.Services;
using Logic.Services.Abstract;

namespace ContactsBook.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly IPhoneService service;

        public PhoneRepository(IPhoneService service)
        {
            this.service = service;
        }

        public async Task AddAsync(Phone phone)
        {
            if (phone is null) throw new ArgumentNullException();
            
            var phoneDTO = new PhoneDTO
            {
                Number = phone.Number,
                ContactId = phone.ContactId
            };

            await service.AddAsync(phoneDTO);
        }

        public async Task DeleteByNumberAsync(string phoneNumber)
        {
            await service.DeleteByNumberAsync(phoneNumber);
        }
    }
}