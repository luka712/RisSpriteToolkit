using System.Text.Json.Serialization;
using RisGameFramework.SpriteToolkit;

namespace RisSpriteToolkit.Dto
{
    /// <summary>
    /// The Sprite Toolkit file definition that is saved to exported format
    /// or loaded with <see cref="ISpriteTKBundleLoader"/>.
    /// </summary>
    public class SpriteToolkitBundle
    {
        /// <summary>
        /// The version of the file.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; } = "0.1.0";
        
        /// <summary>
        /// The application that created the file.
        /// </summary>
        [JsonPropertyName("app")]
        public string App { get; } = "RisSpriteToolkit";
        
        /// <summary>
        /// The list of sprite sheets.
        /// </summary>
        [JsonPropertyName("sprite_sheets")]
        public required IReadOnlyList<SpriteSheet> SpriteSheets { get; set; }
    }
}
