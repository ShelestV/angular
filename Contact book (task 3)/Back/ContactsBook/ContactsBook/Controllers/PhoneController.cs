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
    public class PhoneController : Controller
    {
        private readonly IPhoneRepository phonesRepository;
        private const string argumentNullExceptionMessage = "Contact is empty";

        public PhoneController(IPhoneRepository repository)
        {
            phonesRepository = repository;
        }

        
        [HttpPost]
        public async Task<JsonResult> Add(Phone phone)
        {
            var result = "Phone has been added successfully!";

            try
            {
                await phonesRepository.AddAsync(phone);
            }
            catch (ArgumentNullException)
            {
                result = argumentNullExceptionMessage;
            }
            catch (PhoneIsExistedException e)
            {
                result = e.Message;
            }

            return new JsonResult(result);
        }

        [HttpDelete("{phoneNumber}")]
        public async Task<JsonResult> Delete(string phoneNumber)
        {
            var result = "Phone has been deleted successfully!";

            try
            {
                await phonesRepository.DeleteByNumberAsync(phoneNumber);
            }
            catch (PhoneNotFoundException e)
            {
                result = e.Message;
            }

            return new JsonResult(result);
        }
    }
}