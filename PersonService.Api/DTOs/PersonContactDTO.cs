using System;

namespace PersonService.Api.DTOs
{
    public class PersonContactDTO
    {
        public long Id { get; set; }
        public long AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PreferredName { get; set; }
        public string Gender { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        
    }
}
