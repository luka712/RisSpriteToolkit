using Ris.AssetToolkit.UIClient.Controls;
using Ris.AssetToolkit.UIClient.Data;
using Ris.AssetToolkit.UIClient.Service;
using Avalonia.Controls;
using Avalonia.Notification;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;

namespace Ris.AssetToolkit.UIClient.UserControls
{
    public partial class BuildSpriteSheet : UserControl
    {
        private readonly ImageService imgService =
            Container.Locator.GetRequiredService<ImageService>();

        private readonly INotificationMessageManager manager =
            Container.Locator.GetRequiredService<INotificationMessageManager>();


        public BuildSpriteSheet()
        {
            InitializeComponent();
        }

        public void SelectFolder_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Window? window = this.GetVisualRoot() as Window;
            TopLevel? topLevel = TopLevel.GetTopLevel(window);

            if (topLevel is null)
            {
                return;
            }

            FolderPickerOpenOptions options = new FolderPickerOpenOptions
            {
                Title = "Select a folder",
                AllowMultiple = false,
                // You can set other options here if needed
            };
            topLevel.StorageProvider.OpenFolderPickerAsync(options).ContinueWith(async x =>
            {
                string? folderPath = x.Result.FirstOrDefault()?.Path?.AbsolutePath;
                if (!String.IsNullOrEmpty(folderPath))
                {
                    imgService.OutputFolder = folderPath ?? string.Empty;


                    Dispatcher.UIThread.Post(() =>
                    {
                        // Update folder data.
                        folderData.Source.Add(new FolderData(folderPath));
                        outputFileNameTextBox.Text = imgService.OutputFolder;
                    });
                }
            });

        }

        public void BuildButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ImageService imgService = Container.Locator.GetRequiredService<ImageService>();
            if (imgService.BuildSpriteSheet())
            {
                manager
                 .CreateMessage()
                 .Accent("#1751C3")
                 .Animates(true)
                 .Background("#333")
                 .HasBadge("Info")
                 .HasMessage("Sprite sheet built successfully!")
                 .Dismiss().WithButton("Close", button => { })
                 .Dismiss().WithDelay(TimeSpan.FromSeconds(5))
                 .Queue();
            }
        }

        public void OutputFileName_TextChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender!;
            ImageService imgService = Container.Locator.GetRequiredService<ImageService>();
            imgService.OutputFileName = textBox.Text ?? "";
        }
    }
}
