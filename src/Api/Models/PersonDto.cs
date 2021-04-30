using System.Text.Json.Serialization;
using PowerApps.Interfaces;

namespace PowerApps.Models
{
    /// <summary>
    /// Attributes of starwars universe characters.
    /// </summary>
    internal class PersonDto : IHttpResource
    {
        /// <summary>
        /// The name of this character.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// The height of this character measured in meters.
        /// </summary>
        public string Height { get; init; }

        /// <summary>
        /// The weight of this character measured in kilograms.
        /// </summary>
        public string Mass { get; init; }

        /// <summary>
        /// The hair color of this character.
        /// </summary>
        [JsonPropertyName("hair_color")]
        public string HairColor { get; init; }

        /// <summary>
        /// The skin color of this character.
        /// </summary>
        [JsonPropertyName("skin_color")]
        public string SkinColor { get; init; }

        /// <summary>
        /// The eye color of this character.
        /// </summary>
        [JsonPropertyName("eye_color")]
        public string EyeColor { get; init; }

        /// <summary>
        /// The year of birth of this character defined in 'before battle of yavin' units.
        /// </summary>
        [JsonPropertyName("birth_year")]
        public string BirthYear { get; init; }

        /// <summary>
        /// The gender of this character.
        /// </summary>
        public string Gender { get; init; }

        /// <summary>
        /// A link to the specific information page of this character.
        /// </summary>
        public string Url { get; init; }

        // ReSharper disable once StringLiteralTypo
        /// <summary>
        /// A link to information about the home world of this character.
        /// </summary>
        [JsonPropertyName("homeworld")]
        public string HomeWorld { get; init; }

        /// <summary>
        /// An link to an image source of this character.
        /// </summary>
        public string ImageSource { get; set; }

        /// <summary>
        /// A human readable name of the home world of this character.
        /// </summary>
        public string HomeWorldName { get; set; }
    }
}