using AutoMapper;
using Ris.SpriteToolkit.Math;

namespace Ris.SpriteToolkit.Mapper;

/// <summary>
/// The AutoMapper profile for SpriteToolkit.
/// </summary>
internal class SpriteToolkitProfile : Profile
{
    /// <summary>
    /// The constructor for <see cref="SpriteToolkitProfile"/>.
    /// </summary>
    public SpriteToolkitProfile()
    {
        CreateMap<BuilderSkylineSpriteSheet, SpriteSheet>();
        CreateMap<BuilderSprite, Sprite>();
        CreateMap<Rect, SourceRect>();
    }
}
