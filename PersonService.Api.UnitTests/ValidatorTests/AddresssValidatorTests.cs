using PersonService.Api.Models;
using Xunit;

namespace PersonService.Api.UnitTests.ValidatorTests
{
    public class AddressValidatorTests
    {
        private const string ValidAddress = "1333 Test Dr";
        private const string ValidAddress2 = "Cranberry";
        private const string ZipCode = "99999";
        private const string ValidCity = "Pittsburgh";
        private const string ValidState = "PA";
        private const string TooShortState = "P";
        private const string TooLongState = "PEN";
      
        [Fact]
        public void NoStateFailsValidation()
        {
            var validator = new AddressValidator();
            var address = new Address
            {
                Address = ValidAddress,
                Address2 = ValidAddress2,
                ZipCode = ZipCode,
                City = ValidCity,
                State = null
            };

            var result = validator.IsAddressValid(address);
            
            Assert.False(result);
        }
        
        [Fact]
        public void ShortStateFailsValidation()
        {
            var validator = new AddressValidator();
            var address = new Address
            {
                Address = ValidAddress,
                Address2 = ValidAddress2,
                ZipCode = ZipCode,
                City = ValidCity,
                State = TooShortState
            };

            var result = validator.IsAddressValid(address);
            
            Assert.False(result);
        }

        [Fact]
        public void LongStateFailsValidation()
        {
            var validator = new AddressValidator();
            var address = new Address
            {
                Address = ValidAddress,
                Address2 = ValidAddress2,
                ZipCode = ZipCode,
                City = ValidCity,
                State = TooLongState
            };

            var result = validator.IsAddressValid(address);
            
            Assert.False(result);
        }
        
        [Fact]
        public void AllGoodPassesValidation()
        {
            var validator = new AddressValidator();
            var address = new Address
            {
                Address = ValidAddress,
                Address2 = ValidAddress2,
                ZipCode = ZipCode,
                City = ValidCity,
                State = ValidState
            };

            var result = validator.IsAddressValid(address);

            Assert.True(result);
        }
    }
}