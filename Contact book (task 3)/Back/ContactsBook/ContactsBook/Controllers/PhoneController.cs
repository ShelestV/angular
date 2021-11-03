using System;
using System.Threading.Tasks;
using ContactsBook.Data;
using ContactsBook.DbProviders;
using ContactsBook.DbProviders.Abstract;
using ContactsBook.Entities;
using ContactsBook.EntityServices;
using ContactsBook.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : Controller
    {
        private readonly ILogger<PhoneController> logger;
        private readonly IPhonesProviderAsync phonesProvider;
        private const string argumentNullExceptionMessage = "Contact is empty";

        public PhoneController(ContactsBookDbContext dbContext, ILogger<PhoneController> logger)
        {
            phonesProvider = new PhoneProvider(dbContext);
            this.logger = logger;
        }

        
        [HttpPost]
        public async Task<JsonResult> Add(Phone phone)
        {
            var result = "Phone has been added successfully!";

            try
            {
                logger.LogDebug("Add phone started");
                await phonesProvider.AddAsync(phone);
            }
            catch (ArgumentNullException)
            {
                result = argumentNullExceptionMessage;
                logger.LogError(argumentNullExceptionMessage);
            }
            catch (ExistsContactWithThisPhoneException e)
            {
                result = e.Message;
                logger.LogError(e.Message);
            }
            catch (ContactNotFoundException e)
            {
                result = e.Message;
                logger.LogError(e.Message);
            }

            return new JsonResult(result);
        }

        [HttpDelete("{phoneNumber}")]
        public async Task<JsonResult> Delete(string phoneNumber)
        {
            var result = "Phone has been deleted successfully!";

            try
            {
                logger.LogDebug("Delete phone started");
                await phonesProvider.DeleteByNumberAsync(phoneNumber);
            }
            catch (PhoneNotFoundException e)
            {
                result = e.Message;
                logger.LogError(e.Message);
            }

            return new JsonResult(result);
        }
    }
}