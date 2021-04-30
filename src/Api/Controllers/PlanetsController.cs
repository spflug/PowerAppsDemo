using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PowerApps.Interfaces;
using PowerApps.Models;

namespace PowerApps.Controllers
{
    /// <summary>
    /// Defines endpoints to search for starwars planet information.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly ICachingClient _client;

        /// <summary>
        /// Defines endpoint to search for starwars planet information.
        /// </summary>
        /// <param name="client">A client caching previous made request for performance improvements.</param>
        public PlanetsController(ICachingClient client)
        {
            _client = client;
        }


        /// <summary>
        /// Searches for planet information of the starwars universe
        /// </summary>
        /// <param name="search">An optional search term. When omitted, all planets are returned.</param>
        /// <param name="max">An optional limit. Limits the amount of planets returned to a maximum of the provided value.</param>
        /// <returns>All planets found in the starwars universe matching the given <paramref name="search"/> term up to a maximum number of <paramref name="max"/>.</returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(QueryResult<PlanetDto>), (int)HttpStatusCode.OK)]
        [ActionName("SearchPlanets")]
        public async Task<IActionResult> Search([FromQuery] string search, [FromQuery] int max = int.MaxValue)
        {
            var queryResult = await _client.Search<PlanetDto>("planets", search, max);

            return Ok(queryResult);
        }
    }
}