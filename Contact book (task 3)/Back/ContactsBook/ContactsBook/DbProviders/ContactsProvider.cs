using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsBook.Data;
using ContactsBook.DbProviders.Abstract;
using ContactsBook.Entities;
using ContactsBook.EntityServices;
using ContactsBook.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ContactsBook.DbProviders
{
    internal class ContactsProvider: BaseProvider, IContactsProviderAsync
    {
        public ContactsProvider(ContactsBookDbContext dbContext) : base(dbContext)
        {
        }

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

        public async Task<Contact> GetByPhoneNumberAsync(string phoneNumber)
        {
            await using var context = dbContext;
            var contact = await GetContactByPhoneNumberAsync(context, phoneNumber);
            foreach (var phone in contact.Phones)
            {
                phone.ContactNavigation = null;
            }

            return contact;
        }

        public async Task AddAsync(Contact contact)
        {
            if (contact?.Phones is null || contact.Phones.Count < 1) throw new ArgumentNullException();
            await using var context = dbContext;

            Contact dbContact = null;
            var contacts = await context.Contacts.ToListAsync();
            foreach (var c in contacts)
            {
                c.Phones = await context.Phones.Where(p => p.ContactId == c.Id).ToListAsync();
                if (contact.Equals(c)) dbContact = c;
            }
            
            if (dbContact is not null)
            {
                if (contact.HasEqualPhones(dbContact)) throw new ExistsContactWithThisPhoneException();
                
                foreach (var phone in contact.Phones)
                {
                    var dbPhone = await context.Phones.FirstOrDefaultAsync(p => phone.Equals(p));
                    if (dbPhone is null)
                    {
                        phone.ContactId = dbContact.Id;
                        await context.Phones.AddAsync(phone);
                    }
                    else
                    {
                        var phoneService = new PhoneService(dbPhone);
                        phoneService.SetPropertiesLike(phone);
                    }
                }
            }
            else
            {
                if (contact.Id == Contact.defaultGuid) contact.Id = Guid.NewGuid();
                await context.Contacts.AddAsync(contact);
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid contactId)
        {
            await using var context = dbContext;

            var contact = await context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
            if (contact is null) throw new ContactNotFoundException();
            
            context.Contacts.Remove(contact);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            if (contact is null) throw new ArgumentNullException();
            
            await using var context = dbContext;

            var dbContact = await context.Contacts.FirstOrDefaultAsync(c => contact.Equals(c));
            if (dbContact is null) throw new ContactNotFoundException();

            var contactService = new ContactService(dbContact);
            contactService.SetPropertiesLike(contact);

            await context.SaveChangesAsync();
        }
        
        public static async Task<Contact> GetContactByPhoneNumberAsync(ContactsBookDbContext context, string phoneNumber)
        {
            if (context is null || phoneNumber is null) throw new ArgumentNullException();

            var contacts = await context.Contacts.ToListAsync();
            foreach (var c in contacts)
            {
                c.Phones = await context.Phones.Where(p => p.ContactId == c.Id).ToListAsync();
            }
            
            var contact = contacts.FirstOrDefault(contact => 
                contact.Phones.Any(p => phoneNumber.Equals(p.Number)));

            if (contact is null) throw new ContactNotFoundException();

            return contact;
        }
    }
}