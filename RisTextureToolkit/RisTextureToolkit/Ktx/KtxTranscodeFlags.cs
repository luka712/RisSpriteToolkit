namespace RisTextureToolkit.Ktx
{
    /// <summary>
    /// The KtxTranscodeFlags enumeration defines flags that can be used to control the transcoding process when converting BasisU/ETC1S or UASTC compressed textures to other formats. These flags can specify options such as how to handle non-power-of-two textures, whether to transcode alpha data for opaque formats, and whether to request higher quality transcoding for certain formats.
    /// </summary>
    public enum KtxTranscodeFlags : uint
    {
        /// <summary>
        /// No special transcoding options.
        NONE = 0,

        KTX_TF_PVRTC_DECODE_TO_NEXT_POW2 = 2,
        /*!< PVRTC1: decode non-pow2 ETC1S texture level to the next larger
             power of 2 (not implemented yet, but we're going to support it).
             Ignored if the slice's dimensions are already a power of 2.
         */
        KTX_TF_TRANSCODE_ALPHA_DATA_TO_OPAQUE_FORMATS = 4,
        /*!< When decoding to an opaque texture format, if the Basis data has
             alpha, decode the alpha slice instead of the color slice to the
             output texture format. Has no effect if there is no alpha data.
         */
        KTX_TF_HIGH_QUALITY = 32,
        /*!< Request higher quality transcode of UASTC to BC1, BC3, ETC2_EAC_R11 and
             ETC2_EAC_RG11. The flag is unused by other UASTC transcoders.
         */
    }
}
