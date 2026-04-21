using SkiaSharp;

namespace RisTextureToolkit.ImageExporters
{
    /// <summary>
    /// The interface for image exporters that export an <see cref="SKImage"/> to a file with a specific image format and pixel format.
    /// </summary>
    internal interface IImageExporter
    {
        /// <summary>
        /// The image format of the exported image, which is PNG in this case.
        /// </summary>
        public ImageFormat Format { get; }

        /// <summary>
        /// Exports the given SKImage to the specified file path with the given pixel format.
        /// <param name="skImage">The <see cref="SKImage"/>.</param>
        /// <param name="pixelFormat">The <see cref="PixelFormat"/>.</param>
        /// <param name="filePath">The file path without or without extension.</param>
        public void Export(SKImage skImage, PixelFormat pixelFormat, string filePath);
    }
}
