using Ris.SpriteToolkit.Math;
using System.Drawing;

namespace Ris.SpriteToolkit.Tests;

/// <summary>
/// Tests for the <see cref="Ris.SpriteToolkit.Image.SpriteSheet"/> class.
/// </summary>
[TestFixture]
internal class SpriteSheetTests
{
    /// <summary>
    /// A test for adding a sprite to the sprite sheet.
    /// </summary>
    [Test]
    public void Test_AddSprite()
    {
        BuilderSkylineSpriteSheet sheet = new();

        RawImage testImage = new RawImage("Test.png", 64, 64, new byte[64 * 64 * 4], 4);
        sheet.AddSprite(testImage);

        Assert.That(sheet.Sprites.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// A test for adding a sprite to the sprite sheet.
    /// </summary>
    [Test]
    public void Test_AddMultipleSprites()
    {
        BuilderSkylineSpriteSheet sheet = new();
        sheet.Padding = 0; // No padding for this test

        sheet.AddSprite(new RawImage("Test.png", 64, 64, new byte[64 * 64 * 4], 4));
        sheet.AddSprite(new RawImage("Test2.png", 64, 64, new byte[64 * 64 * 4], 4));

        Assert.That(sheet.Sprites.Count, Is.EqualTo(2));

        // Confirm coordinates do not overlap
        Rect first = sheet.Sprites[0].SourceRect;
        Rect second = sheet.Sprites[1].SourceRect;

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

        BuilderSkylineSpriteSheet sheet = new(size: new Size(10 + PADDING * 4, 10 + PADDING * 4));
        sheet.Padding = PADDING;

        sheet.AddSprite(new RawImage("Test.png", 5, 5, new byte[5 * 5 * 4], 4));
        sheet.AddSprite(new RawImage("Test2.png", 5, 5, new byte[5 * 5 * 4], 4));
        sheet.AddSprite(new RawImage("Test3.png", 5, 5, new byte[5 * 5 * 4], 4));
        sheet.AddSprite(new RawImage("Test4.png", 5, 5, new byte[5 * 5 * 4], 4));

        // Confirm coordinates do not overlap
        Rect first = sheet.Sprites[0].SourceRect;
        Rect second = sheet.Sprites[1].SourceRect;
        Rect third = sheet.Sprites[2].SourceRect;
        Rect fourth = sheet.Sprites[3].SourceRect;

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
        BuilderSkylineSpriteSheet sheet = new();
        sheet.Padding = 0; // No padding for this test

        sheet.AddSprite(new RawImage("Test.png", 64, 64, new byte[64 * 64 * 4], 4));
        sheet.AddSprite(new RawImage("Test2.png", 64, 64, new byte[64 * 64 * 4], 4));

        sheet.Save("Test.png");
        Assert.True(true); // If we reach here, the save succeeded.
    }
}
