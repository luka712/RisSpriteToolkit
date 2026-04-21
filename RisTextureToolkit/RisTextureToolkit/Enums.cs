using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisTextureToolkit
{

    /// <summary>
    /// The pixel format of an image.
    /// </summary>
    public enum PixelFormat
    {
        RGBA8_UNORM,
        BASIS_LZ,
    }

    /// <summary>
    /// The supported image formats.
    /// </summary>
    public enum ImageFormat
    {
        /// <summary>
        /// Portable Network Graphics.
        /// </summary>
        PNG,

        /// <summary>
        /// Joint Photographic Experts Group.
        /// </summary>
        JPEG,

        /// <summary>
        /// Bitmap Image File.
        /// </summary>
        BMP,

        /// <summary>
        /// Graphics Interchange Format.
        /// </summary>
        GIF,

        /// <summary>
        /// Tagged Image File Format.
        /// </summary>
        TIFF,

        /// <summary>
        /// WebP Image Format.
        /// </summary>
        WEBP,

        /// <summary>
        /// Save as KTX2 file format.
        /// </summary>
        KTX2,
    }
}
