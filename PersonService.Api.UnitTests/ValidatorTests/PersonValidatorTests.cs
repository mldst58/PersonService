using Moq;
using PersonService.Api.DataAccess.Queries;
using PersonService.Api.Models;
using System;
using System.Threading.Tasks;
using PersonService.Api.UnitTests.ControllerTests;
using Xunit;

namespace PersonService.Api.UnitTests.ValidatorTests
{
    public class PersonValidatorTests
    {
        [Fact]
        public void NoFirstNameFailsValidation()
        {
            var validator = new PersonValidator(null);
            var person = new NewPerson
            {
                FirstName = null
            };
            
            var result = validator.IsPersonValid(person);
            
            Assert.False(result);
        }

        [Fact]
        public void NoLastNameFailsValidation()
        {
            var validator = new PersonValidator(null);
            var person = new NewPerson
            {
                LastName = null
            };

            var result = validator.IsPersonValid(person);

            Assert.False(result);
        }

        [Fact]
        public void NoBirthDateFailsValidation()
        {
            var validator = new PersonValidator(null);
            var person = new NewPerson
            {
                DateOfBirth = DateTimeOffset.MinValue
            };

            var result = validator.IsPersonValid(person);

            Assert.False(result);
        }
        
        [Fact]
        public void AddressNotFoundFailsValidation()
        {
            var addressQueryMock = new Mock<IAddressQueryService>();
            addressQueryMock.Setup(q => q.GetAddress(999)).
                Returns(Task.FromResult<Address>(null));
            var validator = new PersonValidator(addressQueryMock.Object);
            var address = new Person
            {
               FirstName = "Megan",
               LastName = "Chipps",
               PreferredName = "Megan Chipps",
               Gender = 2,
               DateOfBirth = new DateTimeOffset(1980, 5, 1, 8, 6, 32,
                   new TimeSpan(1, 0, 0)),
               AddressId = 999

            };
            
            var result = validator.IsPersonValid(address);
            
            Assert.False(result);
        }
        
        [Fact]
        public void AddressFoundPassesValidation()
        {
            var data = TestData.TestDriveAddress;
            var addressQueryMock = new Mock<IAddressQueryService>();
            addressQueryMock.Setup(q => q.GetAddress(1)).
                Returns(Task.FromResult(data));
            var validator = new PersonValidator(addressQueryMock.Object);
            var person = new Person
            {
                FirstName = "Megan",
                LastName = "Chipps",
                PreferredName = "Megan Chipps",
                Gender = 2,
                DateOfBirth = new DateTimeOffset(1980, 5, 1, 8, 6, 32,
                    new TimeSpan(1, 0, 0)),
                AddressId = 1
            };

            var result = validator.IsPersonValid(person);

            Assert.True(result);
        }
        
    }
}