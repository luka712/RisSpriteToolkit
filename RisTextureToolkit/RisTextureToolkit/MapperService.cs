using Riok.Mapperly.Abstractions;
using RisGameFramework.SpriteToolkit;
using RisGameFramework.SpriteToolkit.Math;
using RisSpriteToolkit.Dto;
using RisSpriteToolkit.Sprites;
using RisTextureToolkit.Dto;
using RisTextureToolkit.Sprites;

namespace RisTextureToolkit;

/// <summary>
/// The mapper service.
/// </summary>
[Mapper]
public partial class MapperService
{
    /// <summary>
    /// Creates a <see cref="TextureAtlas"/> from a <see cref="IBuilderTextureAtlas"/>.
    /// </summary>
    /// <param name="sheet">The <see cref="IBuilderTextureAtlas"/>.</param>
    /// <returns>The <see cref="TextureAtlas"/>.</returns>
    public partial TextureAtlas ToTextureAtlas(IBuilderTextureAtlas sheet);
    
    /// <summary>
    /// Creates a <see cref="Texture"/> from a <see cref="BuilderTexture"/>.
    /// </summary>
    /// <param name="texture">The <see cref="BuilderTexture"/>.</param>
    /// <returns>The <see cref="Texture"/>.</returns>
    public partial Texture ToTexture(BuilderTexture texture);
    
    /// <summary>
    /// Creates a <see cref="SourceRect"/> from a <see cref="Rect"/>.
    /// </summary>
    /// <param name="rect">The <see cref="Rect"/>.</param>
    /// <returns>The <see cref="SourceRect"/>.</returns>
    public partial SourceRect ToSourceRect(Rect rect);
}