using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ContactsControllerTests
    {
        [Fact]
        public async Task Get_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestContacts());
            var controller = new ContactController(mock.Object);

            var result = await controller.Get();

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Contact>>(jsonResult.Value);
            Assert.Equal(GetTestContacts().Count(), value.Count());
        }

        private IEnumerable<Contact> GetTestContacts()
        {
            var id1 = new Guid("9dc90059-e8cf-4013-b550-4ee5ff78f400");
            var id2 = new Guid("87128f9f-38b8-4e2c-852b-8794c7232bc0");

            return new Contact[]
            {
                new()
                {
                    Id = id1,
                    Name = "Test1",
                    Surname = "Test1",
                    Patronymic = "Test1",
                    Phones = new List<Phone>
                    {
                        new() {Number = "0634475396", ContactId = id1},
                        new() {Number = "0962762859", ContactId = id1}
                    }
                },
                new()
                {
                    Id = id2,
                    Name = "Test2",
                    Surname = "Test3",
                    Patronymic = "",
                    Phones = new List<Phone>
                    {
                        new() {Number = "0000000000"}
                    }
                }
            };
        }

        [Fact]
        public async Task Add_CatchContactIsExistedException_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.AddAsync(It.IsAny<Contact>())).ThrowsAsync(new ContactIsExistedException());
            var controller = new ContactController(mock.Object);

            var result = await controller.Add(It.IsAny<Contact>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact is existed", value);
        }

        [Fact]
        public async Task Add_CatchNullArgumentException_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.AddAsync(null)).ThrowsAsync(new ArgumentNullException());
            var controller = new ContactController(mock.Object);

            var result = await controller.Add(null);

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact is empty", value);
        }

        [Fact]
        public async Task Add_Successful_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.AddAsync(It.IsAny<Contact>()));
            var controller = new ContactController(mock.Object);

            var result = await controller.Add(It.IsAny<Contact>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact has been added successfully!", value);
        }

        [Fact]
        public async Task Edit_CatchContactNotFoundException_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.UpdateAsync(It.IsAny<Contact>())).ThrowsAsync(new ContactNotFoundException());
            var controller = new ContactController(mock.Object);

            var result = await controller.Edit(It.IsAny<Contact>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact has been not found", value);
        }

        [Fact]
        public async Task Edit_CatchArgumentNullException_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.UpdateAsync(It.IsAny<Contact>())).ThrowsAsync(new ArgumentNullException());
            var controller = new ContactController(mock.Object);

            var result = await controller.Edit(It.IsAny<Contact>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact is empty", value);
        }

        [Fact]
        public async Task Edit_Successful_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.UpdateAsync(It.IsAny<Contact>()));
            var controller = new ContactController(mock.Object);

            var result = await controller.Edit(It.IsAny<Contact>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact has been edited successfully!", value);
        }

        [Fact]
        public async Task Delete_CatchContactNotFoundException_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.DeleteByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new ContactNotFoundException());
            var controller = new ContactController(mock.Object);

            var result = await controller.Delete(It.IsAny<Guid>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact has been not found", value);
        }
        
        [Fact]
        public async Task Delete_Successful_Test()
        {
            var mock = new Mock<IContactRepository>();
            mock.Setup(repo => repo.DeleteByIdAsync(It.IsAny<Guid>()));
            var controller = new ContactController(mock.Object);

            var result = await controller.Delete(It.IsAny<Guid>());

            var jsonResult = Assert.IsType<JsonResult>(result);
            var value = Assert.IsAssignableFrom<string>(jsonResult.Value);
            Assert.Equal("Contact has been deleted successfully!", value);
        }
    }
}