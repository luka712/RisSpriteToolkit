using RisGameFramework.SpriteToolkit;

namespace RisGameFramework.SpriteToolkit.Tests;

public class SpriteSheetBuilderTests
{
    /// <summary>
    /// Try to add a batch of images to the <see cref="SpriteSheetBuilder"/>.
    /// It should be 6 images which need to be added the <see cref="SpriteSheetBuilder"/>.
    /// </summary>
    [Test]
    public void Test_AddBatchToBuilder()
    {
        SpriteSheetBuilder builder = new(new System.Drawing.Size(1024,1024));
        builder.Padding = 0; // No padding for this test

        List<RawImage> images = new();
        for (int i = 0; i < 6; i++)
        {
            images.Add(new RawImage($"Test{i}.png", 512, 512, new byte[512 * 512 * 4], 4));
        }

        foreach (var image in images)
        {
            builder.AddSprite(image);
        }

        // Builder is set to 1024x1024, so it should fit 4 images of 512x512 in one sheet.
        Assert.That(builder.SpriteSheets.Count, Is.EqualTo(2));
    }

    /// <summary>
    /// Add 1 entity of 510x510 to the builder which has padding of 1.
    /// </summary>
    [Test]
    public void Test_Add1ToBuilder_PaddingIsCorrect()
    {
        SpriteSheetBuilder builder = new(new System.Drawing.Size(1024, 1024));
        builder.Padding = 1;
        builder.AllowReplace = true;

        BuilderSprite sprite = builder.AddImage("Data/test.png");

        builder.Save("Output", out _, out _);

        Assert.That(sprite.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.X, Is.EqualTo(1));
        Assert.That(sprite.SourceRect.Y, Is.EqualTo(1));
        Assert.That(builder.SpriteSheets.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Add 2 entities of 510x510 to the builder which has padding of 1.
    /// </summary>
    [Test]
    public void Test_Add2ToBuilder_PaddingIsCorrect()
    {
        const int Padding = 1;

        SpriteSheetBuilder builder = new(new System.Drawing.Size(1024, 1024));
        builder.Padding = Padding;
        builder.AllowReplace = true;

        BuilderSprite sprite = builder.AddImage("Data/test.png");
        BuilderSprite sprite2 = builder.AddImage("Data/test.png");

        builder.Save("Output", out _, out _);

        Assert.That(sprite.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.X, Is.EqualTo(Padding));
        Assert.That(sprite.SourceRect.Y, Is.EqualTo(Padding));

        Assert.That(sprite2.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite2.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite2.SourceRect.X, Is.EqualTo(Padding + sprite.SourceRect.Width + Padding * 2));
        Assert.That(sprite2.SourceRect.Y, Is.EqualTo(Padding));

        Assert.That(builder.SpriteSheets.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Add 3 entities of 510x510 to the builder which has padding of 1.
    /// </summary>
    [Test]
    public void Test_Add3ToBuilder_PaddingIsCorrect()
    {
        const int Padding = 1;

        SpriteSheetBuilder builder = new(new System.Drawing.Size(1024, 1024));
        builder.Padding = Padding;
        builder.AllowReplace = true;

        BuilderSprite sprite = builder.AddImage("Data/test.png");
        BuilderSprite sprite2 = builder.AddImage("Data/test.png");
        BuilderSprite sprite3 = builder.AddImage("Data/test.png");

        builder.Save("Output", out _, out _);

        Assert.That(sprite.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.X, Is.EqualTo(Padding));
        Assert.That(sprite.SourceRect.Y, Is.EqualTo(Padding));

        Assert.That(sprite2.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite2.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite2.SourceRect.X, Is.EqualTo(Padding + sprite.SourceRect.Width + Padding * 2)); 
        Assert.That(sprite2.SourceRect.Y, Is.EqualTo(Padding));

        Assert.That(sprite3.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite3.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite3.SourceRect.X, Is.EqualTo(Padding)); // New row
        Assert.That(sprite3.SourceRect.Y, Is.EqualTo(Padding + sprite.SourceRect.Height + Padding * 2)); 

        Assert.That(builder.SpriteSheets.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Add 4 entities of 510x510 to the builder which has padding of 1.
    /// </summary>
    [Test]
    public void Test_Add4ToBuilder_PaddingIsCorrect()
    {
        const int Padding = 1;

        SpriteSheetBuilder builder = new(new System.Drawing.Size(1024, 1024));
        builder.Padding = Padding;
        builder.AllowReplace = true;

        BuilderSprite sprite = builder.AddImage("Data/test.png");
        BuilderSprite sprite2 = builder.AddImage("Data/test.png");
        BuilderSprite sprite3 = builder.AddImage("Data/test.png");
        BuilderSprite sprite4 = builder.AddImage("Data/test.png");

        builder.Save("Output", out _, out _);

        Assert.That(sprite.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite.SourceRect.X, Is.EqualTo(Padding));
        Assert.That(sprite.SourceRect.Y, Is.EqualTo(Padding));

        Assert.That(sprite2.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite2.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite2.SourceRect.X, Is.EqualTo(Padding + sprite.SourceRect.Width + Padding * 2));
        Assert.That(sprite2.SourceRect.Y, Is.EqualTo(Padding));

        Assert.That(sprite3.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite3.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite3.SourceRect.X, Is.EqualTo(Padding)); // New row
        Assert.That(sprite3.SourceRect.Y, Is.EqualTo(Padding + sprite.SourceRect.Height + Padding * 2)); 

        Assert.That(sprite4.SourceRect.Width, Is.EqualTo(510));
        Assert.That(sprite4.SourceRect.Height, Is.EqualTo(510));
        Assert.That(sprite4.SourceRect.X, Is.EqualTo(Padding + sprite3.SourceRect.Width + Padding * 2)); 
        Assert.That(sprite4.SourceRect.Y, Is.EqualTo(Padding + sprite.SourceRect.Height + Padding * 2)); 

        Assert.That(builder.SpriteSheets.Count, Is.EqualTo(1));
    }

    /// <summary>
    /// Test changing the Size property of the SpriteSheetBuilder.
    /// </summary>
    [Test]  
    public void Test_Size()
    {
        SpriteSheetBuilder builder = new(new System.Drawing.Size(2048, 2048));
        Assert.That(builder.Size, Is.EqualTo(new System.Drawing.Size(2048, 2048)));
        builder.Size = new System.Drawing.Size(1024, 1024);
        Assert.That(builder.Size, Is.EqualTo(new System.Drawing.Size(1024, 1024)));
    }
    
    
    /// <summary>
    /// Try to add a batch of images to the <see cref="SpriteSheetBuilder"/> which will result
    /// in 2 spritesheets. Confirm that both names are indexed.
    /// </summary>
    [Test]
    public void Test_AddBatchToBuilder_ConfirmNameIndexed()
    {
        SpriteSheetBuilder builder = new(new System.Drawing.Size(1024,1024));
        builder.Padding = 0; // No padding for this test

        List<RawImage> images = new();
        for (int i = 0; i < 6; i++)
        {
            images.Add(new RawImage($"Test{i}.png", 512, 512, new byte[512 * 512 * 4], 4));
        }

        foreach (var image in images)
        {
            builder.AddSprite(image);
        }

        // Builder is set to 1024x1024, so it should fit 4 images of 512x512 in one sheet.
        Assert.That(builder.SpriteSheets.Count, Is.EqualTo(2));
        Assert.That(builder.SpriteSheets[0].Name, Is.EqualTo($"{builder.DefaultSheetName}_0"));
        Assert.That(builder.SpriteSheets[1].Name, Is.EqualTo($"{builder.DefaultSheetName}_1"));

    }
}