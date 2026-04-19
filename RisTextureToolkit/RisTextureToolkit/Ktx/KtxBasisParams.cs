
using System.Runtime.InteropServices;

namespace RisTextureToolkit.Ktx
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public  struct KtxBasisParams
    {
        /// <summary>
        /// Specifies whether to use UASTC compression. 
        /// </summary>
        public bool UseUastc { get; set; }
    }

}
