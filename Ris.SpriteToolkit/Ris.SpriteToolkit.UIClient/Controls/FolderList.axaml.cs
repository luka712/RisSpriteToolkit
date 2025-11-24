using Ris.AssetToolkit.UIClient.Data;
using Avalonia;
using Avalonia.Controls.Primitives;
using System.Collections.ObjectModel;

namespace Ris.AssetToolkit.UIClient.Controls;

public class FolderList : TemplatedControl
{
    public FolderList()
    {
        Source = new ObservableCollection<FolderData>();
    }

    /// <summary>
    /// The source property for the folder list.
    /// </summary>
    public static readonly StyledProperty<ObservableCollection<FolderData>> SourceProperty =
        AvaloniaProperty.Register<FolderList, ObservableCollection<FolderData>>(nameof(Source));

    /// <summary>
    /// The collection of folder names.
    /// </summary>
    public ObservableCollection<FolderData> Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }
}