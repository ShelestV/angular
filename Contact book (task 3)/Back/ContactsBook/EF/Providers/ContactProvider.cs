using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EF.Entities;
using EF.Providers.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EF.Providers
{
    public class ContactProvider : BaseProvider, IContactsProviderAsync
    {
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            await using var context = dbContext;
            var contacts = await context.Contacts.ToListAsync();
            foreach (var contact in contacts)
            {
                contact.Phones = await context.Phones.Where(p => p.ContactId == contact.Id).ToListAsync();
            }

            return contacts;        
        }
        
        public async Task AddAsync(Contact contact)
        {
            await using var context = dbContext;
            context.Contacts.Add(contact);
            await context.SaveChangesAsync();
            
        }

        public async Task DeleteByIdAsync(Guid contactId)
        {
            await using var context = dbContext;
            var contact = await context.Contacts.FindAsync(contactId);
            context.Contacts.Remove(contact);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            await using var context = dbContext;
            var dbContact = await context.Contacts.FindAsync(contact);
            dbContact.Name = contact.Name;
            dbContact.Surname = contact.Surname;
            dbContact.Patronymic = contact.Patronymic;
            await context.SaveChangesAsync();
        }
    }
}