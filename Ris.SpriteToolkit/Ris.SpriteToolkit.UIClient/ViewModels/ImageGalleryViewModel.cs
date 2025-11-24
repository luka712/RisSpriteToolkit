using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Ris.AssetToolkit.UIClient.ViewModels;

public partial class ImageGalleryViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<Bitmap> images = new ObservableCollection<Bitmap>();
}
