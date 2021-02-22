using System.Text.Json.Serialization;
using PowerApps.Interfaces;

namespace PowerApps.Models
{
    internal class PlanetDto : IHttpResource
    {
        public string Name { get; init; }
        [JsonPropertyName("rotation_period")]
        public string RotationPeriod { get; init; }
        [JsonPropertyName("orbital_period")]
        public string OrbitalPeriod { get; init; }
        public string Diameter { get; init; }
        public string Climate { get; init; }
        public string Gravity { get; init; }
        public string Terrain { get; init; }
        [JsonPropertyName("surface_water")]
        public string SurfaceWater { get; init; }
        public string Population { get; init; }
        public string Url { get; init; }
    }
}