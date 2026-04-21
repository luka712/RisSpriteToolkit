using System.Drawing;
using System.Text.Json.Serialization;
using RisGameFramework.SpriteToolkit.Loaders;
using RisSpriteToolkit.Data.Image;
using RisTextureToolkit.Data.Image;
using RisTextureToolkit.ImageExporters;
using RisTextureToolkit.Textures;
using SkiaSharp;

namespace RisTextureToolkit.Sprites.Base
{
    /// <summary>
    /// The sprite sheet.
    /// </summary>
    public abstract class ABaseBuilderTextureAtlas : IBuilderTextureAtlas
    {
        private static Dictionary<ImageFormat, IImageExporter> _exporters = new Dictionary<ImageFormat, IImageExporter>
        {
            [ImageFormat.PNG] = new PngExporter(),
            [ImageFormat.KTX2] = new Ktx2Exporter(),
        };

        /// <summary>
        /// The list of sprites in the sprite sheet.
        /// </summary>
        protected readonly List<BuilderTexture> _sprites = new();

        /// <summary>
        /// The constructor for <see cref="ABaseBuilderTextureAtlas"/>.
        /// </summary>
        /// <param name="name">The name of a sprite sheet. By default, it is "SpriteSheet".</param>
        /// <param name="size">The size of a sprite sheet. If <c>null</c> is passed, by default it is <c>(2048,2048)</c>.</param>
        /// <param name="imageBackend">The <see cref="ImageBackend"/>. By default, it is <see cref="ImageBackend.Skia"/>.</param>
        public ABaseBuilderTextureAtlas(string name = "SpriteSheet", Size? size = null,
            ImageBackend imageBackend = ImageBackend.Skia)
        {
            Name = name;
            FilePath = $"{name}.png";
            if (size.HasValue)
            {
                Size = size.Value;
            }

            ImageBackend = imageBackend;
        }

        /// <summary>
        /// The <see cref="ImageBackend"/> being used.
        /// </summary>
        [JsonIgnore]
        public ImageBackend ImageBackend { get; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string FileName => Path.GetFileName(FilePath);

        /// <inheritdoc/>
        public string FilePath { get; set; }

        /// <summary>
        /// The image format of the sprite sheet.
        /// </summary>
        public string Format => "PNG";

        /// <inheritdoc/>
        public int Padding { get; set; } = 1;

        /// <inheritdoc/>
        public IReadOnlyList<BuilderTexture> Textures => _sprites;

        /// <inheritdoc/>
        public Size Size { get; } = new Size(2048, 2048);

        /// <inheritdoc/>
        public abstract bool FitsInSheet(RawImage image, out int pixelX, out int pixelY);

        /// <inheritdoc/>
        public abstract BuilderTexture AddSprite(RawImage image);

        /// <inheritdoc/>
        public abstract bool TryAddSprite(RawImage image, out BuilderTexture? sprite);

        /// <inheritdoc/>
        public bool RemoveSprite(BuilderTexture texture)
            => _sprites.Remove(texture);

        /// <inheritdoc/>
        public void Save(string filePath, ImageFormat imageFormat, PixelFormat pixelFormat)
        {
            using var image = SaveAsSkImage();
            if (ImageBackend == ImageBackend.Skia)
            {
                _exporters[imageFormat].Export(image, pixelFormat, filePath);
            }
            else
            {
                throw new NotSupportedException("Unsupported image backend.");
            }
        }

        /// <inheritdoc/>
        public SKImage SaveAsSkImage()
        {
            SKBitmap targetBitmap = new SKBitmap(Size.Width, Size.Height);

            using (var canvas = new SKCanvas(targetBitmap))
            {
                foreach (BuilderTexture sprite in _sprites)
                {
                    if (sprite.RawImage is RawImageSkia rawImageSkia)
                    {
                        SKBitmap bitmap = rawImageSkia.SkiaBitmap;

                        // Define the region of interest (ROI) in the target image
                        var srcRect = new SKRectI(0, 0, sprite.Size.Width, sprite.Size.Height);
                        var dstRect = new SKRectI(
                            sprite.Position.X,
                            sprite.Position.Y,
                            sprite.Position.X + sprite.Size.Width,
                            sprite.Position.Y + sprite.Size.Height);

                        canvas.DrawBitmap(bitmap, srcRect, dstRect);
                    }
                    else if (sprite.RawImage is RawImage rawImage)
                    {
                        SKData rawImageData = SKData.CreateCopy(rawImage.Data);
                        SKImageInfo info = new SKImageInfo(rawImage.Width, rawImage.Height);
                        SKImage skImg = SKImage.FromPixels(info, rawImageData);
                        SKBitmap bitmap = SKBitmap.FromImage(skImg);

                        // Define the region of interest (ROI) in the target image
                        var srcRect = new SKRectI(0, 0, sprite.Size.Width, sprite.Size.Height);
                        var dstRect = new SKRectI(
                            sprite.Position.X,
                            sprite.Position.Y,
                            sprite.Position.X + sprite.Size.Width,
                            sprite.Position.Y + sprite.Size.Height);

                        canvas.DrawBitmap(bitmap, srcRect, dstRect);

                        bitmap.Dispose();
                        skImg.Dispose();
                        rawImage.Dispose();
                    }
                }

                return SKImage.FromBitmap(targetBitmap);
            }
        }
    }
}