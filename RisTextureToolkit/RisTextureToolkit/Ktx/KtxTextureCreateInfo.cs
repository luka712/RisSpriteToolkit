using System.Runtime.InteropServices;

namespace RisTextureToolkit.Ktx
{
    // This struct should match the native layout exactly
    [StructLayout(LayoutKind.Sequential)]
    public struct KtxTextureCreateInfo
    {
        public uint glInternalformat;     // Ignored for KTX2
        public VkFormat vkFormat;             // Preferred for KTX2 (VkFormat)
        public IntPtr pDfd;               // Pointer to DFD (usually null if using vkFormat)
        public uint baseWidth;
        public uint baseHeight;
        public uint baseDepth;
        public uint numDimensions;        // 1, 2 or 3
        public uint numLevels;            // Number of mip levels
        public uint numLayers;
        public uint numFaces;             // 6 for cubemap, 1 otherwise
        public bool isArray;              // 1 = true, 0 = false  (ktx_bool_t is uint8_t)
        public bool generateMipmaps;      // 1 = true, 0 = false
    }
}
