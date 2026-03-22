// ReSharper disable All
namespace RisGameFramework.SpriteToolkit
{
    /// <summary>
    /// The supported image formats.
    /// </summary>
    public enum ImageFormats
    {
        /// <summary>
        /// Portable Network Graphics.
        /// </summary>
        PNG,

        /// <summary>
        /// Joint Photographic Experts Group.
        /// </summary>
        JPEG,

        /// <summary>
        /// Bitmap Image File.
        /// </summary>
        BMP,

        /// <summary>
        /// Graphics Interchange Format.
        /// </summary>
        GIF,

        /// <summary>
        /// Tagged Image File Format.
        /// </summary>
        TIFF,

        /// <summary>
        /// WebP Image Format.
        /// </summary>
        WEBP,

        /// <summary>
        /// The combination of all supported image formats.
        /// </summary>
        ALL = PNG | JPEG | BMP | GIF | TIFF | WEBP
    }
}
