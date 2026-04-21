using RisTextureToolkit.Ktx;
using SkiaSharp;
using System.Runtime.CompilerServices;

namespace RisTextureToolkit.ImageExporters
{
    /// <summary>
    /// The KTX2 exporter class that exports an <see cref="SKImage"/> to a KTX2 file.
    /// </summary>
    internal class Ktx2Exporter : IImageExporter
    {
        /// <inheritdoc/>
        public ImageFormat Format => ImageFormat.KTX2;

        /// <inheritdoc/>
        public void Export(SKImage skImage, PixelFormat pixelFormat, string filePath)
        {
            var ktxTexture = new Ktx2Texture(new KtxTextureCreateInfo
            {
                BaseWidth = (uint)skImage.Width,
                BaseHeight = (uint)skImage.Height,
                VkFormat = VkFormat.R8G8B8A8_UNORM,
            });

            byte[] pixelData = new byte[skImage.Width * skImage.Height * 4];
            unsafe
            {
                fixed (byte* pixelPtr = pixelData)
                {
                    skImage.ReadPixels(new SKImageInfo(skImage.Width, skImage.Height, SKColorType.Rgba8888), (nint) pixelPtr);
                }
            }
            ktxTexture.SetImageFromMemory(0, 0, 0, pixelData, (uint)pixelData.Length);

            ktxTexture.CompressBasis(new KtxBasisParams
            {
                UseUastc = pixelFormat == PixelFormat.BASIS_LZ
            });

            if (!filePath.EndsWith(".ktx2", StringComparison.OrdinalIgnoreCase))
            {
                filePath += ".ktx2";
            }

            ktxTexture.WriteToNamedFile(filePath);
        }
    }
}
