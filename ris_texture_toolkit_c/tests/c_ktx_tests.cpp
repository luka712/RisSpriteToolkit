#define STB_IMAGE_IMPLEMENTATION

#include <catch2/catch_test_macros.hpp>
#include <c_ktx.hpp>
#include <iostream>
#include <filesystem>
#include "stb_image/stb_image.h"
#include <spdlog/spdlog.h>

const char* TEST_PNG = "test_files/test.png";
const char* TEST_KTX_BASIS_UASTC = "test_files/test_basis_uastc.ktx2";

//! Test if width is correct for test ktx image.
bool c_ktx_get_width()
{
	ktxTexture2* texture;
	auto error = ktxTexture2_CreateFromNamedFile(TEST_KTX_BASIS_UASTC, KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT, &texture);
	if (error == KTX_SUCCESS)
	{
		auto width = ris_ktxTexture2_GetWidth(texture);
		ktxTexture_Destroy(ktxTexture(texture));
		return width > 0;
	}
	else
	{
		std::cerr << "Error loading texture: " << error << std::endl;
		return false;
	}
}

bool c_ktx_get_height()
{
	ktxTexture2* texture;
	auto error = ktxTexture2_CreateFromNamedFile(TEST_KTX_BASIS_UASTC, KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT, &texture);
	if (error == KTX_SUCCESS)
	{
		auto height = ris_ktxTexture2_GetHeight(texture);
		ktxTexture_Destroy(ktxTexture(texture));
		return height > 0;
	}
	else
	{
		std::cerr << "Error loading texture: " << error << std::endl;
		return false;
	}
}

bool c_ktx_get_supercompression_scheme()
{
	ktxTexture2* texture;
	auto error = ktxTexture2_CreateFromNamedFile(TEST_KTX_BASIS_UASTC, KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT, &texture);
	if (error == KTX_SUCCESS)
	{
		auto scheme = ris_ktxTexture2_GetSupercompressionScheme(texture);
		ktxTexture_Destroy(ktxTexture(texture));
		return true;
	}
	else
	{
		std::cerr << "Error loading texture: " << error << std::endl;
		return false;
	}
}

ktxTexture2* createTexture(uint32_t width, uint32_t height)
{
	c_ktxTextureCreateInfo createInfo;
	createInfo.baseWidth = width;
	createInfo.baseHeight = height;
	createInfo.vkFormat = VK_FORMAT_R8G8B8A8_UNORM;

	ktxTexture2* texture;
	ktxTextureCreateStorageEnum storageAllocation = KTX_TEXTURE_CREATE_ALLOC_STORAGE;
	ris_ktxTexture2_Create(&createInfo, storageAllocation, &texture);
	return texture;
}

bool c_ktxTexture2_SetImageFromMemory()
{
	int width, height, channels;
	unsigned char* data = stbi_load(TEST_PNG, &width, &height, &channels, 0);

	ktxTexture2* texture = createTexture(width, height);
	if (data)
	{
		auto error = ris_ktxTexture2_SetImageFromMemory(texture, 0, 0, 0, data, width * height * channels);
		stbi_image_free(data);
		ris_ktxTexture2_Destroy(texture);
		return error == KTX_SUCCESS;
	}
	else
	{
		stbi_image_free(data);
		std::cerr << "Error loading image data: " << stbi_failure_reason() << std::endl;
		ris_ktxTexture2_Destroy(texture);
		return false;
	}
}

ktxTexture2* createAndFillTexture()
{
	int width, height, channels;
	unsigned char* data = stbi_load(TEST_PNG, &width, &height, &channels, 0);

	c_ktxTextureCreateInfo createInfo;
	createInfo.baseWidth = width;
	createInfo.baseHeight = height;
	createInfo.vkFormat = VK_FORMAT_R8G8B8A8_UNORM;

	ktxTexture2* texture;
	ktxTextureCreateStorageEnum storageAllocation = KTX_TEXTURE_CREATE_ALLOC_STORAGE;
	ris_ktxTexture2_Create(&createInfo, storageAllocation, &texture);

	ris_ktxTexture2_SetImageFromMemory(texture, 0, 0, 0, data, width * height * channels);

	stbi_image_free(data);

	return texture;
}

bool c_ktxTexture_WriteToNamedFile()
{
	ktxTexture2* texture = createAndFillTexture();
	c_ktxBasisParams params;
	params.uastc = KTX_TRUE;
	ris_ktxTexture2_CompressBasisEx(
		texture,
		&params
	);
	auto error = ris_ktxTexture_WriteToNamedFile(texture, "test_files/output_basis_uastc.ktx2");
	ris_ktxTexture2_Destroy(texture);
	return error == KTX_SUCCESS;
}

bool c_ktxTexture_LoadBasis_TranscodeToGPUFormat()
{
	ktxTexture2* texture;
	auto errorCode = ktxTexture2_CreateFromNamedFile(TEST_KTX_BASIS_UASTC, KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT, &texture);
	if (errorCode != KTX_SUCCESS)
	{
		auto strError = ktxErrorString(errorCode);
		spdlog::warn("Failed to load KTX texture from file '{}'. Error code: {}", TEST_KTX_BASIS_UASTC, strError);
		ris_ktxTexture2_Destroy(texture);
		return false;
	}

	bool needsTranscoding = ktxTexture2_NeedsTranscoding(texture);

	errorCode = ktxTexture2_TranscodeBasis(texture, KTX_TTF_BC7_RGBA, 0);
	if (errorCode != KTX_SUCCESS)
	{
		spdlog::warn("Failed to transcode KTX texture to GPU format. Error code: {}", ktxErrorString(errorCode));
		ris_ktxTexture2_Destroy(texture);
		return false;
	}

	ris_ktxTexture2_Destroy(texture);
	return true;
}


TEST_CASE("ktx tests", "[c_ktx_get_width, c_ktx_get_height, c_ktx_get_supercompression_scheme, c_ktxTexture2_SetImageFromMemory, c_ktxTexture_WriteToNamedFile, c_ktxTexture_LoadBasis_TranscodeToGPUFormat]")
{
	REQUIRE(c_ktx_get_width());
	REQUIRE(c_ktx_get_height());
	REQUIRE(c_ktx_get_supercompression_scheme());
	REQUIRE(c_ktxTexture2_SetImageFromMemory());
	REQUIRE(c_ktxTexture_WriteToNamedFile());
	REQUIRE(c_ktxTexture_LoadBasis_TranscodeToGPUFormat());
}
