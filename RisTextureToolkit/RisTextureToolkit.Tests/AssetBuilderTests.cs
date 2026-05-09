namespace RisTextureToolkit.Tests
{
    /// <summary>
    /// Tests for <see cref="TextureTKBundleBuilder"/>.
    /// </summary>
    [TestFixture]
    public class AssetBuilderTests
    {
        /// <summary>
        /// Test serializing the AssetBuilder to JSON.
        /// </summary>
        [Test]
        public void Test_AssetBuilderToJson()
        {
            TextureTKBundleBuilder builder = new();
            builder.AllowReplaceJsonBundle = true;
            builder.AllowReplaceTextureAtlas = true;
            builder.DefaultSheetName = "game_assets_sheet";
            builder.AddDirectoryContents("./Data/");
            builder.SaveBundle("./game_assets", "game_assets");
            string json = File.ReadAllText("./game_assets/game_assets.json");
            Assert.IsNotNull(json);
            Assert.IsNotEmpty(json);
        }
    }
}