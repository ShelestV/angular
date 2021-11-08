using System.Collections.Generic;
using System.Threading.Tasks;
using EF.Entities;
using EF.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EF.Providers
{
    public class PhoneProvider : BaseProvider, IPhonesProviderAsync
    {
        public async Task<IEnumerable<Phone>> GetAllAsync()
        {
            return await dbContext.Phones.ToListAsync();
        }

        public async Task AddAsync(Phone phone)
        {
            await using var context = dbContext;
            await context.Phones.AddAsync(phone);
            await context.SaveChangesAsync();
        }

        public async Task DeleteByNumberAsync(string phoneNumber)
        {
            await using var context = dbContext;
            var phone = await context.Phones.FindAsync(phoneNumber);
            context.Phones.Remove(phone);
            await context.SaveChangesAsync();
        }
    }
}