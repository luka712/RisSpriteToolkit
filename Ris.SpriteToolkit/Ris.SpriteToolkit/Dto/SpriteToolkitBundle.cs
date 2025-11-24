using System.Text.Json.Serialization;

namespace Ris.SpriteToolkit;

/// <summary>
/// The Sprite Toolkit file definition that is saved to exported format
/// or loaded with <see cref="SpriteTKBundleLoader"/>.
/// </summary>
public class SpriteToolkitBundle
{
    /// <summary>
    /// The list of sprite sheets.
    /// </summary>
    [JsonPropertyName("sprite_sheets")]
    public required IReadOnlyList<SpriteSheet> SpriteSheets { get; set; }
}
