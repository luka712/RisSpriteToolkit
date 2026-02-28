namespace RisGameFramework.SpriteToolkit.Tests
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
            SpriteTKBundleBuilder builder = new();
            builder.AllowReplace = true;
            builder.PngSpriteSheetBuilder.AllowReplace = true;
            builder.PngSpriteSheetBuilder.DefaultSheetName = "game_assets_sheet";
            builder.AddDirectoryContents("./Data/");
            builder.SaveBundle("./game_assets", "game_assets");
            string json = File.ReadAllText("./game_assets/game_assets.json");
            Assert.IsNotNull(json);
            Assert.IsNotEmpty(json);
        }
    }
}