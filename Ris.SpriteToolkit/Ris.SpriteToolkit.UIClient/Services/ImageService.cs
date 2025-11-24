
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Ris.AssetToolkit;
using Ris.AssetToolkit.Data;
using Ris.AssetToolkit.UIClient.Data;


namespace Ris.AssetToolkit.UIClient.Service;

/// <summary>
/// This class is responsible for loading images into the asset builder.
/// </summary>
/// <param name="assetBuilder">The <see cref="AssetBuilder_Old"/>.</param>
internal class ImageService(AssetBuilder assetBuilder)
{
    /// <summary>
    /// Gets or set the current folder where images sprite sheet is built to.
    /// </summary>
    public string OutputFolder { get; set; } = string.Empty;

    /// <summary>
    /// The name of the output JSON file for the sprite sheet.
    /// </summary>
    public string OutputFileName { get; set; } = "sprite_sheet";

    /// <summary>
    /// Builds the sprite sheet and saves it to the folder specified in <see cref="OutputFolder"/>.
    /// It is saved as a JSON file with the name specified in <see cref="OutputFileName"/>.
    /// </summary>
    /// <returns><c>true</c> if sprite sheet was built.</returns>
    public bool BuildSpriteSheet()
    {
        // Build the sprite sheet
        assetBuilder.Save(OutputFolder, OutputFileName);

        // TODO: Catch errors and show message to user

        return true;
    }

    /// <summary>
    /// Adds an image to the asset builder and returns a bitmap of the image.
    /// </summary>
    /// <param name="filePath">The file path of image to add.</param>
    /// <returns>The <see cref="Bitmap"/> if image was added successfully, <c>null</c> otherwise.</returns>
    public ImageData? AddImage(string filePath)
    {
        SpriteData sprite = assetBuilder.AddImage(filePath);


        // Bitmap data is created internally!
        return new ImageData(sprite);
    }

    /// <summary>
    /// Creates a <see cref="Bitmap"/> from the given <see cref="Sprite"/>.
    /// </summary>
    /// <param name="sprite">The <see cref="Sprite"/>.</param>
    /// <returns>The <see cref="Bitmap"/>.</returns>
    public static Bitmap CreateBitmap(SpriteData sprite)
    {
        WriteableBitmap bitmap = new WriteableBitmap(
        new(sprite.Size.Width, sprite.Size.Height),
        new(96, 96), // DPI
        PixelFormat.Bgra8888,
        AlphaFormat.Premul);

        int bytesPerPixel = (sprite.Size.Width * sprite.Size.Height * 4) == sprite.Data.Length ? 4 : 3;

        using (var fb = bitmap.Lock())
        {
            unsafe
            {
                byte* ptr = (byte*)fb.Address;
                for (int y = 0; y < sprite.Size.Height; y++)
                {
                    for (int x = 0; x < sprite.Size.Width; x++)
                    {
                        for (int i = 0; i < bytesPerPixel; i++)
                        {
                            int index = (y * sprite.Size.Width + x) * bytesPerPixel + i;
                            ptr[index] = sprite.Data[index];
                        }
                    }
                }
            }
        }

        return bitmap;
    }

    /// <summary>
    /// Removes an image from the asset builder.
    /// </summary>
    /// <param name="image">The <see cref="ImageData"/>.</param>
    /// <returns><c>true</c> if image is removed. <c>false</c> if it does not exists.</returns>
    public bool RemoveImage(ImageData image)
        => assetBuilder.RemoveSprite(image.Sprite);
}
