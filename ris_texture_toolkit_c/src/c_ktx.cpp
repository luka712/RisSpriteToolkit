//
// Created by lukaa on 18.4.2026..
//

#include "c_ktx.hpp"

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

void ris_ktxTexture2_Destroy(const ktxTexture2* tex)
{
    ktxTexture_Destroy(ktxTexture(tex));
}
