using System;
using System.Threading.Tasks;
using ContactsBook.Models;
using ContactsBook.Repositories.Abstract;
using Logic.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ContactsBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository contactsRepository;
        private const string argumentNullExceptionMessage = "Contact is empty";

        public ContactController(IContactRepository repository)
        {
            contactsRepository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var contacts = await contactsRepository.GetAllAsync();
            return new JsonResult(contacts);
        }

        [HttpPost]
        public async Task<JsonResult> Add(Contact contact)
        {
            var result = "Contact has been added successfully!";

            try
            {
                await contactsRepository.AddAsync(contact);
            }
            catch (ContactIsExistedException e)
            {
                result = e.Message;
            }
            catch (ArgumentNullException)
            {
                result = argumentNullExceptionMessage;
            }

            return new JsonResult(result);
        }

        [HttpPut]
        public async Task<JsonResult> Edit(Contact contact)
        {
            var result = "Contact has been edited successfully!";

            try
            {
                await contactsRepository.UpdateAsync(contact);
            }
            catch (ContactNotFoundException e)
            {
                result = e.Message;
            }
            catch (ArgumentNullException)
            {
                result = argumentNullExceptionMessage;
            }

            return new JsonResult(result);
        }

        [HttpDelete("{contactId}")]
        public async Task<JsonResult> Delete(Guid contactId)
        {
            var result = "Contact has been deleted successfully!";

            try
            {
                await contactsRepository.DeleteByIdAsync(contactId);
            }
            catch (ContactNotFoundException e)
            {
                result = e.Message;
            }

            return new JsonResult(result);
        }

    }
}