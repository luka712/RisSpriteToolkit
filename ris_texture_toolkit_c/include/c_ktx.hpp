//
// Created by lukaa on 18.4.2026.
//

#include "ktx.h"
#include "macros.hpp"
#include <cstdint>
#include <vulkan/vulkan.h>

extern "C" {

	struct c_ktxTextureCreateInfo
    {
    	uint32_t baseWidth;
        uint32_t baseHeight;
		VkFormat vkFormat;
    };

	struct c_ktxBasisParams
	{
		bool uastc;
	};

	API_EXPORT
		KTX_error_code ris_ktxTexture2_Create(const c_ktxTextureCreateInfo* createInfo, ktxTextureCreateStorageEnum storageAllocation, ktxTexture2** outTexture);

	API_EXPORT
		uint32_t ris_ktxTexture2_GetWidth(const ktxTexture2* tex);

	API_EXPORT
		uint32_t ris_ktxTexture2_GetHeight(const ktxTexture2* tex);

	API_EXPORT
		uint8_t* ris_ktxTexture2_GetData(const ktxTexture2* tex);

	API_EXPORT
		size_t ris_ktxTexture2_GetImageSize(const ktxTexture2* tex, uint32_t level);

	API_EXPORT
		ktxSupercmpScheme ris_ktxTexture2_GetSupercompressionScheme(const ktxTexture2* tex);

	API_EXPORT
		KTX_error_code ris_ktxTexture2_SetImageFromMemory(const ktxTexture2* tex,
			uint32_t level,
			uint32_t layer,
			uint32_t faceSlice,
			const uint8_t* src,
			size_t srcSize);

	API_EXPORT
		KTX_error_code ris_ktxTexture2_WriteToNamedFile(const ktxTexture2* tex, const char* const dstname);

	API_EXPORT
		KTX_error_code ris_ktxTexture2_CompressBasisEx(ktxTexture2* tex, const c_ktxBasisParams* params);

	API_EXPORT
		KTX_error_code ris_ktxTexture2_GetImageOffset(const ktxTexture2* tex, uint32_t level, uint32_t layer, uint32_t faceSlice, size_t* pOffset);

	API_EXPORT
		void ris_ktxTexture2_Destroy(const ktxTexture2* tex);
}

