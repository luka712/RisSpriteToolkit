using Ris.AssetToolkit.UIClient.Data;
using Avalonia;
using Avalonia.Controls.Primitives;
using System.Collections.ObjectModel;

namespace Ris.AssetToolkit.UIClient.Controls;

/// <summary>
/// The control for displaying a folder item in the folder list.
/// </summary>
public partial class FolderListItem : TemplatedControl
{
    /// <summary>
    /// The default constructor for the image list item.
    /// </summary>
    public FolderListItem()
    {
        FolderName = "Template";
        SubFolders = new ObservableCollection<FolderData>();
    }

    /// <summary>
    /// The title property for the image item.
    /// </summary>
    public static readonly StyledProperty<string> FolderNameProperty =
        AvaloniaProperty.Register<ImageListItem, string>(nameof(FolderName));

    /// <summary>
    /// The name of the image.
    /// </summary>
    public string FolderName
    {
        get => GetValue(FolderNameProperty);
        set => SetValue(FolderNameProperty, value);
    }

    /// <summary>
    /// The title property for the image item.
    /// </summary>
    public static readonly StyledProperty<ObservableCollection<FolderData>> SubFoldersProperty =
        AvaloniaProperty.Register<ImageListItem, ObservableCollection<FolderData>>(nameof(SubFolders));

    /// <summary>
    /// The name of the image.
    /// </summary>
    public ObservableCollection<FolderData> SubFolders
    {
        get => GetValue(SubFoldersProperty);
        set => SetValue(SubFoldersProperty, value);
    }

    /// <summary>
    /// The has sub-folders property.
    /// </summary>
    public static readonly StyledProperty<bool> HasSubFoldersProperty =
        AvaloniaProperty.Register<ImageListItem, bool>(nameof(HasSubFolders));

    /// <summary>
    /// Indicates if folder has sub-folders.
    /// </summary>
    public bool HasSubFolders
    {
        get => GetValue(HasSubFoldersProperty);
        set => SetValue(HasSubFoldersProperty, value);
    }
}