#define STB_IMAGE_IMPLEMENTATION

#include <catch2/catch_test_macros.hpp>
#include <c_ktx.hpp>
#include <iostream>
#include <filesystem>
#include "stb_image/stb_image.h"

/**
* Note that test here assumes that KTX texture is BASISU/UASTC compressed, and that the file is located at "test_files/test.ktx2".
*/

//! Test if width is correct for test ktx image.
bool c_ktx_get_width()
{
	ktxTexture2* texture;
	auto error = ktxTexture2_CreateFromNamedFile("test_files/test.ktx2", KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT, &texture);
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
	auto error = ktxTexture2_CreateFromNamedFile("test_files/test.ktx2", KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT, &texture);
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
	auto error = ktxTexture2_CreateFromNamedFile("test_files/test.ktx2", KTX_TEXTURE_CREATE_LOAD_IMAGE_DATA_BIT, &texture);
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
	ktxTextureCreateInfo createInfo;
	createInfo.baseWidth = width;
	createInfo.baseHeight = height;
	createInfo.pDfd = nullptr;
	createInfo.vkFormat = 37; // VK_FORMAT_R8G8B8A8_UNORM
	createInfo.baseDepth = 1;
	createInfo.numDimensions = 2;
	createInfo.numLevels = 1;
	createInfo.numLayers = 1;
	createInfo.numFaces = 1;
	createInfo.isArray = KTX_FALSE;
	createInfo.generateMipmaps = KTX_FALSE;

	ktxTexture2* texture;
	ktxTextureCreateStorageEnum storageAllocation = KTX_TEXTURE_CREATE_ALLOC_STORAGE;
	ktxTexture2_Create(&createInfo, storageAllocation, &texture);
	return texture;
}

bool c_ktxTexture2_SetImageFromMemory()
{
	int width, height, channels;
	unsigned char* data = stbi_load("test_files/png_test.png", &width, &height, &channels, 0);

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
	unsigned char* data = stbi_load("test_files/png_test.png", &width, &height, &channels, 0);

	ktxTextureCreateInfo createInfo;
	createInfo.baseWidth = width;
	createInfo.baseHeight = height;
	createInfo.pDfd = nullptr;
	createInfo.vkFormat = 37; // VK_FORMAT_R8G8B8A8_UNORM
	createInfo.baseDepth = 1;
	createInfo.numDimensions = 2;
	createInfo.numLevels = 1;
	createInfo.numLayers = 1;
	createInfo.numFaces = 1;
	createInfo.isArray = KTX_FALSE;
	createInfo.generateMipmaps = KTX_FALSE;

	ktxTexture2* texture;
	ktxTextureCreateStorageEnum storageAllocation = KTX_TEXTURE_CREATE_ALLOC_STORAGE;
	ktxTexture2_Create(&createInfo, storageAllocation, &texture);

	ris_ktxTexture2_SetImageFromMemory(texture, 0, 0, 0, data, width * height * channels);

	ktxTexture2_CompressBasis(
		texture,
		KTX_TTF_BC7_RGBA
	);

	stbi_image_free(data);

	return texture;
}

bool c_ktxTexture_WriteToNamedFile()
{
	ktxTexture2* texture = createAndFillTexture();
	auto error = ris_ktxTexture_WriteToNamedFile(texture, "test_files/output.ktx2");
	ris_ktxTexture2_Destroy(texture);
	return error == KTX_SUCCESS;
}


TEST_CASE("ktx tests", "[c_ktx_get_width, c_ktx_get_height, c_ktx_get_supercompression_scheme, c_ktxTexture2_SetImageFromMemory, c_ktxTexture_WriteToNamedFile]")
{
	REQUIRE(c_ktx_get_width());
	REQUIRE(c_ktx_get_height());
	REQUIRE(c_ktx_get_supercompression_scheme());
	REQUIRE(c_ktxTexture2_SetImageFromMemory());
	REQUIRE(c_ktxTexture_WriteToNamedFile());
}
