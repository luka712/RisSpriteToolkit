using SkiaSharp;

namespace RisGameFramework.SpriteToolkit;

/// <summary>
/// The raw image class for Skia images.
/// </summary>
public class RawImageSkia : RawImage
{
    private byte[]? bytes;

    /// <summary>
    /// The constructor to create a RawImage from an OpenCV Mat.
    /// </summary>
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
            if (bytes == null)
            {
                // Convert Mat to byte array
                bytes = new byte[SkiaBitmap.Width * SkiaBitmap.Height * 4];
                SkiaBitmap.Bytes.CopyTo(bytes, 0);
            }
            return bytes;
        }
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        SkiaBitmap.Dispose();
        base.Dispose();
    }
}