using System.Text.Json.Serialization;
using RisGameFramework.SpriteToolkit;

namespace RisSpriteToolkit.Dto
{
    /// <summary>
    /// The sprite JSON DTO.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// Name of the sprite.
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        /// <summary>
        /// The file name of the sprite image.
        /// </summary>
        [JsonPropertyName("file_name")]
        public required string FileName { get; set; }

        /// <summary>
        /// The source rectangle of the sprite in the sprite sheet.
        /// </summary>
        [JsonPropertyName("source_rect")]
        public required SourceRect SourceRect { get; set; }

        /// <summary>
        /// The U0 texture coordinate.
        /// </summary>
        [JsonPropertyName("u0")]
        public required float U0 { get; set; }

        /// <summary>
        /// The U1 texture coordinate.
        /// </summary>
        [JsonPropertyName("u1")]
        public required float U1 { get; set; }

        /// <summary>
        /// The V0 texture coordinate.
        /// </summary>
        [JsonPropertyName("v0")]
        public required float V0 { get; set; }

        /// <summary>
        /// The V1 texture coordinate.
        /// </summary>
        [JsonPropertyName("v1")]
        public required float V1 { get; set; }
    }
}
