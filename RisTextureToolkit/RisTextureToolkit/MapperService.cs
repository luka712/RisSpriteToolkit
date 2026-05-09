using Riok.Mapperly.Abstractions;
using RisTextureToolkit.Dto;
using RisTextureToolkit.Math;
using RisTextureToolkit.Textures;

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
#pragma warning disable RMG020
#pragma warning disable RMG012
    public partial TextureAtlas ToTextureAtlas(IBuilderTextureAtlas sheet);
#pragma warning restore RMG012
#pragma warning restore RMG020
    
    /// <summary>
    /// Creates a <see cref="Texture"/> from a <see cref="BuilderTexture"/>.
    /// </summary>
    /// <param name="texture">The <see cref="BuilderTexture"/>.</param>
    /// <returns>The <see cref="Texture"/>.</returns>
#pragma warning disable RMG020
    public partial Texture ToTexture(BuilderTexture texture);
#pragma warning restore RMG020
    
    /// <summary>
    /// Creates a <see cref="SourceRect"/> from a <see cref="Rect"/>.
    /// </summary>
    /// <param name="rect">The <see cref="Rect"/>.</param>
    /// <returns>The <see cref="SourceRect"/>.</returns>
#pragma warning disable RMG020
    public partial SourceRect ToSourceRect(Rect rect);
#pragma warning restore RMG020
}