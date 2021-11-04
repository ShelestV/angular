using System;
using System.Threading.Tasks;
using ContactsBook.Data;
using ContactsBook.DbProviders.Abstract;
using ContactsBook.Entities;
using ContactsBook.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ContactsBook.DbProviders
{
    internal class PhoneProvider : BaseProvider, IPhonesProviderAsync
    {
        public PhoneProvider(ContactsBookDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddAsync(Phone phone)
        {
            if (phone is null) throw new ArgumentNullException();

            await using var context = dbContext;

            if (await context.Phones.AnyAsync(p => p.Equals(phone)))
                throw new ExistsContactWithThisPhoneException();

            var contact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == phone.ContactId);
            if (contact is null)
                throw new ContactNotFoundException();
            
            contact.Phones.Add(phone);
            context.Phones.Add(phone);

            await context.SaveChangesAsync();
        }

        public async Task DeleteByNumberAsync(string phoneNumber)
        {
            await using var context = dbContext;

            var phone = await context.Phones.FirstOrDefaultAsync(p => phoneNumber.Equals(p.Number));
            if (phone is null)
                throw new PhoneNotFoundException();

            var contact = await ContactsProvider.GetContactByPhoneNumberAsync(dbContext, phoneNumber);
            
            if (contact.Phones.Count == 1)
            {
                context.Contacts.Remove(contact);
            }
            else
            {
                contact.Phones.Remove(phone);
                
                var dbPhone = await context.Phones.FirstAsync(p => phoneNumber.Equals(p.Number));
                context.Phones.Remove(dbPhone);
            }

            await context.SaveChangesAsync();
        }
    }
}