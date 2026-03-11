using System.Drawing;
using System.Text.Json.Serialization;
using RisGameFramework.SpriteToolkit.Math;
using RisSpriteToolkit.Data.Image;

namespace RisSpriteToolkit.Sprites
{
    /// <summary>
    /// The sprite class.
    /// </summary>
    public class BuilderSprite
    {
        /// <summary>
        /// The constructor for <see cref="BuilderSprite"/>.
        /// </summary>
        /// <param name="rawImage">The <see cref="RawImage"/>.</param>
        /// <param name="position">The position in a sprite sheet.</param>
        /// <param name="spriteSheet">The sprite sheet that contains this sprite.</param>
        public BuilderSprite(RawImage rawImage, Point position, IBuilderSpriteSheet spriteSheet)
        {
            RawImage = rawImage;
            FileName = rawImage.ImageName;
            Name = Path.GetFileNameWithoutExtension(rawImage.ImageName);
            FilePath = rawImage.FilePath;
            Position = position;
            Size = new Size(rawImage.Width, rawImage.Height);
            SpriteSheet = spriteSheet;
        }

        /// <summary>
        /// The original <see cref="RawImage"/> of the sprite.
        /// </summary>
        [JsonIgnore]
        public RawImage RawImage { get; }

        /// <summary>
        /// The name of the sprite without the file extension.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The name of the sprite with the file extension.
        /// </summary>
        public string FileName { get; } = string.Empty;

        /// <summary>
        /// Sprite sheet that contains this sprite.
        /// If <c>null</c>, the sprite is not part of any sprite sheet.
        /// </summary>
        [JsonIgnore]
        public IBuilderSpriteSheet? SpriteSheet { get; }

        /// <summary>
        /// The original file path of the sprite image.
        /// </summary>
        [JsonIgnore]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// The position of the sprite in the sprite sheet (in pixels).
        /// </summary>
        [JsonIgnore]
        public Point Position { get; set; }

        /// <summary>
        /// The size of the sprite in the sprite sheet (in pixels).
        /// </summary>
        [JsonIgnore]
        public Size Size { get; set; }

        /// <summary>
        /// The raw image data of the sprite.
        /// </summary>
        [JsonIgnore]
        public byte[] Data => RawImage.Data;

        /// <summary>
        /// The bounds of the sprite in the sprite sheet.
        /// </summary>
        public Rect SourceRect => new Rect(Position, Size);

        /// <summary>
        /// Returns the U0 coordinate of the sprite.
        /// </summary>
        public float U0 => SpriteSheet is null ? 0 : (float) Position.X / (float) SpriteSheet.Size.Width;

        /// <summary>
        /// Returns the U1 coordinate of the sprite.
        /// </summary>
        public float U1 => SpriteSheet is null ? 1 : (float)(Position.X + Size.Width) / (float) SpriteSheet.Size.Width;

        /// <summary>
        /// Returns the V0 coordinate of the sprite.
        /// </summary>
        public float V0 => SpriteSheet is null ? 0 : (float)Position.Y / (float) SpriteSheet.Size.Height;

        /// <summary>
        /// Returns the V1 coordinate of the sprite.
        /// </summary>
        public float V1 => SpriteSheet is null ? 1 : (float)(Position.Y + Size.Height) / (float) SpriteSheet.Size.Height;

        /// <summary>
        /// Checks if this sprite intersects with another sprite.
        /// </summary>
        /// <param name="other">The other <see cref="BuilderSprite"/>.</param>
        /// <returns>
        /// <c>true</c> if the sprites intersect, <c>false</c> otherwise.
        /// </returns>
        public bool Intersects(BuilderSprite other)
            => SourceRect.IntersectsWith(other.SourceRect);
    }
}
