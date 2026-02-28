using System.Drawing;
using RisGameFramework.SpriteToolkit;
using RisSpriteToolkit.Data.Image;
using SkiaSharp;

namespace RisSpriteToolkit.Sprite
{
    /// <summary>
    /// The sprite sheet interface.
    /// </summary>
    public interface IBuilderSpriteSheet
    {
        /// <summary>
        /// The name of the sprite sheet.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The file name of the sprite sheet image (e.g., "SpriteSheet.png").
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// The file path to the sprite sheet image (e.g., "sprites/SpriteSheet.png").
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The padding (in pixels) between sprites in the sprite sheet.
        /// By default, it is <c>1</c>.
        /// </summary>
        public int Padding { get; set; }

        /// <summary>
        /// Sprites in the sprite sheet.
        /// </summary>
        public IReadOnlyList<BuilderSprite> Sprites { get; }

        /// <summary>
        /// The size of the sprite sheet.
        /// </summary>
        public Size Size { get; }
        
        /// <summary>
        /// Checks if a <see cref="RawImage"/> can fit in the sprite sheet.
        /// </summary>
        /// <param name="image">The <see cref="RawImage"/>.</param>
        /// <param name="pixelX">
        /// Pixel X coordinate where the image can fit, or <c>-1</c> if it cannot fit.
        /// </param>
        /// <param name="pixelY">
        /// Pixel Y coordinate where the image can fit, or <c>-1</c> if it cannot fit.
        /// </param>
        /// <returns>
        /// <c>true</c> if the image can fit, <c>false</c> otherwise.
        /// </returns>
        public bool FitsInSheet(RawImage image, out int pixelX, out int pixelY);

        /// <summary>
        /// Adds a sprite to the sprite sheet from a <see cref="RawImage"/>.
        /// </summary>
        /// <param name="image">The <see cref="RawImage"/>.</param>
        /// <returns>The added <see cref="BuilderSprite"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If the image size exceeds the sprite sheet size.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If there is no space available to add the sprite.
        /// </exception>
        public BuilderSprite AddSprite(RawImage image);

        /// <summary>
        /// Tries to add a sprite to the sprite sheet from a <see cref="RawImage"/>.
        /// </summary>
        /// <param name="image">The <see cref="RawImage"/>.</param>
        /// <param name="sprite">The added <see cref="BuilderSprite"/>, or <c>null</c> if not added.</param>
        /// <returns>
        /// <c>true</c> if the sprite was added, <c>false</c> otherwise.
        /// </returns>
        public bool TryAddSprite(RawImage image, out BuilderSprite? sprite);

        /// <summary>
        /// Removes a sprite from the sprite sheet.
        /// </summary>
        /// <param name="sprite">The <see cref="BuilderSprite"/> to remove.</param>
        /// <returns>
        /// <c>true</c> if the sprite was removed, <c>false</c> otherwise.
        /// </returns>
        public bool RemoveSprite(BuilderSprite sprite);

        /// <summary>
        /// Saves the sprite sheet to a file.
        /// </summary>
        /// <param name="file">
        /// The file path where to save the sprite sheet image.
        /// </param>
        public void Save(string file);

        /// <summary>
        /// Saves the sprite sheet to as a <see cref="SKImage"/>.
        /// </summary>
        public SKImage SaveAsSkImage();
    }
}
