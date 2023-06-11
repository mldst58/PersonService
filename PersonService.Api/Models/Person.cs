namespace PersonService.Api.Models
{
    /// <inheritdoc />
    /// <summary>
    /// A Person.
    /// </summary>
    public class Person : NewPerson
    {
        /// <summary>
        /// The internal identifier for this Person.
        /// </summary>
        public long Id { get; set; }
       
        public static Person FromNew(long newId, NewPerson newPerson)
        {
            return new Person
            {
                Id = newId,
                AddressId = newPerson.AddressId,
                FirstName = newPerson.FirstName,
                LastName = newPerson.LastName,
                PreferredName = newPerson.PreferredName,
                Gender = newPerson.Gender,
                Deleted = newPerson.Deleted,
                DateOfBirth = newPerson.DateOfBirth,
                CreatedBy = newPerson.CreatedBy
            };
        }
    }
}