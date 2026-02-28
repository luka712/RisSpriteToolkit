using System.Text.Json.Serialization;

namespace RisGameFramework.SpriteToolkit
{
    /// <summary>
    /// The sprite sheet JSON DTO.
    /// </summary>
    public record SpriteSheet
    {
        /// <summary>
        /// The name of the sprite sheet.
        /// </summary>
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        /// <summary>
        /// The file name of the sprite sheet image.
        /// </summary>
        [JsonPropertyName("file_name")]
        public required string FileName { get; set; }

        /// <summary>
        /// The file path of the sprite sheet image.
        /// </summary>
        [JsonPropertyName("file_path")]
        public string? FilePath { get; set; }

        /// <summary>
        /// The padding (in pixels) between sprites in the sprite sheet and the edges.
        /// </summary>
        [JsonPropertyName("padding")]
        public int Padding { get; set; }

        /// <summary>
        /// The image format of the sprite sheet (e.g., "png", "jpg").
        /// </summary>
        [JsonPropertyName("format")]
        public required string Format { get; set; }

        /// <summary>
        /// The collection of sprites in the sprite sheet.
        /// </summary>
        [JsonPropertyName("sprites")]
        public required IReadOnlyList<Sprite> Sprites { get; set; }
    }
}
