using System.Drawing;
using Microsoft.Extensions.Logging;
using RisGameFramework.SpriteToolkit.Exceptions;
using RisSpriteToolkit.Data.Image;
using RisTextureToolkit.Data.Image;
using RisTextureToolkit.Dto;
using RisTextureToolkit.Sprites;
using RisTextureToolkit.Textures;

namespace RisTextureToolkit
{
    /// <summary>
    /// The asset builder which is responsible for creating and managing sprite sheets.
    /// </summary>
    public class TextureTKBundleBuilder
    {
        private readonly ILogger? _logger;
        private readonly MapperService _mapper = new();
        private bool _allowReplaceTextureAtlas = false;

        /// <summary>
        /// The PNG sprite sheet builder.
        /// Builds a sprite sheet as PNG files.
        /// </summary>
        public TextureAtlasBuilder TextureAtlasBuilder { get; }

        /// <summary>
        /// The size of the sprite sheets.
        /// </summary>
        public Size Size
        {
            get => TextureAtlasBuilder.Size;
            set => TextureAtlasBuilder.Size = value;
        }

        /// <summary>
        /// If set to <c>true</c>, allows replacing of an existing JSON bundle with the same name.
        /// </summary>
        public bool AllowReplaceJsonBundle { get; set; }

        /// <summary>
        /// If set to <c>true</c>, allows replacing of an existing texture atlas with the same name.
        /// </summary>
        public bool AllowReplaceTextureAtlas
        {
            get => _allowReplaceTextureAtlas;
            set
            {
                _allowReplaceTextureAtlas = value;
                TextureAtlasBuilder.AllowReplaceTextureAtlas = value;
            }
        }

        /// <summary>
        /// The constructor for <see cref="TextureTKBundleBuilder"/>.
        /// </summary>
        /// <param name="logger">The optional <see cref="ILogger"/>.</param>
        public TextureTKBundleBuilder(ILogger? logger = null)
        {
            _logger = logger;
            TextureAtlasBuilder = new TextureAtlasBuilder(logger: _logger);
        }

        /// <summary>
        /// Add an image to the asset builder.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The <see cref="BuilderTexture"/> added.</returns>
        public BuilderTexture AddImage(string filePath)
            => TextureAtlasBuilder.AddImage(filePath);

        /// <summary>
        /// Adds a raw image to the sprite sheets.
        /// </summary>
        /// <param name="rawImage">The <see cref="RawImage"/>.</param>
        /// <returns>The <see cref="BuilderTexture"/>.</returns>
        public BuilderTexture AddRawImage(RawImage rawImage)
            => TextureAtlasBuilder.AddRawImage(rawImage);

        /// <summary>
        /// Removes a sprite from the sprite sheets.
        /// </summary>
        /// <param name="texture">The <see cref="BuilderTexture"/> to remove.</param>
        /// <returns>
        /// <c>true</c> if the sprite was removed; otherwise, <c>false</c>.
        /// </returns>
        public bool RemoveSprite(BuilderTexture texture)
            => TextureAtlasBuilder.RemoveSprite(texture);

        /// <summary>
        /// Add the contents of a directory to the asset builder.
        /// </summary>
        /// <param name="path">
        /// The path to the directory.
        /// </param>
        /// <param name="recursive">
        /// Whether to include subdirectories.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If the path is null, empty, or whitespace, or if the path does not exist.
        /// </exception>
        public void AddDirectoryContents(string path, bool recursive = false)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or whitespace.", nameof(path));
            }

            if (!Directory.Exists(path))
            {
                throw new ArgumentException("Path does not exist.", nameof(path));
            }

            // Add to the sprite sheet builder.
            TextureAtlasBuilder.AddDirectoryContents(path, recursive);
        }

        /// <summary>
        /// Save the assets to the specified directory.
        /// </summary>
        /// <param name="directoryPath">
        /// The directory path where to save the assets.
        /// </param>
        /// <param name="bundleName">The name of a bundle which is saved as JSON file.</param>
        /// <param name="imageFormat">The image format to save the sprite sheets. Default is PNG.</param>
        /// <param name="pixelFormat">The pixel format to save the sprite sheets. Default is RGBA8_UNORM.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="directoryPath"/> or <paramref name="bundleName"/> is null, empty, or whitespace.
        /// </exception>
        /// <exception cref="FileAlreadyExistsException">
        /// Thrown if the file already exists and <see cref="AllowReplace"/> is <c>false</c>.
        /// </exception>
        public void SaveBundle(string directoryPath, string bundleName,
                ImageFormat imageFormat = ImageFormat.PNG,
                PixelFormat pixelFormat = PixelFormat.RGBA8_UNORM
            )
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("Directory path cannot be null or whitespace.", nameof(directoryPath));
            }
            if (string.IsNullOrWhiteSpace(bundleName))
            {
                throw new ArgumentException("JSON name cannot be null or whitespace.", nameof(bundleName));
            }
            TextureAtlasBuilder.Save(directoryPath, imageFormat, pixelFormat, out List<string> sheetsFilePaths, out List<string> sheetsFileNames);
            string jsonFilePath = Path.Combine(directoryPath, $"{bundleName}.json");

            bool fileExists = File.Exists(jsonFilePath);
            if (AllowReplaceJsonBundle && fileExists)
            {
                File.Delete(jsonFilePath);
            }
            else if (fileExists)
            {
                _logger?.LogError($"Cannot save asset file. File already exists: {jsonFilePath}");
                throw new FileAlreadyExistsException(jsonFilePath);
            }

            File.WriteAllText(jsonFilePath, CreateJson(sheetsFilePaths, sheetsFileNames));
        }

        /// <summary>
        /// Save the assets to the specified directory asynchronously.
        /// </summary>
        /// <param name="directoryPath">
        /// The directory path where to save the assets.
        /// </param>
        /// <param name="bundleName">The name of a bundle which is saved as a JSON file.</param>
        /// <param name="imageFormat">The image format to save the sprite sheets. Default is PNG.</param>
        /// <param name="pixelFormat">The pixel format to save the sprite sheets. Default is RGBA8_UNORM.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="directoryPath"/> or <paramref name="bundleName"/> is null, empty, or whitespace.
        /// </exception>
        /// <exception cref="FileAlreadyExistsException">
        /// Thrown if the file already exists and <see cref="AllowReplace"/> is <c>false</c>.
        /// </exception>
        public async Task SaveBundleAsync(
            string directoryPath,
            string bundleName,
            ImageFormat imageFormat = ImageFormat.PNG,
            PixelFormat pixelFormat = PixelFormat.RGBA8_UNORM)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException("Directory path cannot be null or whitespace.", nameof(directoryPath));
            }
            if (string.IsNullOrWhiteSpace(bundleName))
            {
                throw new ArgumentException("JSON name cannot be null or whitespace.", nameof(bundleName));
            }

            // TODO: save async
            TextureAtlasBuilder.Save(directoryPath, imageFormat, pixelFormat, out List<string> sheetsFilePaths, out List<string> sheetsFileNames);
            string jsonFilePath = Path.Combine(directoryPath, $"{bundleName}.json");

            bool fileExists = File.Exists(jsonFilePath);
            if (AllowReplaceJsonBundle && fileExists)
            {
                File.Delete(jsonFilePath);
            }
            else if (fileExists)
            {
                _logger?.LogError($"Cannot save asset file. File already exists: {jsonFilePath}");
                throw new FileAlreadyExistsException(jsonFilePath);
            }

            await File.WriteAllTextAsync(jsonFilePath, CreateJson(sheetsFilePaths, sheetsFileNames));
        }


        private string CreateJson(IReadOnlyList<string> sheetsFilePaths, IReadOnlyList<string> sheetsFileNames)
        {

            TextureAtlasBundle textureAtlasDto = new()
            {
                Atlases = TextureAtlasBuilder.SpriteSheets.Select(x => _mapper.ToTextureAtlas(x)).ToList()
            };

            // Add file paths.
            for (int i = 0; i < sheetsFilePaths.Count; i++)
            {
                textureAtlasDto.Atlases[i].FilePath = sheetsFilePaths[i];
                textureAtlasDto.Atlases[i].FileName = sheetsFileNames[i];
            }

            return System.Text.Json.JsonSerializer.Serialize(textureAtlasDto, new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower
            });
        }
    }
}
