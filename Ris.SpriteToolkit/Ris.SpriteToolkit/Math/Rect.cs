using System.Drawing;
using System.Text.Json.Serialization;

namespace RisGameFramework.SpriteToolkit.Math;

/// <summary>
/// The rectangle structure.
/// </summary>
public struct Rect
{
    /// <summary>
    /// Constructor for <see cref="Rect"/>.
    /// </summary>
    /// <param name="x">The x position.</param>
    /// <param name="y">The y position.</param>
    /// <param name="width">The width of rect.</param>
    /// <param name="height">The height of rect.</param>
    public Rect(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Constructor for <see cref="Rect"/>.
    /// </summary>
    /// <param name="position">The position of rect.</param>
    /// <param name="size">The size of rect.</param>
    public Rect(Point position, Size size)
    {
        X = position.X;
        Y = position.Y;
        Width = size.Width;
        Height = size.Height;
    }

    /// <summary>
    /// The location of this rect.
    /// </summary>
    [JsonIgnore]
    public Point Location => new Point(X, Y);

    /// <summary>
    /// The X position of this rect.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The Y position of this rect.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// The width of this rect.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of this rect.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// The top-left position of this rect.
    /// </summary>
    [JsonIgnore]
    public Point TopLeft => new Point(X, Y);

    /// <summary>
    /// The top-right position of this rect.
    /// </summary>
    [JsonIgnore]
    public Point TopRight => new Point(X + Width, Y);

    /// <summary>
    /// The bottom-left position of this rect.
    /// </summary>
    [JsonIgnore]
    public Point BottomLeft => new Point(X, Y + Height);

    /// <summary>
    /// The bottom-right position of this rect.
    /// </summary>
    [JsonIgnore]
    public Point BottomRight => new Point(X + Width, Y + Height);

    /// <summary>
    /// Checks intersection with another rect.
    /// </summary>
    /// <param name="rect">The other <see cref="Rect"/>.</param>
    /// <returns>
    /// <c>true</c> if intersects, <c>false</c> otherwise.
    /// </returns>
    public bool IntersectsWith(Rect rect)
        => X < rect.X + rect.Width && X + Width > rect.X &&
           Y < rect.Y + rect.Height && Y + Height > rect.Y;
}
