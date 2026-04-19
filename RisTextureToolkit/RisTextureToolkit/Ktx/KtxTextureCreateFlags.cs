using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisTextureToolkit.Ktx
{
    /// <summary>
    /// The KtxTextureCreateFlags enumeration defines flags that can be used to control the behavior of texture creation when loading KTX files.
    /// </summary>
    public enum KtxTextureCreateFlags
    {
        /// <summary>
        /// No special handling. The texture will be created with the default behavior, which includes loading image data and key-value data from the KTX source.
        /// </summary>
        NO_FLAGS = 0x00,

        /// <summary>
        /// Load the image data from the KTX source.
        /// If this flag is not set, the texture will be created without loading the image data, which can be useful for scenarios where
        /// you only need to access metadata or key-value data without needing the actual texture data in memory.
        /// </summary>
        TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT = 0x01,

        KTX_TEXTURE_CREATE_RAW_KVDATA_BIT = 0x02,
        /*!< Load the raw key-value data instead of
             creating a @c ktxHashList from it. */
        KTX_TEXTURE_CREATE_SKIP_KVDATA_BIT = 0x04,
        /*!< Skip any key-value data. This overrides
             the RAW_KVDATA_BIT. */
        KTX_TEXTURE_CREATE_CHECK_GLTF_BASISU_BIT = 0x08
        /*!< Load texture compatible with the rules
             of KHR_texture_basisu glTF extension */
    }
}
