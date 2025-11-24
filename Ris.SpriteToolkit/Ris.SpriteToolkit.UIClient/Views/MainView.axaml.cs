using Ris.AssetToolkit.UIClient.Service;
using Ris.AssetToolkit.UIClient.ViewModels;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using Microsoft.Extensions.DependencyInjection;
using Ris.AssetToolkit.UIClient;

namespace Ris.AssetToolkit.UIClient.Views;

public partial class MainView : UserControl
{
    private MainViewModel viewModel;

    public MainView()
    {
        InitializeComponent();
        viewModel = new MainViewModel();
        DataContext = viewModel;
       
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (viewModel != null)
        {
            viewModel.MainWindow = this.Parent as Window;
        }
    }

    public void BuildButton_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
 
            ImageService imgService = Container.Locator.GetRequiredService<ImageService>();
            //imgService.BuildSpriteSheet("D:\\Projects\\Ris.AssetToolkit\\Ris.AssetToolkit\\Ris.AssetToolkit.UIClient.Desktop\\bin\\Debug\\net8.0\\test.json");
       
    }
}
