namespace RisSpriteToolkit.Data.Image
{
    /// <summary>
    /// The raw image data class.
    /// </summary>
    public class RawImage : IDisposable
    {
        /// <summary>
        /// The default constructor for <see cref="RawImage"/>.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <param name="data">The raw data.</param>
        /// <param name="numChannels">The number of channels.</param>
        public RawImage(string filePath, int width, int height, byte[] data, byte numChannels)
        {
            FilePath = filePath;
            ImageName = Path.GetFileName(filePath);
            Data = data;
            Width = width;
            Height = height;
            Channels = numChannels;
        }

        /// <summary>
        /// The file path of the image.
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// The file name of the image with an extension (e.g., "image.png").
        /// </summary>
        public string ImageName { get; set; } = string.Empty;

        /// <summary>
        /// The data of the image in bytes.
        /// </summary>
        public virtual byte[] Data { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// The width of the image in pixels.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the image in pixels.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The number of channels in the image.
        /// </summary>
        public byte Channels { get; set; }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            // Nothing to dispose in the base class.
        }
    }
}
