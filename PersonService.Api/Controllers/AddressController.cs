using Microsoft.AspNetCore.Mvc;
using PersonService.Api.DataAccess.Commands;
using PersonService.Api.DataAccess.Queries;
using PersonService.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AddressController : BaseController
    {
        private readonly IAddressValidator _addressValidator;
        private readonly IAddressQueryService _queries;
        private readonly IAddressCommandService _commands;

        public AddressController(IAddressValidator addressValidator,
            IAddressQueryService queries, IAddressCommandService commands)
        {
            _addressValidator = addressValidator;
            _queries = queries;
            _commands = commands;
        }

        /// <summary>
        /// Returns the address at the given internal identifier.
        /// </summary>
        /// <param name="id">The internal identifier of the review.</param>
        /// <returns>The Review</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Address>> GetAsync(long id)
        {
            if (id <= 0)
            {
                return BadRequest("id must be greater than 0");
            }

            var review = await _queries.GetAddress(id);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        /// <summary>
        /// Returns a list of addresses. 
        /// </summary>
        /// <returns>
        /// A collection of Restaurant objects matching the given city and state.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<List<Address>>> GetListAsync()
        {
            
            return Ok(await _queries.GetAllAddresses());
        }

        /// <summary>
        /// Adds a new address, if it does not exist.
        /// </summary>
        /// <param name="newAddress">The Address to add.</param>
        /// <returns>The Address, with the internal Id added.</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Address>> PostAsync(NewAddress newAddress)
        {
            if (!_addressValidator.IsAddressValid(newAddress))
            {
                return BadRequest("Addresses require a valid State.");
            }

            var id = await _commands.Insert(newAddress, UserId);

            return Created(nameof(GetAsync), Address.FromNew(id, newAddress));
        }

        /// <summary>
        /// Updates an address, if they exist.
        /// </summary>
        /// <param name="id">The Id of the address record to update.</param>
        /// <param name="address">The Address to add.</param>
        /// <returns>The Address, with the internal Id added.</returns>
        [HttpPut]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Address>> PutAsync(long id, Address address)
        {
            if (address == null)
            {
                return BadRequest("Address update object required");
            }

            if (!_addressValidator.IsAddressValid(address))
            {
                return BadRequest("Addresses require a valid State.");
            }

            var result = await _commands.Update(id, address, UserId);

            return Ok(result);
        }

        /// <summary>
        /// Soft deletes an address, if it exists.
        /// </summary>
        /// <param name="id">The Id of the address record to soft delete.</param>
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
