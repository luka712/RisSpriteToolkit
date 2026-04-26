#include <catch2/catch_test_macros.hpp>
#include <c_ktx.hpp>
#include <iostream>
#include <filesystem>
#include <spdlog/spdlog.h>
#include "test_utilities.hpp"

bool ris_ktx_texture_write_unviversal_basis_test()
{
	int width, height, channels;
	auto data = loadTestPng(&width, &height, &channels);

	// Create a KTX texture.
	c_ktxTextureCreateInfo createInfo = {};
	createInfo.baseWidth = width;
	createInfo.baseHeight = height;
	createInfo.vkFormat = VK_FORMAT_R8G8B8A8_UNORM;

	ktxTexture2* texture;
	ktxTextureCreateStorageEnum storageAllocation = KTX_TEXTURE_CREATE_ALLOC_STORAGE; 
	auto error = ris_ktxTexture2_Create(&createInfo, storageAllocation, &texture);

	if (error != KTX_SUCCESS)
	{
		spdlog::warn("Failed to create KTX texture. Error code: {}", ktxErrorString(error));
		return false;
	}

	// Fill the texture with image data.
	ris_ktxTexture2_SetImageFromMemory(texture, 0, 0, 0, data, width * height * channels);
	
	// Compress the texture using Basis Universal.
	c_ktxBasisParams params = {};
	params.uastc = KTX_TRUE;
	ris_ktxTexture2_CompressBasisEx(
		texture,
		&params
	);

	// Write the compressed texture to a file.
	error = ris_ktxTexture2_WriteToNamedFile(texture, TEST_OUTPUT_KTX);
	ris_ktxTexture2_Destroy(texture);

	freeTestPng(data);
	return error == KTX_SUCCESS;
}

bool ris_ktx_texture_load_and_confirm_properties_test()
{
	ktxTexture2* texture;
	auto errorCode = ktxTexture2_CreateFromNamedFile(TEST_OUTPUT_KTX, KTX_TEXTURE_CREATE_NO_FLAGS, &texture);
	if (errorCode != KTX_SUCCESS)
	{
		auto strError = ktxErrorString(errorCode);
		spdlog::warn("Failed to load KTX texture from file '{}'. Error code: {}", TEST_OUTPUT_KTX, strError);
		ris_ktxTexture2_Destroy(texture);
		return false;
	}
	auto width = ris_ktxTexture2_GetWidth(texture);
	auto height = ris_ktxTexture2_GetHeight(texture);
	bool needsTranscoding = ris_ktxTexture2_NeedsTranscoding(texture);
	bool result = width > 0 && height > 0 && needsTranscoding;
	ris_ktxTexture2_Destroy(texture);
	return result;
}

TEST_CASE("ktx tests", "[ris_ktx_texture_write_unviversal_basis_test, ris_ktx_texture_load_and_confirm_properties_test]")
{
	REQUIRE(ris_ktx_texture_write_unviversal_basis_test());
	REQUIRE(ris_ktx_texture_load_and_confirm_properties_test());
}
