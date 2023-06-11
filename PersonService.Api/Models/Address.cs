namespace PersonService.Api.Models
{
    /// <inheritdoc />
    /// <summary>
    /// An address for a person.
    /// </summary>
    public class Address : NewAddress
    {
        /// <summary>
        /// The internal identifier for this Address.
        /// </summary>
        public long Id { get; set; }

        public static Address FromNew(long newId, NewAddress newAddress)
        {
            return new Address
            {
                Id = newId,
                Address = newAddress.Address,
                Address2 = newAddress.Address2,
                City = newAddress.City,
                State = newAddress.State,
                CreatedBy = newAddress.CreatedBy
            };
        }
    }
}