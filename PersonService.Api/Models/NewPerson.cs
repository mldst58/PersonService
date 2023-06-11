using System;

namespace PersonService.Api.Models
{
    /// <summary>
    /// A new person.
    /// </summary>
    public class NewPerson
    {
        /// <summary>
        /// The identifier for the Address of the Person.
        /// </summary>
        public long AddressId { get; set; }

        /// <summary>
        /// The First Name of the Person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The Last Name of the Person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Preferred Name of the Person.
        /// </summary>
        public string PreferredName { get; set; }

        /// <summary>
        /// The Gender of the Person - based on ISO 5218.
        /// </summary>
        public byte Gender { get; set; }

        /// <summary>
        /// The deleted state.
        /// </summary>
        public byte Deleted { get; set; }

        /// <summary>
        /// The Date of Birth of the Person.
        /// </summary>
        public DateTimeOffset DateOfBirth  { get; set; }
        
        /// <summary>
        /// The Username of the creator of the Person.
        /// </summary>
        public string CreatedBy { get; set; }
    }
}