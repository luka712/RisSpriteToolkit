#include <catch2/catch_test_macros.hpp>
#include <c_basisu.hpp>
#include <iostream>
#include <filesystem>
#include "stb_image/stb_image.h"


bool create_ktx_basisu_encoded_texture()
{
	int width, height, channels;
	unsigned char* data = stbi_load("test_files/png_test.png", &width, &height, &channels, 0);

	basisu::image img;
	img.init(data, width, height, channels);

	basisu::basis_compressor_params params;
	params.m_source_images.resize(1);
	params.m_source_images[0] = img;
	params.m_out_filename = "test_files/output.ktx2";

	// IMPORTANT SETTINGS
	params.m_create_ktx2_file = true;          // Write KTX2
	params.m_uastc = true;         // High quality (set false for ETC1S)
	params.m_mip_gen = false;       // Generate mipmaps

	// Optional settings
	params.m_pack_uastc_flags = basisu::cPackUASTCLevelDefault; // Fastest UASTC encoding (set cPackUASTCLevelDefault for better quality)
	params.m_rdo_uastc = true;     // Enable RDO (smaller size)
	params.m_rdo_uastc_quality_scalar = 1.0f;

	// Create compressor
	basisu::basis_compressor compressor;

	if (!compressor.init(params))
	{
		printf("Failed to init compressor\n");
		stbi_image_free(data);
		return false;
	}

	// Run compression + KTX2 writing
	auto result = compressor.process();

	stbi_image_free(data);

	if (result != basisu::basis_compressor::cECSuccess)
	{
		printf("Compression failed: %d\n", result);
		return false;
	}

	return true;
}


TEST_CASE("basisu tests", "[create_ktx_basisu_encoded_texture]")
{
	REQUIRE(create_ktx_basisu_encoded_texture());
}
