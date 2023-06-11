using Dapper;
using Microsoft.Extensions.Logging;
using PersonService.Api.DTOs;
using PersonService.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonService.Api.DataAccess.Queries
{
    public class PersonQueryService : IPersonQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PersonQueryService> _logger;
        private const string BaseQuery = "SELECT * FROM dbo.Person";

        public PersonQueryService(IUnitOfWork unitOfWork, ILogger<PersonQueryService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Person> GetPerson(long id)
        {
            var result = await _unitOfWork.Connection.QueryAsync<Person>(
                BaseQuery + " WHERE Id=@Id",
                new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<List<PersonContactDTO>> GetPersonsByState(string state)
        {
            var result = await _unitOfWork.Connection.QueryAsync<PersonContactDTO>(
                @"select p.Id,
                    a.AddressId,
                    p.FirstName,
                    p.LastName,
                    p.PreferredName,
                    p.Gender,
                    p.DateOfBirth,
                    a.Address,
                    a.Address2,
                    a.State
                        from Person p
                    inner 
                        join Address a on p.AddressId = a.Id
                    where a.State = @val
                    order by p.LastName",
                new { val = state });
            return result.ToList();
        }

        public async Task<List<PersonContactDTO>> GetAllPersons()
        {
            var result = await _unitOfWork.Connection.QueryAsync<PersonContactDTO>(
                @"select p.Id,
                    a.AddressId,
                    p.FirstName,
                    p.LastName,
                    p.PreferredName,
                    p.Gender,
                    p.DateOfBirth,
                    a.Address,
                    a.Address2,
                    a.State
                        from Person p
                    inner 
                        join Address a on p.AddressId = a.Id
                    where p.Deleted <> 0
                    order by p.LastName");
            return result.ToList();
        }
    }
}
