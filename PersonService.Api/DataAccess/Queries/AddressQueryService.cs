using Dapper;
using Microsoft.Extensions.Logging;
using PersonService.Api.DataAccess.Commands;
using PersonService.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonService.Api.DataAccess.Queries
{
    public class AddressQueryService : IAddressQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AddressCommandService> _logger;
        private const string BaseQuery = "SELECT * FROM dbo.Address";

        public AddressQueryService(IUnitOfWork unitOfWork, ILogger<AddressCommandService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Address> GetAddress(long id)
        {
            var result = await _unitOfWork.Connection.QueryAsync<Address>(
                BaseQuery + " WHERE Id=@Id",
                new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<List<Address>> GetAddressesByState(string state)
        {
            var result = await _unitOfWork.Connection.QueryAsync<Address>(
                BaseQuery + " WHERE State=@state",
                new { state = state });
            return result.ToList();
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            var result = await _unitOfWork.Connection.QueryAsync<Address>(
                BaseQuery + " ORDER BY State");
            return result.ToList();
        }
    }
}
