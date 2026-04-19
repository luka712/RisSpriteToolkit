//
// Created by lukaa on 18.4.2026.
//

#include "ktx.h"
#include "macros.hpp"
#include <cstdint>

extern "C" {

	API_EXPORT
		uint32_t ris_ktxTexture2_GetWidth(const ktxTexture2* tex);

	API_EXPORT
		uint32_t ris_ktxTexture2_GetHeight(const ktxTexture2* tex);

	API_EXPORT
		ktxSupercmpScheme ris_ktxTexture2_GetSupercompressionScheme(const ktxTexture2* tex);

	API_EXPORT
		KTX_error_code ris_ktxTexture2_SetImageFromMemory(const ktxTexture2* tex,
			ktx_uint32_t level,
			ktx_uint32_t layer,
			ktx_uint32_t faceSlice,
			const ktx_uint8_t* src,
			ktx_size_t srcSize);

	API_EXPORT
		KTX_error_code ris_ktxTexture_WriteToNamedFile(const ktxTexture2* tex, const char* const dstname);

	API_EXPORT
		void ris_ktxTexture2_Destroy(const ktxTexture2* tex);
}

