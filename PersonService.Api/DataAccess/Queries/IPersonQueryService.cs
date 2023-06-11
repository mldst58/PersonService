using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonService.Api.DTOs;
using PersonService.Api.Models;

namespace PersonService.Api.DataAccess.Queries
{
    public interface IPersonQueryService
    {
        Task<Person> GetPerson(long id);

        Task<List<PersonContactDTO>> GetPersonsByState(string state);

        Task<List<PersonContactDTO>> GetAllPersons();
    }
}
