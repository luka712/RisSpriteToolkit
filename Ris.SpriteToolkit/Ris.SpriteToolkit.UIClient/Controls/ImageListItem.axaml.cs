using Ris.AssetToolkit.UIClient.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Ris.AssetToolkit.UIClient.Data;
using System;
using System.Windows.Input;

namespace Ris.AssetToolkit.UIClient.Controls;

/// <summary>
/// The control for displaying an image item in the image list.
/// </summary>
public class ImageListItem : TemplatedControl
{
    /// <summary>
    /// The default constructor for the image list item.
    /// </summary>
    public ImageListItem()
    {
        ImageName = "Template";
    }

    /// <summary>
    /// The source property for the image item.
    /// </summary>
    public static readonly StyledProperty<Bitmap> SourceProperty =
        AvaloniaProperty.Register<ImageListItem, Bitmap>(nameof(Source));

    /// <summary>
    /// The image as <see cref="Bitmap"/>.
    /// </summary>
    public Bitmap Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>
    /// The title property for the image item.
    /// </summary>
    public static readonly StyledProperty<string> ImageNameProperty =
        AvaloniaProperty.Register<ImageListItem, string>(nameof(ImageName));

    /// <summary>
    /// The name of the image.
    /// </summary>
    public string ImageName
    {
        get => GetValue(ImageNameProperty);
        set => SetValue(ImageNameProperty, value);
    }

    /// <summary>
    /// The command to execute when the Remove button is clicked.
    /// </summary>
    public static readonly StyledProperty<ICommand?> RemoveImageCommandProperty =
         AvaloniaProperty.Register<ImageListItem, ICommand?>(nameof(RemoveImageCommand));

    /// <summary>
    /// The command to execute when the Remove button is clicked.
    /// </summary>
    public ICommand? RemoveImageCommand
    {
        get => GetValue(RemoveImageCommandProperty);
        set => SetValue(RemoveImageCommandProperty, value);
    }

    /// <summary>
    /// The command parameter for the RemoveImageCommand.
    /// </summary>
    public static readonly StyledProperty<ImageData?> RemoveImageCommandParameterProperty =
     AvaloniaProperty.Register<ImageListItem, ImageData?>(nameof(RemoveImageCommandParameter));

    /// <summary>
    /// The command parameter for the RemoveImageCommand.
    /// </summary>
    public ImageData? RemoveImageCommandParameter
    {
        get => GetValue(RemoveImageCommandParameterProperty);
        set => SetValue(RemoveImageCommandParameterProperty, value);
    }
}
