using PersonService.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonService.Api.DataAccess.Queries
{
    public interface IAddressQueryService
    {
        Task<Address> GetAddress(long id);
        Task<List<Address>> GetAddressesByState(string state);
        Task<List<Address>> GetAllAddresses();
    }
}
