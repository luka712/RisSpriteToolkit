using RisSpriteToolkit.Loaders;
using RisTextureToolkit.Ktx;


namespace RisGameFramework.SpriteToolkit.Tests
{
    /// <summary>
    /// The KTX texture tests.
    /// </summary>
    [TestFixture]
    internal class KtxTextureTest
    {
        private const string KTX_TEXTURE_PATH = "Data/test.ktx2";

        /// <summary>
        /// Test try load unexisting KTX texture.
        /// </summary>
        [Test]
        public void Test_Try_Load_Unexisting_KTX_Texture()
        {
            Assert.Throws<FileNotFoundException>(() =>
            {
                var texture = new KtxTexture2("nonexistent_file.ktx", KtxTranscodeFormat.TTF_BC7_RGBA);
            });
        }

        /// <summary>
        /// Test try load existing KTX texture.
        /// </summary>
        [Test]
        public void Test_Try_Load_Existing_KTX_Texture()
        {
            try
            {
                using var texture = new KtxTexture2(KTX_TEXTURE_PATH, KtxTranscodeFormat.TTF_BC7_RGBA);
                Assert.That(texture.TexturePtr, Is.Not.EqualTo(IntPtr.Zero), "Texture pointer should not be null after successful load.");
            }
            catch (Exception ex)
            {
                Assert.Fail($"Loading a valid KTX texture should not throw an exception. Exception message: {ex.Message}");
            }
        }

        /// <summary>
        /// Test try load existing KTX texture.
        /// </summary>
        [Test]
        public void Test_Confirm_KTX_Texture_Dimensions()
        {
            try
            {
                using var texture = new KtxTexture2(KTX_TEXTURE_PATH, KtxTranscodeFormat.TTF_BC7_RGBA);
                Assert.That(texture.TexturePtr, Is.Not.EqualTo(IntPtr.Zero), "Texture pointer should not be null after successful load.");
                Assert.That(texture.Width, Is.GreaterThan(0), "Texture width should be greater than 0.");
                Assert.That(texture.Height, Is.GreaterThan(0), "Texture height should be greater than 0.");

            }
            catch (Exception ex)
            {
                Assert.Fail($"Loading a valid KTX texture should not throw an exception. Exception message: {ex.Message}");
            }
        }

        private KtxTexture2 CreateAndFillTexture()
        {
            var imageLoader = new ImageLoader();
            var image = imageLoader.LoadImage("Data/png_test.png");

            var createInfo = new KtxTextureCreateInfo();
            createInfo.vkFormat = VkFormat.R8G8B8A8_UNORM;  // or your desired format
            createInfo.baseWidth = (uint)image.Width;
            createInfo.baseHeight = (uint)image.Height;
            createInfo.pDfd = IntPtr.Zero; // Data Format Descriptor, can be null for simple formats
            createInfo.baseDepth = 1;
            createInfo.numDimensions = 2;
            createInfo.numLevels = 1;      // 1 = no mipmaps, or more if you generate them
            createInfo.numLayers = 1;
            createInfo.numFaces = 1;
            createInfo.isArray = false;
            createInfo.generateMipmaps = false;

            var texture = new KtxTexture2(createInfo);

            texture.SetImageFromMemory(0, 0, 0, image.Data, (uint)image.Data.Length);
            return texture;
        }

        /// <summary>
        /// Test create and fill KTX texture.
        /// </summary>
        [Test]
        public void Test_Create_And_Fill_Texture()
        {
            using var texture = CreateAndFillTexture();
            var textureData = texture.GetTextureData();
            Assert.That(textureData, Is.Not.Null, "Texture data should not be null after setting image data.");
            Assert.That(textureData.Length, Is.GreaterThan(0), "Texture data length should be greater than 0 after setting image data.");
        }

        /// <summary>
        /// Test create and fill KTX texture, then try to write to file.
        /// </summary>
        [Test]
        public void Test_Create_And_Fill_Texture_Write_To_File()
        {
            using var texture = CreateAndFillTexture();
            texture.WriteToNamedFile("Data/test_output.ktx2");
            Assert.That(File.Exists("Data/test_output.ktx2"), "Output KTX file should exist after writing.");
            var fileInfo = new FileInfo("Data/test_output.ktx2");
            Assert.That(fileInfo.Length, Is.GreaterThan(0), "Output KTX file should have a size greater than 0.");
        }
    }
}
