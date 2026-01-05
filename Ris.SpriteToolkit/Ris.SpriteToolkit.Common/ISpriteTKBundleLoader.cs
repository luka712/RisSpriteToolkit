namespace Ris.SpriteToolkit;

/// <summary>
/// The sprite toolkit bundle loader.
/// </summary>
public interface ISpriteTKBundleLoader
{
    /// <summary>
    /// Is raised when a bundle is loaded.
    /// </summary>
    event Action<SpriteToolkitBundle>? OnBundleLoaded;
    
    /// <summary>
    /// If <c>true</c> loaded bundles will be cached.
    /// By default, it is <c>true</c>.
    /// </summary>
    bool UseCache { get; set; }

    /// <summary>
    /// Tries to get the bundle from cache.
    /// </summary>
    /// <param name="filePath">The file path of a bundle.</param>
    /// <param name="bundle">The <see cref="SpriteToolkitBundle"/> if found; otherwise <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if found in cache; otherwise, <c>false</c>.
    /// </returns>
    bool TryGetFromCache(string filePath, out SpriteToolkitBundle? bundle);

    /// <summary>
    /// Loads and imports the sprite toolkit file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>
    /// The <see cref="SpriteToolkitBundle"/>.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// In case if there is no file under <paramref name="filePath"/>.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// If loaded JSON is not valid.
    /// </exception>
    Task<SpriteToolkitBundle> LoadAsync(string filePath);

    /// <summary>
    /// Loads and imports the sprite toolkit file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>
    /// The <see cref="SpriteToolkitBundle"/>.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// In case if there is no file under <paramref name="filePath"/>.
    /// </exception>
    /// <exception cref="InvalidDataException">
    /// If loaded JSON is not valid.
    /// </exception>
    SpriteToolkitBundle Load(string filePath);
}