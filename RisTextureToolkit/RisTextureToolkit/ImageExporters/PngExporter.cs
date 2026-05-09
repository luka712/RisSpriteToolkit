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
           Export(skImage, pixelFormat, filePath, new PngExportOptions()
           {
               Quality = 100, 
           });
        }

        /// <inheritdoc/>
        public void Export(SKImage skImage, PixelFormat pixelFormat, string filePath, object options)
        {
            if (options is not PngExportOptions pngOptions)
            {
                throw new ArgumentException($"Invalid options type. Expected type {typeof(PngExportOptions)}.", nameof(options));
            }
            
            using var data = skImage.Encode(SKEncodedImageFormat.Png, pngOptions.Quality);

            if(filePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) == false)
            {
                filePath += ".png";
            }

            File.WriteAllBytes(filePath, data.ToArray());
        }
    }

    /// <summary>
    /// The PNG exporter options.
    /// </summary>
    public class PngExportOptions
    {
        /// <summary>
        /// The quality of the PNG image in the range of <c>0</c> to <c>100</c>.
        /// By default, it is <c>100</c>.
        /// </summary>
        public int Quality { get; set; } = 100;
    }
}
