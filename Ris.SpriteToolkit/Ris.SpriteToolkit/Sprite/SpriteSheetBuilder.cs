using Microsoft.Extensions.Logging;
using RisGameFramework.SpriteToolkit.Exceptions;
using System.Drawing;
using System.Text.Json.Serialization;

namespace RisGameFramework.SpriteToolkit;

/// <summary>
/// The class responsible for creating sprite sheets.
/// </summary>
public class SpriteSheetBuilder
{
    private readonly List<IBuilderSpriteSheet> _spriteSheets = new();
    private readonly List<RawImage> _rawImages = new();

    private Size _size = new Size(2048, 2048);

    /// <summary>
    /// Event which is invoked when the <see cref="Size"/> property changes.
    /// </summary>
    public event EventHandler? SizeChanged;

    /// <summary>
    /// The constructor for <see cref="SpriteSheetBuilder"/>.
    /// </summary>
    /// <param name="size">
    /// The desired size of each sprite sheet. If <c>null</c> is passed, by default it is <c>(2048,2048)</c>.
    /// </param>
    public SpriteSheetBuilder(Size? size = null, ILogger? logger = null)
    {
        if (size.HasValue)
        {
            Size = size.Value;
        }

        Logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger.Instance;
    }

    /// <summary>
    /// If set to <c>true</c>, allows replacing of existing assets with the same name.
    /// </summary>
    public bool AllowReplace { get; set; } = false;

    /// <summary>
    /// The image loader to use for loading images.
    /// </summary>
    public ImageLoader ImageLoader { get; } = new ImageLoader();

    /// <summary>
    /// The logger to use for logging messages.
    /// </summary>
    [JsonIgnore]
    public ILogger Logger { get; set; }

    /// <summary>
    /// The image format to use when saving the sprite sheets. Default is <see cref="ImageFormats.PNG"/>.
    /// We do not currently support builder that can mix image formats.
    /// </summary>
    [JsonIgnore]
    public ImageFormats ImageFormat { get; set; } = ImageFormats.PNG;

    /// <summary>
    /// The sprite sheets created by this builder.
    /// </summary>
    public IList<IBuilderSpriteSheet> SpriteSheets => _spriteSheets;

    /// <summary>
    /// The desired size of each sprite sheet. Default is <c>(2048, 2048)</c>.
    /// </summary>
    [JsonIgnore]
    public Size Size
    {
        get => _size;
        set
        {
            if (_size != value)
            {
                _size = value;
                OnSizeChanged();
            }
        }
    }

    /// <summary>
    /// The padding (in pixels) between sprites in the sprite sheets. Default is <c>1</c>.
    /// </summary>
    [JsonIgnore]
    public int Padding { get; set; } = 1;

    /// <summary>
    /// The default name to use for sprite sheets. Default is "sprite_sheet".
    /// Note: It must be assigned before adding any sprites, as first sprite sheet will use this name.
    /// </summary>
    [JsonIgnore]
    public string DefaultSheetName { get; set; } = "sprite_sheet";

    private void OnSizeChanged()
    {
        _spriteSheets.Clear();

        // We need to copy the list to avoid modifying the collection while iterating.
        List<RawImage> rawImagesCopy = _rawImages.ToList();

        // Re-add all images to the new sprite sheets.
        _rawImages.Clear();
        foreach (RawImage rawImage in rawImagesCopy)
        {
            AddSprite(rawImage);
        }

        SizeChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Adds all images from a directory to the sprite sheets.
    /// </summary>
    /// <param name="path">
    /// The path to the directory.
    /// </param>
    /// <param name="recursive">
    /// <c>true</c> to include subdirectories; otherwise, <c>false</c>. Default is <c>false</c>.
    /// </param>
    /// <exception cref="ArgumentException">
    /// If the path is null, empty, or whitespace, or if the path does not exist.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If the image is too large to fit in a new sprite sheet.
    /// </exception>
    public void AddDirectoryContents(string path, bool recursive = false)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            string msg = "Path cannot be null or whitespace.";
            Logger.LogError(msg);
            throw new ArgumentException(msg, nameof(path));
        }

        if (!Directory.Exists(path))
        {
            string msg = "Path does not exist.";
            Logger.LogError(msg);
            throw new ArgumentException(msg, nameof(path));
        }

        Logger.LogInformation("Loading images from directory: {Path}", path);

        // Load all images from the directory
        IEnumerable<RawImage> images = ImageLoader.LoadFromDirectory(
            path,
            ImageFormats.PNG,
            searchOption: recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
            .OrderBy(img => img.ImageName); // Sort images by name for consistent ordering

        // In case if we are allowing replace, we are not adding any sprites that 
        // have the pattern name that will exists in the sheets.
        if (AllowReplace)
        {
            images = images.Where(
                x => !x.ImageName.StartsWith(DefaultSheetName, StringComparison.OrdinalIgnoreCase)
            );
        }

        foreach (RawImage image in images)
        {
            AddSprite(image);
        }
    }

    /// <summary>
    /// Loads an image from file and adds it to the sprite sheets.
    /// </summary>
    /// <param name="filePath">
    /// The file path of the image to add.
    /// </param>
    /// <returns>The <see cref="BuilderSprite"/>.</returns>
    public BuilderSprite AddImage(string filePath)
    {
        RawImage rawImage = ImageLoader.LoadImage(filePath);
        return AddSprite(rawImage);
    }

    /// <summary>
    /// Adds a raw image to the sprite sheets.
    /// </summary>
    /// <param name="rawImage">The <see cref="RawImage"/>.</param>
    /// <returns>The <see cref="BuilderSprite"/>.</returns>
    public BuilderSprite AddRawImage(RawImage rawImage)
    {
        return AddSprite(rawImage);
    }

    /// <summary>
    /// Adds a sprite to the sprite sheets, creating a new sheet if necessary.
    /// </summary>
    /// <param name="rawImage">
    /// The <see cref="RawImage"/> to add as a sprite.
    /// </param>
    /// <returns>The <see cref="BuilderSprite"/>.</returns>
    /// <exception cref="InvalidOperationException">
    /// If the image is too large to fit in a new sprite sheet.
    /// </exception>
    public BuilderSprite AddSprite(RawImage rawImage)
    {
        _rawImages.Add(rawImage);

        BuilderSprite? sprite = null;
        foreach (var sheet in _spriteSheets)
        {
            if (sheet.TryAddSprite(rawImage, out sprite))
            {
                Logger.LogInformation("Added sprite {SpriteName} to existing sheet {SheetName}", rawImage.ImageName, sheet.Name);

                // Successfully added to an existing sheet, so we can exit function.
                return sprite!;
            }
            else
            {
                Logger.LogDebug("Could not add sprite {SpriteName} to existing sheet {SheetName}, trying next sheet or creating a new one.", rawImage.ImageName, sheet.Name);
            }
        }

        // The index of a sprite sheet.
        int index = _spriteSheets.Count;
        
        // Create a new sprite sheet
        IBuilderSpriteSheet newSheet = new BuilderSkylineSpriteSheet(size: Size)
        {
            Padding = Padding,
            Name = $"{DefaultSheetName}_{index}",
        };

        Logger.LogInformation("Created new sprite sheet {SheetName} for sprite {SpriteName}", newSheet.Name, rawImage.ImageName);

        // This one will always succeed or throw.
        sprite = newSheet.AddSprite(rawImage);

        Logger.LogInformation("Added sprite {SpriteName} to new sheet {SheetName}", rawImage.ImageName, newSheet.Name);

        _spriteSheets.Add(newSheet);

        return sprite;
    }

    /// <summary>
    /// Removes a sprite from the sprite sheets.
    /// </summary>
    /// <param name="sprite">The <see cref="BuilderSprite"/> to remove.</param>
    /// <returns>
    /// <c>true</c> if the sprite was removed; otherwise, <c>false</c>.
    /// </returns>
    public bool RemoveSprite(BuilderSprite sprite)
    {
        foreach (IBuilderSpriteSheet sheet in _spriteSheets)
        {
            if (sheet.Sprites.Contains(sprite))
            {
                bool removed = sheet.RemoveSprite(sprite);
                if (removed)
                {
                    Logger.LogInformation("Removed sprite {SpriteName} from sheet {SheetName}", sprite.FileName, sheet.Name);
                    return true;
                }
            }
        }
        Logger.LogWarning("Sprite {SpriteName} not found in any sheet", sprite.FileName);
        _rawImages.Remove(sprite.RawImage);
        return false;
    }

    /// <summary>
    /// Saves all sprite sheets to the specified directory.
    /// </summary>
    /// <param name="sheetsFilePaths">List of saved file paths per sheet.</param>
    /// <param name="sheetsFileNames">List of file names per sheet.</param>
    /// <exception cref="FileAlreadyExistsException">
    /// Thrown if a file already exists and <see cref="AllowReplace"/> is <c>false</c>.
    /// </exception>
    public void Save(string directoryPath, out List<string> sheetsFilePaths, out List<string> sheetsFileNames)
    {
        sheetsFilePaths = new List<string>();
        sheetsFileNames = new List<string>();

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        int sheetIndex = 0;
        foreach (ABaseBuilderSpriteSheet sheet in _spriteSheets)
        {
            string fileName = $"{sheet.Name}_{sheetIndex}";
            sheetsFileNames.Add(fileName);
            string filePath = $"{fileName}.{ImageFormat.ToString().ToLower()}";
            sheetsFilePaths.Add(filePath);

            bool fileExists = File.Exists(filePath);
            if (AllowReplace && fileExists)
            {
                File.Delete(filePath);
            }
            else if (File.Exists(filePath))
            {
                string msg = $"Cannot save sprite sheet file. File already exists: {filePath}";
                Logger.LogError(msg);
                throw new FileAlreadyExistsException(filePath);
            }

            sheet.Save(filePath);
            sheetIndex++;
        }
    }
}
