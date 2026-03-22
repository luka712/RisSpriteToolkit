using System.Text.Json.Serialization;

namespace RisTextureToolkit.Dto
{
    /// <summary>
    /// The Sprite Toolkit file definition that is saved to exported format
    /// or loaded with <see cref="ITextureTKBundleLoader"/>.
    /// </summary>
    public class TextureAtlasBundle
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
        public string App { get; } = "RisTextureToolkit";
        
        /// <summary>
        /// The list of sprite sheets.
        /// </summary>
        [JsonPropertyName("atlases")]
        public required IReadOnlyList<TextureAtlas> Atlases { get; set; }
    }
}
