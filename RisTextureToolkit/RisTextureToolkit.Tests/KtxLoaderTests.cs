using RisSpriteToolkit.Loaders;
using RisTextureToolkit.Ktx;


namespace RisGameFramework.SpriteToolkit.Tests
{
    /// <summary>
    /// The KTX texture tests.
    /// </summary>
    [TestFixture]
    internal class KtxLoaderTest
    {
        private const string TEXT_PNG = "Data/test.png";
        private const string TEST_KTX_BASIS_UASTC = "Data/test_basis_uastc.ktx2";

        /// <summary>
        /// Test try load unexisting KTX texture.
        /// </summary>
        [Test]
        public void Test_LoadBasis()
        {
            var loader = new Ktx2Loader();
            var rawImage = loader.LoadBasis(TEST_KTX_BASIS_UASTC, KtxTranscodeFormat.TTF_BC7_RGBA);
            Assert.That(rawImage, Is.Not.Null);
            Assert.That(rawImage.Data, Is.Not.Null);
            Assert.That(rawImage.Width, Is.GreaterThan(0));
            Assert.That(rawImage.Height, Is.GreaterThan(0));
        }
    }
}
