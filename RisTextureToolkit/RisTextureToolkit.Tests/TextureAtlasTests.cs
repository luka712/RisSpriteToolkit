using RisGameFramework.SpriteToolkit;
using System.Drawing;
using RisSpriteToolkit.Data.Image;
using RisTextureToolkit.Data.Image;
using RisTextureToolkit.Dto;
using RisTextureToolkit.Math;
using RisTextureToolkit.Sprites.Skyline;

namespace SpriteToolkit.Tests
{
    /// <summary>
    /// Tests for the <see cref="TextureAtlas"/> class.
    /// </summary>
    [TestFixture]
    internal class TextureAtlasTests
    {
        /// <summary>
        /// A test for adding a sprite to the sprite sheet.
        /// </summary>
        [Test]
        public void Test_AddSprite()
        {
            BuilderSkylineTextureAtlas sheet = new();

            RawImage testImage = new RawImage("Test.png", 64, 64, new byte[64 * 64 * 4], 4);
            sheet.AddSprite(testImage);

            Assert.That(sheet.Textures.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// A test for adding a sprite to the sprite sheet.
        /// </summary>
        [Test]
        public void Test_AddMultipleSprites()
        {
            BuilderSkylineTextureAtlas sheet = new();
            sheet.Padding = 0; // No padding for this test

            sheet.AddSprite(new RawImage("Test.png", 64, 64, new byte[64 * 64 * 4], 4));
            sheet.AddSprite(new RawImage("Test2.png", 64, 64, new byte[64 * 64 * 4], 4));

            Assert.That(sheet.Textures.Count, Is.EqualTo(2));

            // Confirm coordinates do not overlap
            Rect first = sheet.Textures[0].SourceRect;
            Rect second = sheet.Textures[1].SourceRect;

            Assert.IsFalse(first.IntersectsWith(second));

            // Confirm positions are as expected
            Assert.That(first.Location, Is.EqualTo(new System.Drawing.Point(0, 0)));
            Assert.That(second.Location, Is.EqualTo(new Point(64, 0)));
        }

        /// <summary>
        /// A test for adding a sprite to the sprite sheet
        /// where padding is applied.
        /// </summary>
        [Test]
        public void Test_AddMultipleSpritesWithPadding()
        {
            const int PADDING = 1;

            BuilderSkylineTextureAtlas sheet = new(size: new Size(10 + PADDING * 4, 10 + PADDING * 4));
            sheet.Padding = PADDING;

            sheet.AddSprite(new RawImage("Test.png", 5, 5, new byte[5 * 5 * 4], 4));
            sheet.AddSprite(new RawImage("Test2.png", 5, 5, new byte[5 * 5 * 4], 4));
            sheet.AddSprite(new RawImage("Test3.png", 5, 5, new byte[5 * 5 * 4], 4));
            sheet.AddSprite(new RawImage("Test4.png", 5, 5, new byte[5 * 5 * 4], 4));

            // Confirm coordinates do not overlap
            Rect first = sheet.Textures[0].SourceRect;
            Rect second = sheet.Textures[1].SourceRect;
            Rect third = sheet.Textures[2].SourceRect;
            Rect fourth = sheet.Textures[3].SourceRect;

            // Confirm positions are as expected
            Assert.That(first.Location, Is.EqualTo(new Point(PADDING, PADDING)));
            Assert.That(second.Location, Is.EqualTo(new Point(PADDING + 5 + PADDING * 2, PADDING))); 
            Assert.That(third.Location, Is.EqualTo(new Point(PADDING, PADDING + 5 + PADDING * 2)));
            Assert.That(fourth.Location, Is.EqualTo(new Point(PADDING + 5 + PADDING * 2, PADDING + 5 + PADDING * 2)));
        }

        /// <summary>
        /// Test saving the sprite sheet.
        /// </summary>
        [Test]
        public void Test_SaveSheet()
        {
            BuilderSkylineTextureAtlas sheet = new();
            sheet.Padding = 0; // No padding for this test

            sheet.AddSprite(new RawImage("Test.png", 64, 64, new byte[64 * 64 * 4], 4));
            sheet.AddSprite(new RawImage("Test2.png", 64, 64, new byte[64 * 64 * 4], 4));

            sheet.Save("Test.png", RisTextureToolkit.ImageFormat.PNG, RisTextureToolkit.PixelFormat.RGBA8_UNORM);
            Assert.True(true); // If we reach here, the save succeeded.
        }
    }
}
