using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ris.SpriteToolkit;

/// <summary>
/// The loader for sprite toolkit files.
/// </summary>
/// <param name="logger">
/// The optional <see cref="ILogger"/>.
/// </param>
public class SpriteTKBundleLoader(ILogger? logger = null)
{
    private readonly Dictionary<string, SpriteToolkitBundle> loadedBundles = new();

    /// <summary>
    /// Is raised when a bundle is loaded.
    /// </summary>
    public event Action<SpriteToolkitBundle>? OnBundleLoaded;

    /// <summary>
    /// If <c>true</c> loaded bundles will be cached.
    /// </summary>
    public bool UseCache { get; set; } = true;

    private void ValidateFilePath(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            string msg = "File path cannot be null or whitespace.";
            logger?.LogError(msg);
            throw new ArgumentException(msg, nameof(filePath));
        }
    }

    private SpriteToolkitBundle Deserialize(string jsonData, string filePath)
    {
        try
        {
            SpriteToolkitBundle dto = JsonSerializer.Deserialize<SpriteToolkitBundle>(jsonData)!;
            return dto;
        }
        catch (Exception ex)
        {
            string msg = $"Failed to deserialize file: {filePath}. Exception: {ex}";
            logger?.LogError(msg);
            throw new InvalidDataException(msg, ex);
        }
    }

    /// <summary>
    /// Tries to get the bundle from cache.
    /// </summary>
    /// <param name="filePath">The file path of a bundle.</param>
    /// <param name="bundle">The <see cref="SpriteToolkitBundle"/> if found; otherwise <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if found in cache; otherwise, <c>false</c>.
    /// </returns>
    public bool TryGetFromCache(string filePath, out SpriteToolkitBundle? bundle)
    {
        bundle = null;
        if (UseCache && loadedBundles.TryGetValue(filePath, out SpriteToolkitBundle? cachedDto))
        {
            bundle = cachedDto;
        }
        return bundle is not null;
    }

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
    public async Task<SpriteToolkitBundle> LoadAsync(string filePath)
    {
        if (TryGetFromCache(filePath, out SpriteToolkitBundle? cachedDto))
        {
            return cachedDto!;
        }

        ValidateFilePath(filePath);

        string jsonData = await File.ReadAllTextAsync(filePath);
        SpriteToolkitBundle bundle = Deserialize(jsonData, filePath);

        if (UseCache)
        {
            loadedBundles[filePath] = bundle;
        }

        OnBundleLoaded?.Invoke(bundle);

        return bundle;
    }

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
    public SpriteToolkitBundle Load(string filePath)
    {
        if (TryGetFromCache(filePath, out SpriteToolkitBundle? cachedDto))
        {
            return cachedDto!;
        }

        ValidateFilePath(filePath);

        string jsonData = File.ReadAllText(filePath);
        SpriteToolkitBundle bundle = Deserialize(jsonData, filePath);

        if (UseCache)
        {
            loadedBundles[filePath] = bundle;
        }

        OnBundleLoaded?.Invoke(bundle);

        return bundle;
    }
}