using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Data;
using EF.Entities;
using EF.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EF.Providers
{
    public class ContactProvider : BaseProvider, IContactsProviderAsync
    {
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            await using var context = GetNewContext();
            return await GetAllContactsAsync(context);        
        }

        public async Task<Contact> GetByPhoneNumberAsync(string phoneNumber)
        {
            await using var context = GetNewContext();
            var contacts = await GetAllContactsAsync(context);
            return contacts.FirstOrDefault(c => c.Phones.Any(p => phoneNumber.Equals(p.Number)));
        }

        private async Task<IEnumerable<Contact>> GetAllContactsAsync(ContactsBookDbContext context)
        {
            var contacts = await context.Contacts.ToListAsync();
            foreach (var contact in contacts)
            {
                contact.Phones = await context.Phones.Where(p => p.ContactId == contact.Id).ToListAsync();
            }

            return contacts; 
        }

        public async Task AddAsync(Contact contact)
        {
            await using var context = GetNewContext();
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
            
        }

        public async Task DeleteByIdAsync(Guid contactId)
        {
            await using var context = GetNewContext();
            var contact = await context.Contacts.FindAsync(contactId);
            context.Contacts.Remove(contact);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            await using var context = GetNewContext();
            var dbContact = await context.Contacts.FindAsync(contact.Id);
            dbContact.Name = contact.Name;
            dbContact.Surname = contact.Surname;
            dbContact.Patronymic = contact.Patronymic;
            await context.SaveChangesAsync();
        }
    }
}