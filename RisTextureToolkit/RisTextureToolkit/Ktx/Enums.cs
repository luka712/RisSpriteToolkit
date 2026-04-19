namespace RisTextureToolkit.Ktx
{
    /// <summary>
    /// The KtxErrorCode enumeration defines the various error codes that can be returned by functions in the KTX library.
    /// These error codes provide information about the type of error that occurred during operations such as loading, transcoding, or uploading textures. By using these error codes, developers can handle errors more effectively and provide better feedback to users when issues arise with KTX texture processing.
    /// </summary>
    public enum KtxErrorCode : int
    {
        /// <summary>
        /// Operation was successful.
        /// </summary>
        KTX_SUCCESS = 0,        
        KTX_FILE_DATA_ERROR,     /*!< The data in the file is inconsistent with the spec. */
        KTX_FILE_ISPIPE,         /*!< The file is a pipe or named pipe. */
        /// <summary>
        /// The target file could not be opened.
        /// </summary>
        FILE_OPEN_FAILED,
        KTX_FILE_OVERFLOW,       /*!< The operation would exceed the max file size. */
        KTX_FILE_READ_ERROR,     /*!< An error occurred while reading from the file. */
        KTX_FILE_SEEK_ERROR,     /*!< An error occurred while seeking in the file. */
        KTX_FILE_UNEXPECTED_EOF, /*!< File does not have enough data to satisfy request. */
        KTX_FILE_WRITE_ERROR,    /*!< An error occurred while writing to the file. */
        KTX_GL_ERROR,            /*!< GL operations resulted in an error. */
        /// <summary>
        /// The operation is not allowed in the current state.
        /// </summary>
        KTX_INVALID_OPERATION,  
        KTX_INVALID_VALUE,       /*!< A parameter value was not valid. */
        KTX_NOT_FOUND,           /*!< Requested metadata key or required dynamically loaded GPU function was not found. */
        KTX_OUT_OF_MEMORY,       /*!< Not enough memory to complete the operation. */
        KTX_TRANSCODE_FAILED,    /*!< Transcoding of block compressed texture failed. */
        KTX_UNKNOWN_FILE_FORMAT, /*!< The file not a KTX file */
        KTX_UNSUPPORTED_TEXTURE_TYPE, /*!< The KTX file specifies an unsupported texture type. */
        KTX_UNSUPPORTED_FEATURE,  /*!< Feature not included in in-use library or not yet implemented. */
        KTX_LIBRARY_NOT_LINKED,  /*!< Library dependency (OpenGL or Vulkan) not linked into application. */
        KTX_DECOMPRESS_LENGTH_ERROR, /*!< Decompressed byte count does not match expected byte size */
        KTX_DECOMPRESS_CHECKSUM_ERROR, /*!< Checksum mismatch when decompressing */
        KTX_ERROR_MAX_ENUM = KTX_DECOMPRESS_CHECKSUM_ERROR /*!< For safety checks. */

    }

    /// <summary>
    /// The <see cref="KtxTextureCreateStorage"/> enum defines options for allocating image storage when creating a KTX texture.
    /// </summary>
    public enum KtxTextureCreateStorage : uint
    {
        /// <summary>
        /// Don't allocate any image storage.
        /// </summary>
        KTX_TEXTURE_CREATE_NO_STORAGE = 0,

        /// <summary>
        /// Allocate image storage.
        /// </summary>
        KTX_TEXTURE_CREATE_ALLOC_STORAGE = 1
    }

    /// <summary>
    /// Common Vulkan VkFormat values used when creating KTX2 textures.
    /// Only the most frequently used ones are included here.
    /// You can add more as needed.
    /// </summary>
    public enum VkFormat : uint
    {
        Undefined = 0,

        // 8-bit UNORM (linear) formats
        R8G8B8A8_UNORM = 37,

        // 8-bit SRGB formats (most common for color textures)
        R8G8B8A8_SRGB = 43,

        // Other useful formats
        R8Unorm = 9,
        R8Srgb = 13,
        R16G16B16A16Sfloat = 97,
        R32G32B32A32Sfloat = 109,

        // BC compressed formats (Block Compression)
        BC7UnormBlock = 145,
        BC7SrgbBlock = 146,

        // ETC2 / ASTC etc. can be added here if needed
    }
}
