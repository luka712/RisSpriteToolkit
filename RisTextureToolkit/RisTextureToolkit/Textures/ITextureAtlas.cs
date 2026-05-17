using System.Drawing;
using RisTextureToolkit.Data.Image;
using RisTextureToolkit.ImageExporters;
using RisTextureToolkit.Textures;
using SkiaSharp;

namespace RisTextureToolkit.Textures
{
    /// <summary>
    /// The sprite sheet interface.
    /// </summary>
    public interface ITextureAtlas
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
        public IReadOnlyList<BuilderTexture> Textures { get; }

        /// <summary>
        /// The size of the sprite sheet.
        /// </summary>
        public Size Size { get; }


        // TODO: write doc comment
        public IImageExporter? GetExporter(ImageFormat imageFormat);

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
        /// <returns>The added <see cref="BuilderTexture"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If the image size exceeds the sprite sheet size.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// If there is no space available to add the sprite.
        /// </exception>
        public BuilderTexture AddSprite(RawImage image);

        /// <summary>
        /// Tries to add a sprite to the sprite sheet from a <see cref="RawImage"/>.
        /// </summary>
        /// <param name="image">The <see cref="RawImage"/>.</param>
        /// <param name="sprite">The added <see cref="BuilderTexture"/>, or <c>null</c> if not added.</param>
        /// <returns>
        /// <c>true</c> if the sprite was added, <c>false</c> otherwise.
        /// </returns>
        public bool TryAddSprite(RawImage image, out BuilderTexture? sprite);

        /// <summary>
        /// Removes a sprite from the sprite sheet.
        /// </summary>
        /// <param name="texture">The <see cref="BuilderTexture"/> to remove.</param>
        /// <returns>
        /// <c>true</c> if the sprite was removed, <c>false</c> otherwise.
        /// </returns>
        public bool RemoveSprite(BuilderTexture texture);

        /// <summary>
        /// Saves the sprite sheet to a file.
        /// </summary>
        /// <param name="file">The file path where to save the sprite sheet image without file extension.</param>
        /// <param name="imageFormat">The image format to which to save.</param>
        /// <param name="pixelFormat">The pixel format to which to save.</param>
        public void Save(string file, ImageFormat imageFormat = ImageFormat.PNG, PixelFormat pixelFormat = PixelFormat.RGBA8_UNORM);

        /// <summary>
        /// Saves the sprite sheet to as a <see cref="SKImage"/>.
        /// </summary>
        public SKImage SaveAsSkImage();
    }
}
