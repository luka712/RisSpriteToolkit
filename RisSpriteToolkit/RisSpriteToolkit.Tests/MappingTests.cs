using RisSpriteToolkit;
using RisSpriteToolkit.Data.Image;
using RisSpriteToolkit.Dto;
using RisSpriteToolkit.Sprites;
using RisSpriteToolkit.Sprites.Skyline;

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
            var rect = new SpriteToolkit.Math.Rect(25, 25, 50, 50);
            var sourceRectJson = mapper.ToSourceRect(rect);
            Assert.That(sourceRectJson.X, Is.EqualTo(25));
            Assert.That(sourceRectJson.Y, Is.EqualTo(25));
            Assert.That(sourceRectJson.Width, Is.EqualTo(50));
            Assert.That(sourceRectJson.Height, Is.EqualTo(50));
        }

        /// <summary>
        /// Test mapping from <see cref="Sprite"/> to <see cref="SpriteDto"/>.
        /// </summary>
        [Test]
        public void Test_Sprite_To_SpriteJson_Mapping()
        {
            var mapper = new MapperService();
            BuilderSkylineSpriteSheet spriteSheet = new()
            {
                FilePath = "TestSheet.png",
                Name = "TestSheet",
            };
            RawImage rawImage = new ("TestSprite.png", 50, 50, new byte[50 * 50 * 4], 4);

            BuilderSprite sprite = new (rawImage, new System.Drawing.Point(100, 100), spriteSheet);
            sprite.Name = "TestSprite";
            Sprite spriteDto = mapper.ToSprite(sprite);
            Assert.That(spriteDto.Name, Is.EqualTo("TestSprite"));
            Assert.That(spriteDto.FileName, Is.EqualTo("TestSprite.png"));
            Assert.That(spriteDto.SourceRect.X, Is.EqualTo(100));
            Assert.That(spriteDto.SourceRect.Y, Is.EqualTo(100));
            Assert.That(spriteDto.SourceRect.Width, Is.EqualTo(50));
            Assert.That(spriteDto.SourceRect.Height, Is.EqualTo(50));
            Assert.That(spriteDto.U0, Is.EqualTo(100f / spriteSheet.Size.Width));
            Assert.That(spriteDto.V0, Is.EqualTo(100f / spriteSheet.Size.Height));
            Assert.That(spriteDto.U1, Is.EqualTo((100f + 50f) / spriteSheet.Size.Width));
            Assert.That(spriteDto.V1, Is.EqualTo((100f + 50f) / spriteSheet.Size.Height));
        }

        /// <summary>
        /// Test mapping from <see cref="SpriteSheet"/> to <see cref="SpriteSheetDto"/>.
        /// </summary>
        [Test]
        public void Test_SpriteSheet_To_SpriteSheetJson_Mapping()
        {
            var mapper = new MapperService();
            BuilderSkylineSpriteSheet spriteSheet = new(size: new System.Drawing.Size(100, 100))
            {
                Name = "TestSheet",
                FilePath = "TestSheet.png",
                Padding = 0,
            };
            spriteSheet.AddSprite(new RawImage("TestSprite.png", 50, 50, new byte[50 * 50 * 4], 4));

            SpriteSheet spriteSheetDto = mapper.ToSpriteSheet(spriteSheet);

            // Test sprite sheet properties
            Assert.That(spriteSheetDto.Name, Is.EqualTo("TestSheet"));
            Assert.That(spriteSheetDto.FilePath, Is.EqualTo("TestSheet.png"));
            Assert.That(spriteSheetDto.Sprites.Count, Is.EqualTo(1));

            // Test sprite properties
            Sprite spriteDto = spriteSheetDto.Sprites[0];
            Assert.That(spriteDto.Name, Is.EqualTo("TestSprite"));
            Assert.That(spriteDto.FileName, Is.EqualTo("TestSprite.png"));
            Assert.That(spriteDto.SourceRect.X, Is.EqualTo(0));
            Assert.That(spriteDto.SourceRect.Y, Is.EqualTo(0));
            Assert.That(spriteDto.SourceRect.Width, Is.EqualTo(50));
            Assert.That(spriteDto.SourceRect.Height, Is.EqualTo(50));
            Assert.That(spriteDto.U0, Is.EqualTo(0.0f));
            Assert.That(spriteDto.V0, Is.EqualTo(0.0f));
            Assert.That(spriteDto.U1, Is.EqualTo(0.5f));
            Assert.That(spriteDto.V1, Is.EqualTo(0.5f));
        }
    }
}
