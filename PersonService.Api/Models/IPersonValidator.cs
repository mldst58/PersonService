namespace PersonService.Api.Models
{
    public interface IPersonValidator
    {
        bool IsPersonValid(NewPerson person);
    }
}