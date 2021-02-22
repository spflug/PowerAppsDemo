using System.Text.Json.Serialization;
using PowerApps.Interfaces;

namespace PowerApps.Models
{
    internal class PersonDto : IHttpResource
    {
        public string Name { get; init; }
        public string Height { get; init; }
        public string Mass { get; init; }
        [JsonPropertyName("hair_color")]
        public string HairColor { get; init; }
        [JsonPropertyName("skin_color")]
        public string SkinColor { get; init; }
        [JsonPropertyName("eye_color")]
        public string EyeColor { get; init; }
        [JsonPropertyName("birth_year")]
        public string BirthYear { get; init; }
        public string Gender { get; init; }
        public string Url { get; init; }
        // ReSharper disable once StringLiteralTypo
        [JsonPropertyName("homeworld")]
        public string HomeWorld { get; init; }
        public string ImageSource { get; set; }
        public string HomeWorldName { get; set; }
    }
}