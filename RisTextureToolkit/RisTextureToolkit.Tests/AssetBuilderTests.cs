using RisTextureToolkit;

namespace RisSpriteToolkit.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test serializing the AssetBuilder to JSON.
        /// </summary>
        [Test]
        public void Test_AssetBuilderToJson()
        {
            TextureTKBundleBuilder builder = new();
            builder.AllowReplaceJsonBundle = true;
            builder.PngTextureAtlasBuilder.AllowReplaceTextureAtlas = true;
            builder.PngTextureAtlasBuilder.DefaultSheetName = "game_assets_sheet";
            builder.AddDirectoryContents("./Data/");
            builder.SaveBundle("./game_assets", "game_assets");
            string json = File.ReadAllText("./game_assets/game_assets.json");
            Assert.IsNotNull(json);
            Assert.IsNotEmpty(json);
        }
    }
}