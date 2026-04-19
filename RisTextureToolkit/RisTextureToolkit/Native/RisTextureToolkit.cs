using RisTextureToolkit.Ktx;
using System.Runtime.InteropServices;

namespace RisTextureToolkit.Native
{
    public static class RisTextureToolkit
    {
        private const string DLL_NAME = "ris_texture_toolkit";


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern KtxErrorCode ris_ktxTexture2_Create(in KtxTextureCreateInfo createInfo, uint storageAllocation, out IntPtr texture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ris_ktxTexture2_Destroy(IntPtr texture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern KtxErrorCode ris_ktxTexture2_SetImageFromMemory(
            IntPtr This,                    // ktxTexture2*
            uint level,
            uint layer,
            uint faceSlice,
            IntPtr src,                     // const ktx_uint8_t*
            ulong srcSize                   // ktx_size_t  (size_t = ulong on 64-bit)
        );

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ris_ktxTexture2_GetWidth(IntPtr texture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ris_ktxTexture2_GetHeight(IntPtr texture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern KtxErrorCode ris_ktxTexture2_GetImageOffset(
            IntPtr texture,
            uint level,
            uint layer,
            uint faceSlice,
            out ulong offset                // ktx_size_t* (size_t = ulong on 64-bit)
        );

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ris_ktxTexture2_GetImageData(IntPtr texture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong ris_ktxTexture2_GetImageSize(IntPtr texture, uint level);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern KtxErrorCode ris_ktxTexture2_WriteToNamedFile(IntPtr texture, string filename);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern KtxErrorCode ris_ktxTexture2_CompressBasisEx(IntPtr texture, IntPtr basisParams);
    }
}
