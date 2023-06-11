namespace PersonService.Api.Models
{
    /// <summary>
    /// A new Address for person services system.
    /// </summary>
    public class NewAddress
    {
        /// <summary>
        /// A line for the Address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// A second line of the Address.
        /// </summary>
        public string Address2 { get; set;}

        /// <summary>
        /// The City for the Address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The Zip Code for the Address.
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// The State for the Address.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// The Username of the creator of the Address.
        /// </summary>
        public string CreatedBy { get; set; }
    }
}