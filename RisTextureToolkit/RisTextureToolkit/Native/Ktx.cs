using RisTextureToolkit.Ktx;
using System.Runtime.InteropServices;

namespace RisTextureToolkit.Native
{
    internal static class Ktx
    {
        private const string DLL_NAME = "ktx";

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern KtxErrorCode ktxTexture2_CreateFromNamedFile(
           string filename,
           uint flags,
           out IntPtr texture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern KtxErrorCode ktxTexture2_TranscodeBasis(
            IntPtr texture,
            int format,
            uint flags);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ktxTexture_GetData(IntPtr texture);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern ulong ktxTexture_GetDataSize(IntPtr texture);


        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ktxTexture2_NeedsTranscoding(IntPtr texture);
    }
}
