using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Ris.SpriteToolkit;

/// <summary>
/// The loader for sprite toolkit files.
/// </summary>
/// <param name="logger">
/// The optional <see cref="ILogger"/>.
/// </param>
public class SpriteTKBundleLoader(ILogger? logger = null) : ISpriteTKBundleLoader
{
    private readonly Dictionary<string, SpriteToolkitBundle> _loadedBundles = new();

    /// <inheritdoc />
    public event Action<SpriteToolkitBundle>? OnBundleLoaded;

    /// <inheritdoc />
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

    /// <inheritdoc />
    public bool TryGetFromCache(string filePath, out SpriteToolkitBundle? bundle)
    {
        bundle = null;
        if (UseCache && _loadedBundles.TryGetValue(filePath, out SpriteToolkitBundle? cachedDto))
        {
            bundle = cachedDto;
        }

        return bundle is not null;
    }

    /// <inheritdoc />
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
            _loadedBundles[filePath] = bundle;
        }

        OnBundleLoaded?.Invoke(bundle);

        return bundle;
    }

    /// <inheritdoc />
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
            _loadedBundles[filePath] = bundle;
        }

        OnBundleLoaded?.Invoke(bundle);

        return bundle;
    }
}