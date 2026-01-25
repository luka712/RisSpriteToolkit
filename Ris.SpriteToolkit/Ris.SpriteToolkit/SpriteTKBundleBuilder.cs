using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace RisGameFramework.SpriteToolkit;

/// <summary>
/// The asset builder which is responsible for creating and managing sprite sheets.
/// </summary>
public class SpriteTKBundleBuilder
{
    private readonly IMapper _mapper;
    private readonly ILogger? _logger;

    /// <summary>
    /// The PNG sprite sheet builder.
    /// </summary>
    public SpriteSheetBuilder PngSpriteSheetBuilder { get; }

    /// <summary>
    /// The size of the sprite sheets.
    /// </summary>
    public Size Size
    {
        get => PngSpriteSheetBuilder.Size;
        set => PngSpriteSheetBuilder.Size = value;
    }

    /// <summary>
    /// If set to <c>true</c>, allows replacing of existing assets with the same name.
    /// </summary>
    public bool AllowReplace { get; set; }

    /// <summary>
    /// The constructor for <see cref="SpriteTKBundleBuilder"/>.
    /// </summary>
    /// <param name="logger">The optional <see cref="ILogger"/>.</param>
    public SpriteTKBundleBuilder(ILogger? logger = null)
    {
        _logger = logger;
        _mapper = Mapper.MapperFactory.CreateMapper();
        PngSpriteSheetBuilder = new SpriteSheetBuilder(logger: _logger);
    }

    /// <summary>
    /// Add an image to the asset builder.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>The <see cref="BuilderSprite"/> added.</returns>
    public BuilderSprite AddImage(string filePath)
        => PngSpriteSheetBuilder.AddImage(filePath);

    /// <summary>
    /// Adds a raw image to the sprite sheets.
    /// </summary>
    /// <param name="rawImage">The <see cref="RawImage"/>.</param>
    /// <returns>The <see cref="BuilderSprite"/>.</returns>
    public BuilderSprite AddRawImage(RawImage rawImage)
        => PngSpriteSheetBuilder.AddRawImage(rawImage);

    /// <summary>
    /// Removes a sprite from the sprite sheets.
    /// </summary>
    /// <param name="sprite">The <see cref="BuilderSprite"/> to remove.</param>
    /// <returns>
    /// <c>true</c> if the sprite was removed; otherwise, <c>false</c>.
    /// </returns>
    public bool RemoveSprite(BuilderSprite sprite)
        => PngSpriteSheetBuilder.RemoveSprite(sprite);

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
        PngSpriteSheetBuilder.AddDirectoryContents(path, recursive);
    }

    /// <summary>
    /// Save the assets to the specified directory.
    /// </summary>
    /// <param name="directoryPath">
    /// The directory path where to save the assets.
    /// </param>
    /// <param name="bundleName">The name of bundle which is saved as JSON file.</param>
    public void SaveBundle(string directoryPath, string bundleName)
    {
        if (string.IsNullOrWhiteSpace(directoryPath))
        {
            throw new ArgumentException("Directory path cannot be null or whitespace.", nameof(directoryPath));
        }
        if (string.IsNullOrWhiteSpace(bundleName))
        {
            throw new ArgumentException("JSON name cannot be null or whitespace.", nameof(bundleName));
        }
        PngSpriteSheetBuilder.Save(directoryPath, out List<string> sheetsFilePaths, out List<string> sheetsFileNames);
        string jsonFilePath = Path.Combine(directoryPath, $"{bundleName}.json");

        bool fileExists = File.Exists(jsonFilePath);
        if (AllowReplace && fileExists)
        {
            File.Delete(jsonFilePath);
        }
        else if (fileExists)
        {
            string msg = $"Cannot save asset file. File already exists: {jsonFilePath}";
            _logger?.LogError(msg);
            throw new InvalidOperationException(msg);
        }

        File.WriteAllText(jsonFilePath, CreateJson(sheetsFilePaths, sheetsFileNames));
    }

    private string CreateJson(IReadOnlyList<string> sheetsFilePaths, IReadOnlyList<string> sheetsFileNames)
    {
        SpriteToolkitBundle spriteToolkitDto = new()
        {
            SpriteSheets = _mapper.Map<IReadOnlyList<SpriteSheet>>(PngSpriteSheetBuilder.SpriteSheets)
        };

        // Add file paths.
        for (int i = 0; i < sheetsFilePaths.Count; i++)
        {
            spriteToolkitDto.SpriteSheets[i].FilePath = sheetsFilePaths[i];
            spriteToolkitDto.SpriteSheets[i].FileName = sheetsFileNames[i];
        }

        return System.Text.Json.JsonSerializer.Serialize(spriteToolkitDto, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.SnakeCaseLower
        });
    }
}
