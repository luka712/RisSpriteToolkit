using RisTextureToolkit.Ktx;
using System.Runtime.InteropServices;

namespace RisTextureToolkit.Native
{
    public static class RisTextureToolkit
    {
        private const string DLL_NAME = "ris_texture_toolkit";

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
        internal static extern KtxErrorCode ris_ktxTexture_WriteToNamedFile(IntPtr texture, string filename);
    }
}
