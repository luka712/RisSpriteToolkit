using RisGameFramework.SpriteToolkit;
using SkiaSharp;

namespace RisSpriteToolkit.Data.Image
{
    /// <summary>
    /// The raw image class for Skia images.
    /// </summary>
    public class RawImageSkia : RawImage
    {
        private byte[]? _bytes;

        /// <summary>
        /// The constructor to create a RawImage from an OpenCV Mat.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="skiaBitmap">The skia bitmap.</param>
        internal RawImageSkia(string filePath, SKBitmap skiaBitmap)
            : base(filePath, skiaBitmap.Width, skiaBitmap.Height, Array.Empty<byte>(), (byte)skiaBitmap.BytesPerPixel)
        {
            SkiaBitmap = skiaBitmap;
        }

        /// <summary>
        /// The underlying <see cref="SkiaBitmap"/>.
        /// </summary>
        public SKBitmap SkiaBitmap { get; }

        /// <inheritdoc/>
        public override byte[] Data
        {
            get
            {
                // Lazy load the byte array only when accessed. No need to store it twice.
                if (_bytes == null)
                {
                    // Convert Mat to a byte array
                    _bytes = new byte[SkiaBitmap.Width * SkiaBitmap.Height * 4];
                    SkiaBitmap.Bytes.CopyTo(_bytes, 0);
                }
                return _bytes;
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            SkiaBitmap.Dispose();
            base.Dispose();
        }
    }
}