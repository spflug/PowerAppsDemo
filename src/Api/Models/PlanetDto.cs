using System.Text.Json.Serialization;
using PowerApps.Interfaces;

namespace PowerApps.Models
{
    /// <summary>
    /// Attributes of starwars universe planets.
    /// </summary>
    internal class PlanetDto : IHttpResource
    {
        /// <summary>
        /// The name of this planet.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The rotation period of this planet measured in earth days.
        /// </summary>
        [JsonPropertyName("rotation_period")]
        public string RotationPeriod { get; init; }

        /// <summary>
        /// The orbital period of this planet measured in earth years.
        /// </summary>
        [JsonPropertyName("orbital_period")]
        public string OrbitalPeriod { get; init; }

        /// <summary>
        /// The diameter of this planet measure in kilometers.
        /// </summary>
        public string Diameter { get; init; }

        /// <summary>
        /// The dominating climate of this planet.
        /// </summary>
        public string Climate { get; init; }

        /// <summary>
        /// The gravity of this planet measured in standard units (1g = 1 standard).
        /// </summary>
        public string Gravity { get; init; }

        /// <summary>
        /// The dominating terrain of this planet.
        /// </summary>
        public string Terrain { get; init; }

        /// <summary>
        /// A value indicating whether this planets has surface water.
        /// </summary>
        [JsonPropertyName("surface_water")]
        public string SurfaceWater { get; init; }

        /// <summary>
        /// The population amount of this planet.
        /// </summary>
        public string Population { get; init; }

        /// <summary>
        /// A link to the specific information page of this planet.
        /// </summary>
        public string Url { get; init; }
    }
}