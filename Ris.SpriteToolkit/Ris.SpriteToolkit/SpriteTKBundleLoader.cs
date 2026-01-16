namespace RisGameFramework.SpriteToolkit;

/// <summary>
/// The implementation of <see cref="ISpriteTKBundleLoader"/>.
/// </summary>
public class SpriteTKBundleLoader : ASpriteTKBundleLoader
{
    /// <inheritdoc/>
    protected override Task<string> ReadFileAsync(string filePath)
        => File.ReadAllTextAsync(filePath);

    /// <inheritdoc/>
    protected override string ReadFile(string filePath)
        => File.ReadAllText(filePath);
}