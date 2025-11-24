namespace SpriteToolkit.Loaders;

/// <summary>
/// The backend used to load images.
/// </summary>
public enum ImageBackend
{
    /// <summary>
    /// The Skia image backend.
    /// </summary>
    Skia,
    
    /// <summary>
    /// The OpenCV image backend.
    /// </summary>
    OpenCV,
}