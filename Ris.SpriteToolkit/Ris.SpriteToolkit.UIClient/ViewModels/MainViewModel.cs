using Ris.AssetToolkit.UIClient.Data;
using Ris.AssetToolkit.UIClient.Service;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Notification;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Ris.AssetToolkit.UIClient;
using Ris.AssetToolkit.UIClient.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Ris.AssetToolkit.UIClient.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!!!";

    /// <summary>
    /// The notification message manager used to display notifications.
    /// The rest of the controls can just resolve <see cref="INotificationMessageManager"/>
    /// and use it to display notifications.
    /// </summary>
    public INotificationMessageManager Manager { get; } = Container.Locator.GetRequiredService<INotificationMessageManager>();

    [ObservableProperty]
    public ImageData? imageSource;

    [ObservableProperty]
    public ObservableCollection<ImageData> images = new();

    public Window MainWindow { get; set; }

    [RelayCommand]
    private async Task AddImagesFolderAsync()
    {
        ImageService imgService = Container.Locator.GetRequiredService<ImageService>();

        TopLevel topLevel = TopLevel.GetTopLevel(this.MainWindow);

        // Start async operation to open the dialog.
        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Add Images",
            AllowMultiple = true,
            FileTypeFilter = new List<FilePickerFileType>
            {
                new FilePickerFileType("Image Files")
                {
                    Patterns = new List<string> { "*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif" }
                }
            }
        });

        if (files.Count >= 1)
        {
            ImageData data = imgService.AddImage(files[0].Path.LocalPath);

            if (data != null)
            {
                ImageSource = data;
            }

            //// Open reading stream from the first file.
            //await using Stream stream = await files[0].OpenReadAsync();
            //using StreamReader streamReader = new StreamReader(stream);
            //// Reads all the content of file as a text.
            //string fileContent = await streamReader.ReadToEndAsync();
        }

        foreach (IStorageFile file in files)
        {
            ImageData? imageData = imgService.AddImage(file.Path.LocalPath);

            if (imageData != null)
            {
                Images.Add(imageData);
            }
        }
    }


    [RelayCommand]
    private void RemoveImage(ImageData imageData)
    {
        if (Images.Contains(imageData))
        {
            ImageService imgService = Container.Locator.GetRequiredService<ImageService>();
            if(!imgService.RemoveImage(imageData))
            {
                // TODO: Alert
                Console.WriteLine("Image not removed.");
            }
            Images.Remove(imageData);
        }
    }
}
