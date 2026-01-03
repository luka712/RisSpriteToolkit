namespace Ris.SpriteToolkit;

/// <summary>
/// The implementation of <see cref="ISpriteTKBundleLoader"/>.
/// </summary>
public class SpriteTKBundleLoader : ASpriteTKBundleLoader
{
    private readonly HttpClient _httpClient = new();

    /// <inheritdoc/>
    protected override Task<string> ReadFileAsync(string filePath)
        => _httpClient.GetStringAsync(filePath);

    /// <inheritdoc/>
    protected override string ReadFile(string filePath)
        => _httpClient.GetStringAsync(filePath).GetAwaiter().GetResult();
}