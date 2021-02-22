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
    public class PeopleController : ControllerBase
    {
        private readonly ICachingClient _client;
        private static readonly Dictionary<string, string> ImageSources = new()
        {
            ["Ackbar"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Ackbar-icon.png",
            ["Anakin Skywalker"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Anakin-Jedi-01-icon.png",
            ["Ayla Secura"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Aayla-Secura-Jedi-icon.png",
            ["Bail Prestor Organa"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Bail-Organa-icon.png",
            ["C-3PO"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/C3PO-icon.png",
            ["Chewbacca"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Chewbacca-icon.png",
            ["Darth Maul"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Darth-Maul-01-icon.png",
            ["Darth Vader"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Vader-01-icon.png",
            ["Boba Fett"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Boba-Fett-icon.png",
            ["Dooku"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Count-Dooku-01-icon.png",
            ["Jar Jar Binks"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Jar-Jar-Binks-icon.png",
            ["Jango Fett"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Jango-Fett-icon.png",
            ["Greedo"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Greedo-icon.png",
            ["Jabba Desilijic Tiure"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Java-the-Hutt-icon.png",
            ["R2-D2"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/R2D2-01-icon.png",
            ["Obi-Wan Kenobi"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Master-Obi-Wan-icon.png",
            ["Luke Skywalker"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Luke-Skywalker-01-icon.png",
            ["Palpatine"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Senator-Palpatine-icon.png",
            ["Plo Koon"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Plo-Koon-Jedi-icon.png",
            ["Qui-Gon Jinn"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Qui-Gon-Jinn-icon.png",
            ["Padmé Amidala"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Padme-Amidala-icon.png",
            ["Leia Organa"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Leia-icon.png",
            ["Lando Calrissian"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Lando-icon.png",
            ["Nute Gunray"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Nute-Gunray-icon.png",
            ["Yoda"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Yoda-01-icon.png",
            ["Wicket Systri Warrick"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Wicket-Warrick-icon.png",
            ["Watto"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Watto-icon.png",
            ["Sebulba"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Sebulba-icon.png",
            ["Han Solo"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Han-Solo-01-icon.png",
            ["Bib Fortuna"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Bib-Fortuna-icon.png",
            ["Rugor Nass"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Boss-Nass-icon.png",
            ["Grievous"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/General-Grievous-icon.png",
            ["Roos Tarpals"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/General-Tarpals-icon.png",
            ["Ki-Adi-Mundi"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Ki-Adi-Mundi-icon.png",
            ["Mace Windu"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Mace-Windu-icon.png",
            ["Quarsh Panaka"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Panaka-icon.png",
            ["Wilhuff Tarkin"] = "https://icons.iconarchive.com/icons/jonathan-rey/star-wars-characters/128/Tarkin-icon.png",
        };

        public PeopleController(ICachingClient client)
        {
            _client = client;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(QueryResult<PersonDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] string search, [FromQuery] int max = int.MaxValue)
        {
            var peopleQuery = _client.Search<PersonDto>("people", search, max);
            var planetsQuery = _client.Search<PlanetDto>("planets", string.Empty, max);

            await Task.WhenAll(peopleQuery, planetsQuery);

            var people = await peopleQuery;
            var planets = (await planetsQuery).Results.ToDictionary(p => p.Url);
            foreach (var person in people.Results)
            {
                if (planets.TryGetValue(person.HomeWorld, out var homeWorld))
                {
                    person.HomeWorldName = homeWorld.Name;
                }

                if (ImageSources.TryGetValue(person.Name, out var img))
                {
                    person.ImageSource = img;
                }
            }

            return Ok(people);
        }
    }
}
