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
    public class PhoneController : Controller
    {
        private readonly ILogger<PhoneController> logger;
        private readonly IPhoneRepository phonesRepository;
        private const string argumentNullExceptionMessage = "Contact is empty";

        public PhoneController(ILogger<PhoneController> logger)
        {
            phonesRepository = new PhoneRepository();
            this.logger = logger;
        }

        
        [HttpPost]
        public async Task<JsonResult> Add(Phone phone)
        {
            var result = "Phone has been added successfully!";

            try
            {
                logger.LogDebug("Add phone started");
                await phonesRepository.AddAsync(phone);
            }
            catch (ArgumentNullException)
            {
                result = argumentNullExceptionMessage;
                logger.LogError(argumentNullExceptionMessage);
            }
            catch (PhoneIsExistedException e)
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
                await phonesRepository.DeleteByNumberAsync(phoneNumber);
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