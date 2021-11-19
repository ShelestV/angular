using System;
using System.Threading.Tasks;
using ContactsBook.Repositories;
using Logic.DTOs;
using Logic.Services.Abstract;
using Moq;
using Xunit;

namespace ContactsBookTests.Tests.RepositoriesTests
{
    public class PhoneRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ThrowArgumentNullException_Test()
        {
            var mock = new Mock<IPhoneService>();
            mock.Setup(service => service.AddAsync(It.IsAny<PhoneDTO>()));
            var repository = new PhoneRepository(mock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.AddAsync(null));
        }
    }
}