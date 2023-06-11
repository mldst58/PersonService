using PersonService.Api.Models;
using System.Threading.Tasks;

namespace PersonService.Api.DataAccess.Commands
{
    public interface IPersonCommandService
    {
        Task<long> Insert(NewPerson person, string uuid);
        Task<Person> Update(long id, Person person, string uuid);
        Task<int> Delete(long id, string uuid);
    }
}
