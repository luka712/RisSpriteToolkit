using SkiaSharp;

namespace RisTextureToolkit.ImageExporters
{
    /// <summary>
    /// The PNG exporter class that exports an <see cref="SKImage"/> to a PNG file.
    /// </summary>
    internal class PngExporter : IImageExporter
    {
        /// <inheritdoc/>
        public ImageFormat Format => ImageFormat.PNG;

        /// <inheritdoc/>
        public void Export(SKImage skImage, PixelFormat pixelFormat, string filePath)
        {
            using var data = skImage.Encode(SKEncodedImageFormat.Png, 100); // 100 = quality

            if(filePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) == false)
            {
                filePath += ".png";
            }

            File.WriteAllBytes(filePath, data.ToArray());
        }
    }
}
