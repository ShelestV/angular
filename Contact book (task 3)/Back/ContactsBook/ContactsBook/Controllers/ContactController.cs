using System;
using System.Threading.Tasks;
using ContactsBook.Models;
using ContactsBook.Repositories;
using ContactsBook.Repositories.Abstract;
using Logic.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> logger;
        private readonly IContactRepository contactsRepository;
        private const string argumentNullExceptionMessage = "Contact is empty";

        public ContactController(ILogger<ContactController> logger)
        {
            contactsRepository = new ContactRepository();
            this.logger = logger;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            logger.LogDebug("Get all contacts started");
            var contacts = await contactsRepository.GetAllAsync();
            return new JsonResult(contacts);
        }

        [HttpPost]
        public async Task<JsonResult> Add(Contact contact)
        {
            var result = "Contact has been added successfully!";

            try
            {
                logger.LogDebug("Add contact started");
                await contactsRepository.AddAsync(contact);
            }
            catch (ContactIsExistedException e)
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
                await contactsRepository.UpdateAsync(contact);
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
                await contactsRepository.DeleteByIdAsync(contactId);
            }
            catch (ContactNotFoundException e)
            {
                logger.LogError(e.Message);
                result = e.Message;
            }

            logger.LogDebug("Delete contact ended");
            return new JsonResult(result);
        }

    }
}