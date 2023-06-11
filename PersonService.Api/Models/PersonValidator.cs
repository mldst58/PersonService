using PersonService.Api.DataAccess.Queries;
using System;

namespace PersonService.Api.Models
{
    public class PersonValidator : IPersonValidator
    {
        private readonly IAddressQueryService _addressQuery;

        public PersonValidator(IAddressQueryService addressQuery)
        {
            _addressQuery = addressQuery;
        }
        
        public bool IsPersonValid(NewPerson person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(person.LastName))
            {
                return false;
            }

            if (person.DateOfBirth == DateTimeOffset.MinValue)
            {
                return false;
            }

            var address = _addressQuery.GetAddress(person.AddressId).Result;
            return address != null;
        }
    }
}