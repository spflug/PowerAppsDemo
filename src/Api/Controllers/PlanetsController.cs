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
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly ICachingClient _client;

        public PlanetsController(ICachingClient client)
        {
            _client = client;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(QueryResult<PlanetDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] string search, [FromQuery] int max = int.MaxValue)
        {
            var queryResult = await _client.Search<PlanetDto>("planets", search, max);

            return Ok(queryResult);
        }
    }
}