using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsBook.Models;
using ContactsBook.Repositories;
using Logic.DTOs;
using Logic.Services.Abstract;
using Moq;
using Xunit;

namespace ContactsBookTests.Tests.RepositoriesTests
{
    public class ContactRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_Test()
        {
            var mock = new Mock<IContactService>();
            mock.Setup(service => service.GetAllAsync()).ReturnsAsync(GetTestContacts);
            var repository = new ContactRepository(mock.Object);

            var result = await repository.GetAllAsync();
            
            Assert.IsAssignableFrom<IEnumerable<Contact>>(result);
            Assert.Equal(GetTestContacts().Count(), result.Count());
        }
        
        private IEnumerable<ContactDTO> GetTestContacts()
        {
            var id1 = new Guid("9dc90059-e8cf-4013-b550-4ee5ff78f400");
            var id2 = new Guid("87128f9f-38b8-4e2c-852b-8794c7232bc0");

            return new ContactDTO[]
            {
                new()
                {
                    Id = id1,
                    Name = "Test1",
                    Surname = "Test1",
                    Patronymic = "Test1",
                    Phones = new List<PhoneDTO>
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
                    Phones = new List<PhoneDTO>
                    {
                        new() {Number = "0000000000"}
                    }
                }
            };
        }

        [Fact]
        public async Task AddAsync_ThrowArgumentNullException_Test()
        {
            var mock = new Mock<IContactService>();
            mock.Setup(service => service.AddAsync(It.IsAny<ContactDTO>()));
            var repository = new ContactRepository(mock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.AddAsync(null));
        }
        
        [Fact]
        public async Task UpdateAsync_ThrowArgumentNullException_Test()
        {
            var mock = new Mock<IContactService>();
            mock.Setup(service => service.UpdateAsync(It.IsAny<ContactDTO>()));
            var repository = new ContactRepository(mock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.AddAsync(null));
        }
    }
}