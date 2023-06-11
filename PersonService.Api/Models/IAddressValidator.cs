namespace PersonService.Api.Models
{
    public interface IAddressValidator
    {
        bool IsAddressValid(NewAddress address);
    }
}