using RisTextureToolkit;
using RisTextureToolkit.Data.Image;
using RisTextureToolkit.Textures;

namespace RisTextureToolkit.Tests
{
    /// <summary>
    /// The <see cref="TextureAtlasBuilder"/> tests.
    /// </summary>
    internal class TextureAtlasBuilderTests
    {
        private const string TEST_PNG = "Data/test.png";

        /// <summary>
        /// Try to add a batch of images to the <see cref="TextureAtlasBuilder"/>.
        /// It should be 6 images that need to be added the <see cref="TextureAtlasBuilder"/>.
        /// </summary>
        [Test]
        public void Test_AddBatchToBuilder()
        {
            TextureAtlasBuilder builder = new(new System.Drawing.Size(1024, 1024));
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
            Assert.That(builder.TextureAtlases.Count, Is.EqualTo(2));
        }

        /// <summary>
        /// Add 1 entity of 510x510 to the builder which has padding of 1.
        /// </summary>
        [Test]
        public void Test_Add1ToBuilder_PaddingIsCorrect()
        {
            TextureAtlasBuilder builder = new(new System.Drawing.Size(1024, 1024));
            builder.Padding = 1;
            builder.AllowReplaceTextureAtlas = true;

            BuilderTexture texture = builder.AddImage(TEST_PNG);

            builder.Save("Output", new BundleBuildOptions(), out _, out _);

            Assert.That(texture.SourceRect.Width, Is.EqualTo(248));
            Assert.That(texture.SourceRect.Height, Is.EqualTo(248));
            Assert.That(texture.SourceRect.X, Is.EqualTo(1));
            Assert.That(texture.SourceRect.Y, Is.EqualTo(1));
            Assert.That(builder.TextureAtlases.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Add 2 entities of 510x510 to the builder which has padding of 1.
        /// </summary>
        [Test]
        public void Test_Add2ToBuilder_PaddingIsCorrect()
        {
            const int Padding = 1;

            TextureAtlasBuilder builder = new(new System.Drawing.Size(1024, 1024));
            builder.Padding = Padding;
            builder.AllowReplaceTextureAtlas = true;

            BuilderTexture texture = builder.AddImage("Data/test.png");
            BuilderTexture sprite2 = builder.AddImage("Data/test.png");

            builder.Save("Output", new BundleBuildOptions(), out _, out _);

            Assert.That(texture.SourceRect.Width, Is.EqualTo(248));
            Assert.That(texture.SourceRect.Height, Is.EqualTo(248));
            Assert.That(texture.SourceRect.X, Is.EqualTo(Padding));
            Assert.That(texture.SourceRect.Y, Is.EqualTo(Padding));

            Assert.That(sprite2.SourceRect.Width, Is.EqualTo(248));
            Assert.That(sprite2.SourceRect.Height, Is.EqualTo(248));
            Assert.That(sprite2.SourceRect.X, Is.EqualTo(Padding + texture.SourceRect.Width + Padding * 2));
            Assert.That(sprite2.SourceRect.Y, Is.EqualTo(Padding));

            Assert.That(builder.TextureAtlases.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Add 3 entities of 510x510 to the builder which has padding of 1.
        /// </summary>
        [Test]
        public void Test_Add3ToBuilder_PaddingIsCorrect()
        {
            const int Padding = 1;

            TextureAtlasBuilder builder = new(new System.Drawing.Size(512, 512));
            builder.Padding = Padding;
            builder.AllowReplaceTextureAtlas = true;

            BuilderTexture texture = builder.AddImage(TEST_PNG);
            BuilderTexture sprite2 = builder.AddImage(TEST_PNG);
            BuilderTexture sprite3 = builder.AddImage(TEST_PNG);

            builder.Save("Output", new BundleBuildOptions(), out _, out _);

            Assert.That(texture.SourceRect.Width, Is.EqualTo(248));
            Assert.That(texture.SourceRect.Height, Is.EqualTo(248));
            Assert.That(texture.SourceRect.X, Is.EqualTo(Padding));
            Assert.That(texture.SourceRect.Y, Is.EqualTo(Padding));

            Assert.That(sprite2.SourceRect.Width, Is.EqualTo(248));
            Assert.That(sprite2.SourceRect.Height, Is.EqualTo(248));
            Assert.That(sprite2.SourceRect.X, Is.EqualTo(Padding + texture.SourceRect.Width + Padding * 2));
            Assert.That(sprite2.SourceRect.Y, Is.EqualTo(Padding));

            Assert.That(sprite3.SourceRect.Width, Is.EqualTo(248));
            Assert.That(sprite3.SourceRect.Height, Is.EqualTo(248));
            Assert.That(sprite3.SourceRect.X, Is.EqualTo(Padding)); // New row
            Assert.That(sprite3.SourceRect.Y, Is.EqualTo(Padding + texture.SourceRect.Height + Padding * 2));

            Assert.That(builder.TextureAtlases.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Add 4 entities of 510x510 to the builder which has padding of 1.
        /// </summary>
        [Test]
        public void Test_Add4ToBuilder_PaddingIsCorrect()
        {
            const int Padding = 1;

            TextureAtlasBuilder builder = new(new System.Drawing.Size(512, 512));
            builder.Padding = Padding;
            builder.AllowReplaceTextureAtlas = true;

            BuilderTexture texture = builder.AddImage(TEST_PNG);
            BuilderTexture sprite2 = builder.AddImage(TEST_PNG);
            BuilderTexture sprite3 = builder.AddImage(TEST_PNG);
            BuilderTexture sprite4 = builder.AddImage(TEST_PNG);

            builder.Save("Output", new BundleBuildOptions(), out _, out _);

            Assert.That(texture.SourceRect.Width, Is.EqualTo(248));
            Assert.That(texture.SourceRect.Height, Is.EqualTo(248));
            Assert.That(texture.SourceRect.X, Is.EqualTo(Padding));
            Assert.That(texture.SourceRect.Y, Is.EqualTo(Padding));

            Assert.That(sprite2.SourceRect.Width, Is.EqualTo(248));
            Assert.That(sprite2.SourceRect.Height, Is.EqualTo(248));
            Assert.That(sprite2.SourceRect.X, Is.EqualTo(Padding + texture.SourceRect.Width + Padding * 2));
            Assert.That(sprite2.SourceRect.Y, Is.EqualTo(Padding));

            Assert.That(sprite3.SourceRect.Width, Is.EqualTo(248));
            Assert.That(sprite3.SourceRect.Height, Is.EqualTo(248));
            Assert.That(sprite3.SourceRect.X, Is.EqualTo(Padding)); // New row
            Assert.That(sprite3.SourceRect.Y, Is.EqualTo(Padding + texture.SourceRect.Height + Padding * 2));

            Assert.That(sprite4.SourceRect.Width, Is.EqualTo(248));
            Assert.That(sprite4.SourceRect.Height, Is.EqualTo(248));
            Assert.That(sprite4.SourceRect.X, Is.EqualTo(Padding + sprite3.SourceRect.Width + Padding * 2));
            Assert.That(sprite4.SourceRect.Y, Is.EqualTo(Padding + texture.SourceRect.Height + Padding * 2));

            Assert.That(builder.TextureAtlases.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Test changing the Size property of the SpriteSheetBuilder.
        /// </summary>
        [Test]
        public void Test_Size()
        {
            TextureAtlasBuilder builder = new(new System.Drawing.Size(2048, 2048));
            Assert.That(builder.Size, Is.EqualTo(new System.Drawing.Size(2048, 2048)));
            builder.Size = new System.Drawing.Size(1024, 1024);
            Assert.That(builder.Size, Is.EqualTo(new System.Drawing.Size(1024, 1024)));
        }

        /// <summary>
        /// Try to add a batch of images to the <see cref="TextureAtlasBuilder"/> which will result
        /// in 2 spritesheets. Confirm that both names are indexed.
        /// </summary>
        [Test]
        public void Test_AddBatchToBuilder_ConfirmNameIndexed()
        {
            TextureAtlasBuilder builder = new(new System.Drawing.Size(1024, 1024));
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
            Assert.That(builder.TextureAtlases.Count, Is.EqualTo(2));
            Assert.That(builder.TextureAtlases[0].Name, Is.EqualTo($"{builder.DefaultSheetName}_0"));
            Assert.That(builder.TextureAtlases[1].Name, Is.EqualTo($"{builder.DefaultSheetName}_1"));
        }

        /// <summary>
        /// Test if JSON and sprite sheet are saved in the same directory.
        /// </summary>
        [Test]
        public void TestSavedInSameDirectory()
        {
            TextureTKBundleBuilder builder = new();
            builder.DefaultSheetName = "Test";
            builder.AllowReplaceTextureAtlas = true;
            builder.AllowReplaceJsonBundle = true;
            builder.AddRawImage(new RawImage($"Test.png", 512, 512, new byte[512 * 512 * 4], 4));

            builder.SaveBundle("Test/Output", "Test");

            Assert.That(Directory.Exists("Test/Output"));
            Assert.That(File.Exists("Test/Output/Test_0.png"));
            Assert.That(File.Exists("Test/Output/Test.json"));
        }

        /// <summary>
        /// Test if JSON and sprite sheet are saved in the same directory.
        /// </summary>
        [Test]
        public void Test_SavedAsKTX2Uncompressed()
        {
            TextureTKBundleBuilder builder = new();
            builder.DefaultSheetName = "Test";
            builder.AllowReplaceTextureAtlas = true;
            builder.AllowReplaceJsonBundle = true;
            builder.AddImage(TEST_PNG);

            builder.SaveBundle("Test/Output", "Test", new BundleBuildOptions()
            {
                TargetImageFormat = ImageFormat.KTX2,
            });

            Assert.That(Directory.Exists("Test/Output"));
            Assert.That(File.Exists("Test/Output/Test_0.ktx2"));
            Assert.That(File.Exists("Test/Output/Test.json"));
            
            File.Delete("Test/Output/Test_0.ktx2");
        }
        
        /// <summary>
        /// Test if JSON and sprite sheet are saved in the same directory.
        /// </summary>
        [Test]
        public void Test_SavedAsKTX2Compressed()
        {
            TextureTKBundleBuilder builder = new();
            builder.DefaultSheetName = "Test";
            builder.AllowReplaceTextureAtlas = true;
            builder.AllowReplaceJsonBundle = true;
            builder.AddImage(TEST_PNG);

            builder.SaveBundle("Test/Output", "Test", new BundleBuildOptions()
            {
                TargetImageFormat = ImageFormat.KTX2,
                TargetPixelFormat = PixelFormat.BASIS_ETC1S,
                Etc1s = new BasisEtc1sOptions()
                {
                    QualityLevel = 128
                }
            });

            Assert.That(Directory.Exists("Test/Output"));
            Assert.That(File.Exists("Test/Output/Test_0.ktx2"));
            Assert.That(File.Exists("Test/Output/Test.json"));
            
            File.Delete("Test/Output/Test_0.ktx2");
        }

        /// <summary>
        /// Test saving a raw image which should be returned by <see cref="TextureAtlasBuilder.Save"/> method.
        /// </summary>
        [Test]
        public void Test_Save_RawImage()
        {
            TextureAtlasBuilder builder = new(new System.Drawing.Size(1, 1));
            builder.Padding = 0; // No padding for this test

            // Add a single white pixel.
            List<RawImage> images = new();
            builder.AddSprite(new RawImage($"Test.png", 1, 1, [255, 255, 255, 255], 4));

            var image = builder.TextureAtlases.First().SaveAsSkImage();

            // There should be a white pixel since we added a single white pixel.
            Assert.NotNull(image);
        }
    }
}