#include "c_ktx.hpp"

API_EXPORT KTX_error_code ris_ktxTexture2_Create(const c_ktxTextureCreateInfo* createInfo, ktxTextureCreateStorageEnum storageAllocation, ktxTexture2** outTexture)
{
	ktxTextureCreateInfo ktxCreateInfo;
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
	ktxCreateInfo.glInternalformat = 0; // Ignored for ktxTexture2

	return ktxTexture2_Create(&ktxCreateInfo, storageAllocation, outTexture);
}

uint32_t ris_ktxTexture2_GetWidth(const ktxTexture2* ktxData)
{
    return ktxData->baseWidth;
}


uint32_t ris_ktxTexture2_GetHeight(const ktxTexture2* ktxData)
{
    return ktxData->baseHeight;
}

ktxSupercmpScheme ris_ktxTexture2_GetSupercompressionScheme(const ktxTexture2* tex)
{
    return tex->supercompressionScheme;
}

KTX_error_code ris_ktxTexture2_SetImageFromMemory(const ktxTexture2* tex, uint32_t level, uint32_t layer, uint32_t faceSlice, const uint8_t* src, size_t srcSize)
{
	return ktxTexture_SetImageFromMemory(ktxTexture(tex), level, layer, faceSlice, src, srcSize);
}

KTX_error_code ris_ktxTexture_WriteToNamedFile(const ktxTexture2* tex, const char* const dstname)
{
	return ktxTexture_WriteToNamedFile(ktxTexture(tex), dstname);
}

KTX_error_code ris_ktxTexture2_CompressBasisEx(ktxTexture2* tex, const c_ktxBasisParams* params)
{
	ktxBasisParams ktxParams = {};
	ktxParams.structSize = sizeof(ktxBasisParams);
	ktxParams.uastc = params->uastc;
	ktxParams.threadCount = 0; // auto
	ktxParams.uastcFlags = KTX_PACK_UASTC_LEVEL_DEFAULT;

	// optional safety defaults
	ktxParams.uastcRDO = KTX_FALSE;

	return ktxTexture2_CompressBasisEx(tex, &ktxParams);
}

void ris_ktxTexture2_Destroy(const ktxTexture2* tex)
{
    ktxTexture_Destroy(ktxTexture(tex));
}
