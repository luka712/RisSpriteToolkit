using Ris.AssetToolkit.UIClient.Service;
using Avalonia.Media.Imaging;
using Ris.AssetToolkit.Data;

namespace Ris.AssetToolkit.UIClient.Data;

/// <summary>
/// The data class for an image.
/// </summary>
public class ImageData
{
    /// <summary>
    /// The default constructor for <see cref="ImageData"/>.
    /// </summary>
    /// <param name="sprite">The <see cref="Sprite"/>.</param>
    public ImageData(SpriteData sprite)
    {
        Sprite = sprite;
        Name = sprite.SpriteName;
        Bitmap = ImageService.CreateBitmap(sprite);
    }

    /// <summary>
    /// The name of the image.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The <see cref="Bitmap"/> of the image.
    /// </summary>
    public Bitmap? Bitmap { get; set; }

    /// <summary>
    /// Saved reference to the input image.
    /// </summary>
    public SpriteData Sprite { get; internal set; }
}
