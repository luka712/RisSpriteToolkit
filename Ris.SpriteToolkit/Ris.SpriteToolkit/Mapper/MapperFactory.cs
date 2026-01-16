using AutoMapper;
using Microsoft.Extensions.Logging;

namespace RisGameFramework.SpriteToolkit.Mapper;

/// <summary>
/// The singleton factory for <see cref="IMapper"/>.
/// </summary>
internal static class MapperFactory
{
    /// <summary>
    /// Creates and configures the AutoMapper instance.
    /// </summary>
    /// <param name="loggerFactory">
    /// The optional <see cref="ILoggerFactory"/>.
    /// </param>
    /// <returns>The <see cref="IMapper"/>.</returns>
    internal static IMapper CreateMapper(ILoggerFactory? loggerFactory = null)
    {
        if (loggerFactory is null)
        {
            loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConsole()
                    .SetMinimumLevel(LogLevel.Error);
            });
            MapperConfiguration config = new(
                cfg => cfg.AddProfile<SpriteToolkitProfile>(), loggerFactory);
            
            IMapper mapper = config.CreateMapper();
            loggerFactory.Dispose();
            return mapper;
        }
        else
        {
            MapperConfiguration config = new(
                cfg => cfg.AddProfile<SpriteToolkitProfile>(), loggerFactory);

            return config.CreateMapper();
        }
    }
}