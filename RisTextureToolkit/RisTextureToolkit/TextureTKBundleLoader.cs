using RisSpriteToolkit;

namespace RisTextureToolkit
{
    /// <summary>
    /// The implementation of <see cref="ITextureTKBundleLoader"/>.
    /// </summary>
    public class TextureTKBundleLoader : ATextureTKBundleLoader
    {
        /// <inheritdoc/>
        protected override Task<string> ReadFileAsync(string filePath)
            => File.ReadAllTextAsync(filePath);

        /// <inheritdoc/>
        protected override string ReadFile(string filePath)
            => File.ReadAllText(filePath);
    }
}