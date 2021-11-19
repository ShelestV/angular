using System;
using System.Threading.Tasks;
using ContactsBook.Controllers;
using ContactsBook.Models;
using ContactsBook.Repositories.Abstract;
using Logic.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContactsBookTests.Tests.ControllersTests
{
    public class PhonesControllerTests
    {
        [Fact]
        public async Task Add_CatchArgumentNullException_Test()
        {
            var mock = new Mock<IPhoneRepository>();
            mock.Setup(repo => repo.AddAsync(It.IsAny<Phone>())).ThrowsAsync(new ArgumentNullException());
            var controller = new PhoneController(mock.Object);

            var result = await controller.Add(It.IsAny<Phone>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Phone is empty", value);
        }
        
        [Fact]
        public async Task Add_CatchPhoneIsExistedException_Test()
        {
            var mock = new Mock<IPhoneRepository>();
            mock.Setup(repo => repo.AddAsync(It.IsAny<Phone>())).ThrowsAsync(new PhoneIsExistedException());
            var controller = new PhoneController(mock.Object);

            var result = await controller.Add(It.IsAny<Phone>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Phone is existed", value);
        }
        
        [Fact]
        public async Task Add_Successful_Test()
        {
            var mock = new Mock<IPhoneRepository>();
            mock.Setup(repo => repo.AddAsync(It.IsAny<Phone>()));
            var controller = new PhoneController(mock.Object);

            var result = await controller.Add(It.IsAny<Phone>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Phone has been added successfully!", value);
        }
        
        [Fact]
        public async Task Delete_CatchPhoneNotFoundException_Test()
        {
            var mock = new Mock<IPhoneRepository>();
            mock.Setup(repo => repo.DeleteByNumberAsync(It.IsAny<string>())).ThrowsAsync(new PhoneNotFoundException());
            var controller = new PhoneController(mock.Object);

            var result = await controller.Delete(It.IsAny<string>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Phone has been not found", value);
        }
        
        [Fact]
        public async Task Delete_Successful_Test()
        {
            var mock = new Mock<IPhoneRepository>();
            mock.Setup(repo => repo.DeleteByNumberAsync(It.IsAny<string>()));
            var controller = new PhoneController(mock.Object);

            var result = await controller.Delete(It.IsAny<string>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Phone has been deleted successfully!", value);
        }
    }
}