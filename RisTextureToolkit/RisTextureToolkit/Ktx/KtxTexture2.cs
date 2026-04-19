using System.Runtime.InteropServices;
using static RisTextureToolkit.Native.Ktx;
using static RisTextureToolkit.Native.RisTextureToolkit;

namespace RisTextureToolkit.Ktx
{
    /// <summary>
    /// The KtxTexture class represents a texture loaded from a KTX file. 
    /// It provides methods for loading, transcoding, and retrieving texture data.
    /// The class manages the lifecycle of the native KTX texture object, ensuring that resources are properly released when the texture is no longer needed. 
    /// It also includes error handling to provide informative exceptions when operations fail, such as loading or transcoding errors.
    /// </summary>
    public class KtxTexture2 : IDisposable
    {
       
       

        private static bool _firstLoad = true;

        /// <summary>
        /// The constructor for the KtxTexture class. It attempts to load a KTX texture from the specified file path. 
        /// If the loading process fails, it throws an exception with a descriptive error message.
        /// </summary>
        /// <param name="filePath">The file path to the .ktx or .ktx2 texture.</param>
        /// <param name="transcodeFormat">The desired transcode format for the texture.</param>
        /// <param name="createFlags">The <see cref="KtxTextureCreateFlags"/>. By default, it is <c>TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT</c>.</param>
        /// <param name="transcodeFlags">The <see cref="KtxTranscodeFlags"/>. By default, it is <c>0</c>.</param>
        /// <exception cref="Exception">
        /// If the texture fails to load from the specified file path, an exception is thrown with details about the failure.
        /// </exception>
        public KtxTexture2(string filePath, KtxTranscodeFormat transcodeFormat,
            KtxTextureCreateFlags createFlags = KtxTextureCreateFlags.TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT,
            KtxTranscodeFlags transcodeFlags = KtxTranscodeFlags.NONE)
        {
            NativeResolver.Setup();
            filePath = Path.GetFullPath(filePath);

            TexturePtr = IntPtr.Zero;

            KtxErrorCode errorCode = ktxTexture2_CreateFromNamedFile(filePath, (uint)createFlags, out IntPtr texture);
            if (errorCode == KtxErrorCode.FILE_OPEN_FAILED)
            {
                throw new FileNotFoundException($"The specified KTX file '{filePath}' could not be found.");
            }
            else if (errorCode != KtxErrorCode.KTX_SUCCESS)
            {
                throw new Exception($"Failed to load KTX texture from '{filePath}'. Error code: {errorCode}");
            }
            TexturePtr = texture;

            errorCode = ktxTexture2_TranscodeBasis(TexturePtr, (int)transcodeFormat, (uint)transcodeFlags);
            if (errorCode == KtxErrorCode.KTX_INVALID_OPERATION)
            {
                throw new InvalidOperationException($"The specified transcode format '{transcodeFormat}' is not valid for transcoding the KTX texture.");
            }
            if (errorCode != KtxErrorCode.KTX_SUCCESS)
            {
                throw new Exception($"Failed to transcode KTX texture. Error code: {errorCode}");
            }
        }

        /// <summary>
        /// Creates a KTX texture using the specified creation information.
        /// </summary>
        /// <param name="createInfo">The <see cref="KtxTextureCreateInfo"/>.</param>
        /// <param name="storageAllocation">The <see cref="KtxTextureCreateStorage"/>.</param>
        /// <exception cref="Exception">
        /// Throws an exception if the texture creation fails, providing details about the error code returned by the native function.
        /// </exception>
        public KtxTexture2(KtxTextureCreateInfo createInfo, KtxTextureCreateStorage storageAllocation = KtxTextureCreateStorage.KTX_TEXTURE_CREATE_ALLOC_STORAGE)
        {
            NativeResolver.Setup();
            TexturePtr = IntPtr.Zero;
            uint storageAllocValue = (uint)storageAllocation;
            KtxErrorCode errorCode = ktxTexture2_Create(createInfo, storageAllocValue, out IntPtr texture);
            if (errorCode != KtxErrorCode.KTX_SUCCESS)
            {
                throw new Exception($"Failed to create KTX texture. Error code: {errorCode}");
            }
            TexturePtr = texture;
        }

        /// <summary>
        /// The pointer to the native KTX texture object.
        /// This should be released using the appropriate native function when no longer needed.
        /// </summary>
        internal IntPtr TexturePtr { get; private set; }

        /// <summary>
        /// Gets the width of the texture.
        /// </summary>
        public uint Width => ktxTexture2_GetBaseWidth(TexturePtr);

        /// <summary>
        /// Gets the height of the texture.
        /// </summary>
        public uint Height => ktxTexture2_GetBaseHeight(TexturePtr);

        /// <summary>
        /// Sets the image data for a specific level, layer, and face/slice of the texture from a byte array in memory.
        /// </summary>
        /// <param name="level">
        /// The mipmap level of the texture to set the image data for. 
        /// Level 0 corresponds to the base level, and higher levels correspond to mipmap levels.
        /// </param>
        /// <param name="layer">
        /// The layer of the texture to set the image data for.
        /// Layer 0 corresponds to the first layer, and higher layers correspond to additional layers in array textures or 3D textures.
        /// </param>
        /// <param name="faceSlice">
        /// The face or slice of the texture to set the image data for.
        /// For cubemap textures, this corresponds to the specific face (e.g., positive X, negative X, positive Y, etc.).
        /// </param>
        /// <param name="src">The source data.</param>
        /// <param name="srcSize">The source data size.</param>
        /// <exception cref="Exception">
        /// If the operation to set the image data fails, an exception is thrown with details about the error code returned by the native function.
        /// </exception>
        public void SetImageFromMemory(uint level, uint layer, uint faceSlice, byte[] src, ulong srcSize)
        {
            unsafe
            {
                fixed (byte* srcPtr = src)
                {
                    KtxErrorCode errorCode = ris_ktxTexture2_SetImageFromMemory(TexturePtr, level, layer, faceSlice, (nint) srcPtr, srcSize);

                    if(errorCode == KtxErrorCode.KTX_INVALID_OPERATION)
                    {
                        throw new InvalidOperationException($"No storage was allocated when the texture was created.");
                    }

                    if (errorCode != KtxErrorCode.KTX_SUCCESS)
                    {
                        throw new Exception($"Failed to set image data for KTX texture. Error code: {errorCode}");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the raw texture data from the native KTX texture object.
        /// </summary>
        /// <returns>The texture data as byte array.</returns>
        public byte[] GetTextureData()
        {
            IntPtr dataPtr = ktxTexture_GetData(TexturePtr);
            ulong dataSize = ktxTexture_GetDataSize(TexturePtr);
            byte[] data = new byte[dataSize];
            Marshal.Copy(dataPtr, data, 0, (int)dataSize);
            return data;
        }

        /// <summary>
        /// Writes the KTX texture to a file at the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void WriteToNamedFile(string filePath)
        {
            KtxErrorCode errorCode = ris_ktxTexture_WriteToNamedFile(TexturePtr, filePath);
            if (errorCode != KtxErrorCode.KTX_SUCCESS)
            {
                throw new Exception($"Failed to write KTX texture to file '{filePath}'. Error code: {errorCode}");
            }
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            if (TexturePtr != IntPtr.Zero)
            {
                ris_ktxTexture2_Destroy(TexturePtr);
                TexturePtr = IntPtr.Zero;
            }
        }
    }
}
