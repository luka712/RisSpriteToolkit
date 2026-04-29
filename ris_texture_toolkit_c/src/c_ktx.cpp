// src/c_ktx.cpp

#include "c_ktx.hpp"

// -----------------------------------------------------------------------------
// Creation & Destruction
// -----------------------------------------------------------------------------
#pragma region Creation_Destruction

API_EXPORT KTX_error_code ris_ktxTexture2_Create(
    const ris_ktxTextureCreateInfo* createInfo,
    ktxTextureCreateStorageEnum storageAllocation,
    ktxTexture2** outTexture)
{
    ktxTextureCreateInfo ktxCreateInfo = {};
    ktxCreateInfo.baseWidth = createInfo->baseWidth;
    ktxCreateInfo.baseHeight = createInfo->baseHeight;
    ktxCreateInfo.baseDepth = 1;
    ktxCreateInfo.numDimensions = 2;
    ktxCreateInfo.numLevels = 1;
    ktxCreateInfo.numLayers = 1;
    ktxCreateInfo.numFaces = 1;
    ktxCreateInfo.isArray = KTX_FALSE;
    ktxCreateInfo.generateMipmaps = KTX_FALSE;
    ktxCreateInfo.vkFormat = createInfo->vkFormat;
    ktxCreateInfo.pDfd = nullptr;
    ktxCreateInfo.glInternalformat = 0;

    return ktxTexture2_Create(&ktxCreateInfo, storageAllocation, outTexture);
}

API_EXPORT KTX_error_code ris_ktxTexture2_CreateFromNamedFile(
    const char* filename,
    ktxTextureCreateFlagBits flags,
    ktxTexture2** outTexture)
{
    return ktxTexture2_CreateFromNamedFile(filename, flags, outTexture);
}

API_EXPORT void ris_ktxTexture2_Destroy(const ktxTexture2* tex)
{
    ktxTexture_Destroy(ktxTexture(tex));
}

#pragma endregion

// -----------------------------------------------------------------------------
// File I/O
// -----------------------------------------------------------------------------
#pragma region File_IO

API_EXPORT KTX_error_code ris_ktxTexture2_WriteToNamedFile(
    const ktxTexture2* tex,
    const char* const dstname)
{
    return ktxTexture_WriteToNamedFile(ktxTexture(tex), dstname);
}

#pragma endregion

// -----------------------------------------------------------------------------
// Image Data Manipulation
// -----------------------------------------------------------------------------
#pragma region Image_Data_Manipulation

API_EXPORT KTX_error_code ris_ktxTexture2_SetImageFromMemory(
    ktxTexture2* tex,
    uint32_t level,
    uint32_t layer,
    uint32_t faceSlice,
    const uint8_t* src,
    size_t srcSize)
{
    return ktxTexture_SetImageFromMemory(ktxTexture(tex), level, layer, faceSlice, src, srcSize);
}

API_EXPORT KTX_error_code ris_ktxTexture2_CompressBasisEx(
    ktxTexture2* tex,
    const ris_ktxBasisParams* params)
{
    ktxBasisParams ktxParams = {};
    ktxParams.structSize = sizeof(ktxBasisParams);
    ktxParams.uastc = params->uastc;
    ktxParams.qualityLevel = params->qualityLevel;
    ktxParams.compressionLevel = params->compressionLevel;
    ktxParams.uastcFlags = KTX_PACK_UASTC_LEVEL_DEFAULT;
    ktxParams.threadCount = 0;
    ktxParams.uastcRDO = KTX_FALSE;

    return ktxTexture2_CompressBasisEx(tex, &ktxParams);
}

API_EXPORT KTX_error_code ris_ktxTexture2_TranscodeBasis(
    ktxTexture2* texture,
    ktx_transcode_fmt_e outputFormat,
    ktx_transcode_flags transcodeFlags)
{
    return ktxTexture2_TranscodeBasis(texture, outputFormat, transcodeFlags);
}

#pragma endregion

// -----------------------------------------------------------------------------
// Query Helpers
// -----------------------------------------------------------------------------
#pragma region Query_Helpers

API_EXPORT uint32_t ris_ktxTexture2_GetWidth(const ktxTexture2* tex)
{
    return tex->baseWidth;
}

API_EXPORT uint32_t ris_ktxTexture2_GetHeight(const ktxTexture2* tex)
{
    return tex->baseHeight;
}

API_EXPORT uint32_t ris_ktxTexture2_GetNumLevels(const ktxTexture2* tex)
{
    return tex->numLevels;
}

API_EXPORT VkFormat ris_ktxTexture2_GetVkFormat(const ktxTexture2* tex)
{
    return static_cast<VkFormat>(tex->vkFormat);
}

API_EXPORT uint8_t* ris_ktxTexture2_GetData(const ktxTexture2* tex)
{
    return ktxTexture_GetData(ktxTexture(tex));
}

API_EXPORT bool ris_ktxTexture2_NeedsTranscoding(ktxTexture2* tex)
{
    return ktxTexture2_NeedsTranscoding(tex) == KTX_TRUE;
}

API_EXPORT size_t ris_ktxTexture2_GetImageSize(const ktxTexture2* tex, uint32_t level)
{
    return ktxTexture_GetImageSize(ktxTexture(tex), level);
}

API_EXPORT ktxSupercmpScheme ris_ktxTexture2_GetSupercompressionScheme(ktxTexture2* tex)
{
    return tex->supercompressionScheme;
}

API_EXPORT KTX_error_code ris_ktxTexture2_GetImageOffset(
    const ktxTexture2* tex,
    uint32_t level,
    uint32_t layer,
    uint32_t faceSlice,
    size_t* pOffset)
{
    return ktxTexture_GetImageOffset(ktxTexture(tex), level, layer, faceSlice, pOffset);
}

#pragma endregion