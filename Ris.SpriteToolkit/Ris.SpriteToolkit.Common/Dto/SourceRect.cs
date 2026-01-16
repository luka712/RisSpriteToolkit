using System.Text.Json.Serialization;

namespace RisGameFramework.SpriteToolkit;

/// <summary>
/// The rectangle JSON DTO.
/// </summary>
public struct SourceRect
{
    /// <summary>
    /// The X position in pixels.
    /// </summary>
    [JsonPropertyName("x")]
    public required int X { get; set; }

    /// <summary>
    /// The Y position in pixels.
    /// </summary>
    [JsonPropertyName("y")]
    public required int Y { get; set; }

    /// <summary>
    /// The width in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public required int Width { get; set; }

    /// <summary>
    /// The height in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public required int Height { get; set; }
}
