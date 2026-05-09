namespace RisTextureToolkit.Loaders
{
    /// <summary>
    /// The backend used to load and decode images.
    /// </summary>
    public enum ImageBackend
    {
        /// <summary>
        /// The Skia (SkiaSharp) image backend. This is the default and currently the only supported backend.
        /// </summary>
        Skia,
    }
}
