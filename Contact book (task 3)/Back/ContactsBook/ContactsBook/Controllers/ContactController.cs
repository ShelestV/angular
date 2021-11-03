using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactsBook.Data;
using ContactsBook.DbProviders;
using ContactsBook.DbProviders.Abstract;
using ContactsBook.Entities;
using ContactsBook.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> logger;
        private readonly IContactsProviderAsync contactsProvider;
        private const string argumentNullExceptionMessage = "Contact is empty";

        public ContactController(ContactsBookDbContext dbContext, ILogger<ContactController> logger)
        {
            contactsProvider = new ContactsProvider(dbContext);
            this.logger = logger;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            logger.LogDebug("Get all contacts started");
            var contacts = await contactsProvider.GetAllAsync();
            var resultContacts = new List<Contact>();

            foreach (var contact in contacts)
            {
                resultContacts.Add((Contact)contact.Clone());
            }
            
            return new JsonResult(resultContacts);
        }

        [HttpGet("{phoneNumber}")]
        public async Task<JsonResult> Get(string phoneNumber)
        {
            logger.LogDebug("Get contact by phone number started");
            return new JsonResult(await contactsProvider.GetByPhoneNumberAsync(phoneNumber));
        }

        [HttpPost]
        public async Task<JsonResult> Add(Contact contact)
        {
            var result = "Contact has been added successfully!";

            try
            {
                logger.LogDebug("Add contact started");
                await contactsProvider.AddAsync(contact);
            }
            catch (ExistsContactWithThisPhoneException e)
            {
                result = e.Message;
                logger.LogError(e.Message);
            }
            catch (ArgumentNullException)
            {
                result = argumentNullExceptionMessage;
                logger.LogError(argumentNullExceptionMessage);
            }

            logger.LogDebug("Add contact ended");
            return new JsonResult(result);
        }

        [HttpPut]
        public async Task<JsonResult> Edit(Contact contact)
        {
            var result = "Contact has been edited successfully!";

            try
            {
                logger.LogDebug("Update contact started");
                await contactsProvider.UpdateAsync(contact);
            }
            catch (ContactNotFoundException e)
            {
                logger.LogError(e.Message);
                result = e.Message;
            }
            catch (ArgumentNullException)
            {
                logger.LogError(argumentNullExceptionMessage);
                result = argumentNullExceptionMessage;
            }

            logger.LogDebug("Update contact ended");
            return new JsonResult(result);
        }

        [HttpDelete("{contactId}")]
        public async Task<JsonResult> Delete(Guid contactId)
        {
            var result = "Contact has been deleted successfully!";

            try
            {
                logger.LogDebug("Delete contact started");
                await contactsProvider.DeleteByIdAsync(contactId);
            }
            catch (ContactNotFoundException e)
            {
                logger.LogError(e.Message);
                result = e.Message;
            }
            catch (ArgumentNullException)
            {
                logger.LogError(argumentNullExceptionMessage);
                result = argumentNullExceptionMessage;
            }

            logger.LogDebug("Delete contact ended");
            return new JsonResult(result);
        }

    }
}