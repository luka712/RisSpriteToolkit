using Riok.Mapperly.Abstractions;
using RisGameFramework.SpriteToolkit;
using RisGameFramework.SpriteToolkit.Math;
using RisSpriteToolkit.Dto;
using RisSpriteToolkit.Sprites;

namespace RisSpriteToolkit;

/// <summary>
/// The mapper service.
/// </summary>
[Mapper]
public partial class MapperService
{
    /// <summary>
    /// Creates a <see cref="SpriteSheet"/> from a <see cref="IBuilderSpriteSheet"/>.
    /// </summary>
    /// <param name="sheet">The <see cref="IBuilderSpriteSheet"/>.</param>
    /// <returns>The <see cref="SpriteSheet"/>.</returns>
    public partial SpriteSheet ToSpriteSheet(IBuilderSpriteSheet sheet);
    
    /// <summary>
    /// Creates a <see cref="Sprite"/> from a <see cref="BuilderSprite"/>.
    /// </summary>
    /// <param name="sprite">The <see cref="BuilderSprite"/>.</param>
    /// <returns>The <see cref="Sprite"/>.</returns>
    public partial Sprite ToSprite(BuilderSprite sprite);
    
    /// <summary>
    /// Creates a <see cref="SourceRect"/> from a <see cref="Rect"/>.
    /// </summary>
    /// <param name="rect">The <see cref="Rect"/>.</param>
    /// <returns>The <see cref="SourceRect"/>.</returns>
    public partial SourceRect ToSourceRect(Rect rect);
}