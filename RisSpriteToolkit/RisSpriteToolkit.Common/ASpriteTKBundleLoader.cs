using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace RisGameFramework.SpriteToolkit
{
    /// <summary>
    /// The implementation of <see cref="ISpriteTKBundleLoader"/>.
    /// </summary>
    /// <param name="logger">
    /// The optional <see cref="ILogger"/>.
    /// </param>
    public abstract class ASpriteTKBundleLoader (ILogger? logger = null) : ISpriteTKBundleLoader
    {
        private readonly Dictionary<string, SpriteToolkitBundle> loadedBundles = new();

        /// <inheritdoc/>
        public event Action<SpriteToolkitBundle>? OnBundleLoaded;

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
        /// <returns>The <see cref="SpriteToolkitBundle"/>.</returns>
        /// <exception cref="InvalidDataException">
        /// If loaded JSON is not valid.
        /// </exception>
        protected SpriteToolkitBundle Deserialize(string jsonData, string filePath)
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

        /// <inheritdoc/>
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
        /// Reads the file asynchronously.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The file contents as string.</returns>
        protected abstract Task<string> ReadFileAsync(string filePath);

        /// <summary>
        /// Reads the file synchronously.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The file contents as string.</returns>
        protected abstract string ReadFile(string filePath);

        /// <inheritdoc/>
        public async Task<SpriteToolkitBundle> LoadAsync(string filePath)
        {
            if (TryGetFromCache(filePath, out SpriteToolkitBundle? cachedDto))
            {
                return cachedDto!;
            }

            ValidateFilePath(filePath);

            string jsonData = await ReadFileAsync(filePath);
            SpriteToolkitBundle bundle = Deserialize(jsonData, filePath);

            if (UseCache)
            {
                loadedBundles[filePath] = bundle;
            }

            OnBundleLoaded?.Invoke(bundle);

            return bundle;
        }

        /// <inheritdoc/>
        public SpriteToolkitBundle Load(string filePath)
        {
            if (TryGetFromCache(filePath, out SpriteToolkitBundle? cachedDto))
            {
                return cachedDto!;
            }

            ValidateFilePath(filePath);

            string jsonData = ReadFile(filePath);
            SpriteToolkitBundle bundle = Deserialize(jsonData, filePath);

            if (UseCache)
            {
                loadedBundles[filePath] = bundle;
            }

            OnBundleLoaded?.Invoke(bundle);

            return bundle;
        }
    }
}