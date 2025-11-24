using Ris.AssetToolkit.UIClient.Service;
using Avalonia.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Ris.AssetToolkit.UIClient;

/// <summary>
/// Registers the services for dependency injection.
/// </summary>
public class Container
{
    public static ServiceProvider Locator { get; set; } = null!;

    /// <summary>
    /// Adds the common services to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="collection">The <see cref="IServiceCollection"/>.</param>
    public static void AddCommonServices(ServiceCollection collection)
    {
        // THIRD PARTY
        collection.AddSingleton<INotificationMessageManager>(new NotificationMessageManager());

        // Asset Toolkit services
        collection.AddSingleton<AssetBuilder>();

        // UI services
        collection.AddSingleton<ImageService>();

        Locator = collection.BuildServiceProvider();
    }
}
