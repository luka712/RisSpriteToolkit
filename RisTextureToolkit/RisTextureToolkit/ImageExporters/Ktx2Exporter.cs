using SkiaSharp;
using RisKtx2;

namespace RisTextureToolkit.ImageExporters
{
    /// <summary>
    /// The KTX2 exporter class that exports an <see cref="SKImage"/> to a KTX2 file.
    /// </summary>
    internal class Ktx2Exporter : IImageExporter
    {
        private readonly Dictionary<PixelFormat, VkFormat> _mapFormat = new()
        {
            [PixelFormat.RGBA8_UNORM] = VkFormat.R8G8B8A8_UNORM,
            
            // Not relevant for KTX2, as these formats are transcoded by runtime.
            [PixelFormat.BASIS_ETC1S] = VkFormat.R8G8B8A8_UNORM,
            [PixelFormat.BASIS_UASTC] = VkFormat.R8G8B8A8_UNORM,
        };
        
        /// <inheritdoc/>
        public ImageFormat Format => ImageFormat.KTX2;

        /// <inheritdoc/>
        public void Export(SKImage skImage, PixelFormat pixelFormat, string filePath)
        {
            Export(skImage, pixelFormat, filePath, new Ktx2ExportOptions());
        }

        public void Export(SKImage skImage, PixelFormat pixelFormat, string filePath, object options)
        {
            if (options is not Ktx2ExportOptions ktx2Options)
            {
                throw new ArgumentException($"Invalid options type. Expected type {typeof(Ktx2ExportOptions)}.",
                    nameof(options));
            }
            
            var ktxTexture = new Ktx2Texture(new KtxTextureCreateInfo
            {
                BaseWidth = (uint)skImage.Width,
                BaseHeight = (uint)skImage.Height,
                VkFormat = _mapFormat[pixelFormat],
            });

            byte[] pixelData = new byte[skImage.Width * skImage.Height * 4];
            unsafe
            {
                fixed (byte* pixelPtr = pixelData)
                {
                    skImage.ReadPixels(new SKImageInfo(skImage.Width, skImage.Height, SKColorType.Rgba8888),
                        (nint)pixelPtr);
                }
            }

            ktxTexture.SetImageFromMemory(0, 0, 0, pixelData, (uint)pixelData.Length);

            //  Compress the image if needed.
            if (pixelFormat is PixelFormat.BASIS_UASTC or PixelFormat.BASIS_ETC1S)
            {
                var ktxParams = new KtxBasisParams()
                {
                    Uastc = pixelFormat == PixelFormat.BASIS_UASTC,
                    QualityLevel = ktx2Options.QualityLevel,
                };

                if (pixelFormat == PixelFormat.BASIS_ETC1S)
                {
                    ktxParams.ETC1SCompressionLevel = ktx2Options.ETC1SCompressionLevel;
                }

                ktxTexture.CompressBasis(ktxParams);
            }
            
            if (!filePath.EndsWith(".ktx2", StringComparison.OrdinalIgnoreCase))
            {
                filePath += ".ktx2";
            }

            ktxTexture.WriteToNamedFile(filePath);
        }
    }

    /// <summary>
    /// The options for KTX2 exporter.
    /// </summary>
    public class Ktx2ExportOptions
    {
        /// <summary>
        /// Compression quality.
        /// Range is [1,255].
        /// <see href="https://github.khronos.org/KTX-Software/libktx/structktxBasisParams.html#aac5068885c586a1454efbf2e9cf4b3ed">
        /// KTX docs.
        /// </see>
        /// </summary>
        public uint QualityLevel { get; set; } = 128;
        
        /// <summary>
        /// ETC1S compression effort levels.
        /// Range is [0,6].
        /// <see href="https://github.khronos.org/KTX-Software/libktx/structktxBasisParams.html#ac7ec144502a7f9b860bf4fdc070f95ac">
        /// KTX docs.
        /// </see>
        /// </summary>
        public uint ETC1SCompressionLevel { get; set; } = 2;
    }
}