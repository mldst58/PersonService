namespace PersonService.Api.Models
{
    public class AddressValidator : IAddressValidator
    {
        public bool IsAddressValid(NewAddress address)
        {
            return address.State?.Length == 2;
        }
    }
}