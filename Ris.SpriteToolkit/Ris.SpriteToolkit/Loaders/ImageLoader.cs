using OpenCvSharp;
using RisGameFramework.SpriteToolkit.Loaders;
using SkiaSharp;

namespace RisGameFramework.SpriteToolkit;

/// <summary>
/// The image loader class responsible for loading images from file paths.
/// </summary>
public class ImageLoader
{
    private readonly string[] _supportedExtensions = [".png" ];

    /// <summary>
    /// The backend for loading images.
    /// By default, it is <see cref="ImageBackend.Skia"/>.
    /// </summary>
    public ImageBackend Backend { get; set; } =  ImageBackend.Skia;

    /// <summary>
    /// Loads an image from a file path and returns a <see cref="RawImage"/>.
    /// </summary>
    /// <param name="filePath">The file path to load from.</param>
    /// <returns>
    /// The loaded <see cref="RawImage"/>.
    /// </returns>
    /// <exception cref="FileNotFoundException">
    /// If the file does not exist at the specified path.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// If the file format is not supported.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If the image could not be loaded.
    /// </exception>
    public RawImage LoadImage(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        if (!_supportedExtensions.Contains(Path.GetExtension(filePath).ToLower()))
        {
            throw new NotSupportedException($"Unsupported file format: {filePath}");
        }

        if (Backend == ImageBackend.OpenCV)
        {
            return LoadImageOpenCV(filePath);
        }
        
        // Skia is default backend.
        return LoadImageSkia(filePath);
    }

    private RawImageSkia LoadImageSkia(string filePath)
    {
        SKBitmap bitmap = SKBitmap.Decode(filePath);
        return new RawImageSkia(filePath, bitmap);
    }

    private RawImageOpenCV LoadImageOpenCV(string filePath)
    {
        // Load an image from file
        Mat img = Cv2.ImRead(filePath);
        img.ToRGBA();

        if (img.Empty())
        {
            throw new InvalidOperationException($"Failed to load image from path: {filePath}");
        }

        return new RawImageOpenCV(filePath, img);
    }

    /// <summary>
    /// Loads all images from a folder and returns a list of <see cref="RawImage"/>.
    /// </summary>
    /// <param name="folderPath">The folder path.</param>
    /// <param name="format">The <see cref="FormatType"/>.</param>
    /// <param name="filePattern">
    /// The search pattern to use when searching for images. By default, all files are searched.
    /// </param>
    /// <param name="searchOption">
    /// The search option to use when searching for images. By default, only the top directory is searched
    /// as value is set to <see cref="SearchOption.TopDirectoryOnly"/>.
    /// </param>
    /// <returns></returns>
    /// <exception cref="DirectoryNotFoundException">
    /// If the folder does not exist at the specified path.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If any image could not be loaded.
    /// </exception>
    public IReadOnlyList<RawImage> LoadFromDirectory(
        string folderPath,
        ImageFormats format = ImageFormats.ALL,
        string filePattern = "*.*",
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        if (!Directory.Exists(folderPath))
        {
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");
        }

        List<string> invalidImageFiles = new();
        List<RawImage> images = new();

        Func<string, bool> filter = format switch
        {
            ImageFormats.PNG => file => file.EndsWith(".png", StringComparison.OrdinalIgnoreCase),
            // ImageFormats.JPEG => file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase),
            // ImageFormats.BMP => file => file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase),
            // ImageFormats.GIF => file => file.EndsWith(".gif", StringComparison.OrdinalIgnoreCase),
            // ImageFormats.TIFF => file => file.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".tif", StringComparison.OrdinalIgnoreCase),
            // ImageFormats.WEBP => file => file.EndsWith(".webp", StringComparison.OrdinalIgnoreCase),
            ImageFormats.ALL => _ => true,
            _ => throw new NotSupportedException($"Unsupported image format: {format}")
        };

        string[] imageFiles = Directory.GetFiles(folderPath, "*.*", searchOption)
            .Where(filter)
            .ToArray();

        // Parallel load images
        
        // OpenCV
        if (Backend == ImageBackend.OpenCV)
        {
            Parallel.ForEach(imageFiles, file =>
            {
                Mat img = Cv2.ImRead(file, ImreadModes.Unchanged);
                img.ToRGBA();

                // If image is invalid, add to invalid list
                if (img.Empty())
                {
                    lock (invalidImageFiles) // protect shared list
                    {
                        invalidImageFiles.Add(file);
                    }
                }
                // If image is valid, add to images list
                else
                {
                    lock (images) // protect shared dictionary
                    {
                        images.Add(new RawImageOpenCV(file, img));
                    }
                }
            });
        }
        else
        {
            // SKIA
            Parallel.ForEach(imageFiles, file =>
            {
                SKBitmap bitmap = SKBitmap.Decode(file);

                // If image is invalid, add to invalid list
                if (bitmap.IsEmpty)
                {
                    lock (invalidImageFiles) // protect shared list
                    {
                        invalidImageFiles.Add(file);
                    }
                }
                // If image is valid, add to images list
                else
                {
                    lock (images) // protect shared dictionary
                    {
                        images.Add(new RawImageSkia(file, bitmap));
                    }
                }
            });
        }

        if (invalidImageFiles.Count > 0)
        {
            throw new InvalidOperationException($"Failed to load the following images: {string.Join(", ", invalidImageFiles)}");
        }


        return images;
    }
}
