using RisSpriteToolkit;
using RisSpriteToolkit.Data.Image;
using RisTextureToolkit.Data.Image;
using RisTextureToolkit.Dto;
using RisTextureToolkit.Math;
using RisTextureToolkit.Sprites.Skyline;
using RisTextureToolkit.Textures;
using MapperService = RisTextureToolkit.MapperService;

namespace RisGameFramework.SpriteToolkit.Tests
{
    /// <summary>
    /// Tests for the mapping between JSON DTOs and data models.
    /// </summary>
    [TestFixture]
    internal class MappingTests
    {
        /// <summary>
        /// Test mapping from <see cref="Math.Rect"/> to <see cref="SourceRectDto"/>.
        /// </summary>
        [Test]
        public void Test_Rect_To_SourceRectJson_Mapping()
        {
            var mapper = new MapperService();
            var rect = new Rect(25, 25, 50, 50);
            var sourceRectJson = mapper.ToSourceRect(rect);
            Assert.That(sourceRectJson.X, Is.EqualTo(25));
            Assert.That(sourceRectJson.Y, Is.EqualTo(25));
            Assert.That(sourceRectJson.Width, Is.EqualTo(50));
            Assert.That(sourceRectJson.Height, Is.EqualTo(50));
        }

        /// <summary>
        /// Test mapping from <see cref="Texture"/> to <see cref="SpriteDto"/>.
        /// </summary>
        [Test]
        public void Test_Sprite_To_SpriteJson_Mapping()
        {
            var mapper = new MapperService();
            BuilderSkylineTextureAtlas textureAtlas = new()
            {
                FilePath = "TestSheet.png",
                Name = "TestSheet",
            };
            RawImage rawImage = new ("TestSprite.png", 50, 50, new byte[50 * 50 * 4], 4);

            BuilderTexture texture = new (rawImage, new System.Drawing.Point(100, 100), textureAtlas);
            texture.Name = "TestSprite";
            Texture textureDto = mapper.ToTexture(texture);
            Assert.That(textureDto.Name, Is.EqualTo("TestSprite"));
            Assert.That(textureDto.FileName, Is.EqualTo("TestSprite.png"));
            Assert.That(textureDto.SourceRect.X, Is.EqualTo(100));
            Assert.That(textureDto.SourceRect.Y, Is.EqualTo(100));
            Assert.That(textureDto.SourceRect.Width, Is.EqualTo(50));
            Assert.That(textureDto.SourceRect.Height, Is.EqualTo(50));
            Assert.That(textureDto.U0, Is.EqualTo(100f / textureAtlas.Size.Width));
            Assert.That(textureDto.V0, Is.EqualTo(100f / textureAtlas.Size.Height));
            Assert.That(textureDto.U1, Is.EqualTo((100f + 50f) / textureAtlas.Size.Width));
            Assert.That(textureDto.V1, Is.EqualTo((100f + 50f) / textureAtlas.Size.Height));
        }

        /// <summary>
        /// Test mapping from <see cref="TextureAtlas"/> to <see cref="SpriteSheetDto"/>.
        /// </summary>
        [Test]
        public void Test_SpriteSheet_To_SpriteSheetJson_Mapping()
        {
            var mapper = new MapperService();
            BuilderSkylineTextureAtlas textureAtlas = new(size: new System.Drawing.Size(100, 100))
            {
                Name = "TestSheet",
                FilePath = "TestSheet.png",
                Padding = 0,
            };
            textureAtlas.AddSprite(new RawImage("TestSprite.png", 50, 50, new byte[50 * 50 * 4], 4));

            TextureAtlas textureAtlasDto = mapper.ToTextureAtlas(textureAtlas);

            // Test sprite sheet properties
            Assert.That(textureAtlasDto.Name, Is.EqualTo("TestSheet"));
            Assert.That(textureAtlasDto.FilePath, Is.EqualTo("TestSheet.png"));
            Assert.That(textureAtlasDto.Textures.Count, Is.EqualTo(1));

            // Test sprite properties
            Texture textureDto = textureAtlasDto.Textures[0];
            Assert.That(textureDto.Name, Is.EqualTo("TestSprite"));
            Assert.That(textureDto.FileName, Is.EqualTo("TestSprite.png"));
            Assert.That(textureDto.SourceRect.X, Is.EqualTo(0));
            Assert.That(textureDto.SourceRect.Y, Is.EqualTo(0));
            Assert.That(textureDto.SourceRect.Width, Is.EqualTo(50));
            Assert.That(textureDto.SourceRect.Height, Is.EqualTo(50));
            Assert.That(textureDto.U0, Is.EqualTo(0.0f));
            Assert.That(textureDto.V0, Is.EqualTo(0.0f));
            Assert.That(textureDto.U1, Is.EqualTo(0.5f));
            Assert.That(textureDto.V1, Is.EqualTo(0.5f));
        }
    }
}
