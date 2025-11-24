using Ris.AssetToolkit.UIClient.Data;
using Avalonia;
using Avalonia.Controls.Primitives;
using Ris.AssetToolkit.UIClient.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Ris.AssetToolkit.UIClient.Controls;

/// <summary>
/// The control for displaying a list of images.
/// Works with <see cref="ImageListItem"/> to display each image in the list.
/// </summary>
public class ImageList : TemplatedControl
{
    /// <summary>
    /// The source property for the image list.
    /// </summary>
    public static readonly StyledProperty<ObservableCollection<ImageData>> SourceProperty =
        AvaloniaProperty.Register<ImageList, ObservableCollection<ImageData>>(nameof(Source));

    /// <summary>
    /// The collection of images.
    /// </summary>
    public ObservableCollection<ImageData> Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    /// <summary>
    /// The command to execute when the Remove button is clicked for an image item.
    /// </summary>
    public static readonly StyledProperty<ICommand?> RemoveImageCommandProperty =
        AvaloniaProperty.Register<ImageList, ICommand?>(nameof(RemoveImageCommand));

    /// <summary>
    /// The command to remove an image from the list.
    /// </summary>
    public ICommand? RemoveImageCommand
    {
        get => GetValue(RemoveImageCommandProperty);
        set => SetValue(RemoveImageCommandProperty, value);
    }

    /// <summary>
    /// The command to execute when the Remove button is clicked for an image item.
    /// </summary>
    public static readonly StyledProperty<ICommand?> TestCommandProperty =
         AvaloniaProperty.Register<ImageList, ICommand?>(nameof(TestCommand));

    /// <summary>
    /// The command to remove an image from the list.
    /// </summary>
    public ICommand? TestCommand
    {
        get => GetValue(TestCommandProperty);
        set => SetValue(TestCommandProperty, value);
    }
}
