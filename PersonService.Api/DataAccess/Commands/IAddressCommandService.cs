using PersonService.Api.Models;
using System.Threading.Tasks;

namespace PersonService.Api.DataAccess.Commands
{
    public interface IAddressCommandService
    {
        Task<long> Insert(NewAddress address, string uuid);
        Task<Address> Update(long id, Address address, string uuid);
        Task<int> Delete(long id, string uuid);
    }
}
