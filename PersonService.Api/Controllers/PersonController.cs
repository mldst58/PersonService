using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PersonService.Api.Models;
using System.Threading.Tasks;
using PersonService.Api.DataAccess.Commands;
using PersonService.Api.DataAccess.Queries;
using PersonService.Api.DTOs;

namespace PersonService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PersonController : BaseController
    {
        private readonly IPersonValidator _personValidator;
        private readonly IPersonQueryService _queries;
        private readonly IPersonCommandService _commands;

        public PersonController(IPersonValidator personValidator, 
            IPersonQueryService queries, IPersonCommandService commands)
        {
            _personValidator = personValidator;
            _queries = queries;
            _commands = commands;
        }
        
        /// <summary>
        /// Returns the person at the given internal identifier.
        /// </summary>
        /// <param name="id">The internal identifier of the review.</param>
        /// <returns>The Review</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Models.Person>> GetAsync(long id)
        {
            if (id <= 0)
            {
                return BadRequest("id must be greater than 0");
            }
            
            var review = await _queries.GetPerson(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        /// <summary>
        /// Returns a list of persons by the state passed in. 
        /// </summary>
        /// <returns>
        /// A collection of Person objects retrieved by state.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<PersonContactDTO>>> GetListAsync(string state)
        {
            return Ok(await _queries.GetPersonsByState(state));
        }

        /// <summary>
        /// Returns a list of all non-deleted persons. 
        /// </summary>
        /// <returns>
        /// A collection of Person objects.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<PersonContactDTO>>> GetListAsync()
        {
            return Ok(await _queries.GetAllPersons());
        }

        /// <summary>
        /// Adds a new person, if they do not exist.
        /// </summary>
        /// <param name="newPerson">The Person to add.</param>
        /// <returns>The Person, with the internal Id added.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Person>> PostAsync(NewPerson newPerson)
        {
            if (!_personValidator.IsPersonValid(newPerson))
            {
                return BadRequest("Addresses require a valid State.");
            }

            var id = await _commands.Insert(newPerson, UserId);

            return Created(nameof(GetAsync), Person.FromNew(id, newPerson));
        }

        /// <summary>
        /// Updates a person, if they exist.
        /// </summary>
        /// <param name="id">The Id of the person record to update.</param>
        /// <param name="person">The Person to add.</param>
        /// <returns>The Person, with the internal Id added.</returns>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Person>> PutAsync(long id, Person person)
        {
            if (person == null)
            {
                return BadRequest("Person update object required");
            }

            if (!_personValidator.IsPersonValid(person))
            {
                return BadRequest("Persons require a name, DOB, and valid state.");
            }

            var result = await _commands.Update(id, person, UserId);

            return Ok(result);
        }

        /// <summary>
        /// Soft deletes a person, if they exist.
        /// </summary>
        /// <param name="id">The Id of the person record to soft delete.</param>
        /// <returns>The row count which should be 0.</returns>
        [HttpDelete]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<int>> DeleteAsync(long id)
        {
            var result = await _commands.Delete(id, UserId);

            return Ok(result);
        }
    }
}