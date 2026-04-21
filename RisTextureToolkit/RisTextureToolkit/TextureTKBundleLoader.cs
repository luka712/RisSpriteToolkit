using System.Text.Json;
using Microsoft.Extensions.Logging;
using RisTextureToolkit.Dto;

namespace RisTextureToolkit
{
    /// <summary>
    /// The implementation of <see cref="ITextureTKBundleLoader"/>.
    /// </summary>
    /// <param name="logger">
    /// The optional <see cref="ILogger"/>.
    /// </param>
    public class TextureTKBundleLoader (ILogger? logger = null) : ITextureTKBundleLoader
    {
        private readonly Dictionary<string, TextureAtlasBundle> _loadedBundles = new();

        /// <inheritdoc/>
        public event Action<TextureAtlasBundle>? OnBundleLoaded;

        /// <inheritdoc/>
        public bool UseCache { get; set; } = true;

        /// <summary>
        /// Validates the file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="ArgumentException">
        /// In case if <paramref name="filePath"/> is null or whitespace.
        /// </exception>
        protected void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                string msg = "File path cannot be null or whitespace.";
                logger?.LogError(msg);
                throw new ArgumentException(msg, nameof(filePath));
            }
        }

        /// <summary>
        /// Deserializes the JSON data.
        /// </summary>
        /// <param name="jsonData">The json data.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>The <see cref="TextureAtlasBundle"/>.</returns>
        /// <exception cref="InvalidDataException">
        /// If loaded, JSON is not valid.
        /// </exception>
        protected TextureAtlasBundle Deserialize(string jsonData, string filePath)
        {
            try
            {
                TextureAtlasBundle dto = JsonSerializer.Deserialize<TextureAtlasBundle>(jsonData)!;
                return dto;
            }
            catch (Exception ex)
            {
                string msg = $"Failed to deserialize file: {filePath}. Exception: {ex}";
                logger?.LogError(msg);
                throw new InvalidDataException(msg, ex);
            }
        }

        /// <inheritdoc/>
        public bool TryGetFromCache(string filePath, out TextureAtlasBundle? bundle)
        {
            bundle = null;
            if (UseCache && _loadedBundles.TryGetValue(filePath, out TextureAtlasBundle? cachedDto))
            {
                bundle = cachedDto;
            }
            return bundle is not null;
        }
        
        /// <summary>
        /// Reads the file asynchronously.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The file contents as string.</returns>
        protected virtual Task<string> ReadFileAsync(string filePath)
            => File.ReadAllTextAsync(filePath);

        /// <summary>
        /// Reads the file synchronously.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The file contents as string.</returns>
        protected virtual string ReadFile(string filePath)
            => File.ReadAllText(filePath);

        /// <inheritdoc/>
        public async Task<TextureAtlasBundle> LoadAsync(string filePath)
        {
            if (TryGetFromCache(filePath, out TextureAtlasBundle? cachedDto))
            {
                return cachedDto!;
            }

            ValidateFilePath(filePath);

            string jsonData = await ReadFileAsync(filePath);
            TextureAtlasBundle bundle = Deserialize(jsonData, filePath);

            if (UseCache)
            {
                _loadedBundles[filePath] = bundle;
            }

            OnBundleLoaded?.Invoke(bundle);

            return bundle;
        }

        /// <inheritdoc/>
        public TextureAtlasBundle Load(string filePath)
        {
            if (TryGetFromCache(filePath, out TextureAtlasBundle? cachedDto))
            {
                return cachedDto!;
            }

            ValidateFilePath(filePath);

            string jsonData = ReadFile(filePath);
            TextureAtlasBundle bundle = Deserialize(jsonData, filePath);

            if (UseCache)
            {
                _loadedBundles[filePath] = bundle;
            }

            OnBundleLoaded?.Invoke(bundle);

            return bundle;
        }
    }
}