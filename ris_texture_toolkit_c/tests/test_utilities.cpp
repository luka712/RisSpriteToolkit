#define STB_IMAGE_IMPLEMENTATION
#include "stb_image/stb_image.h"
#include "test_utilities.hpp"


unsigned char* loadTestPng(int* width, int* height, int* channels)
{
	return stbi_load(TEST_PNG, width, height, channels, 0);
}

void freeTestPng(unsigned char* data)
{
	stbi_image_free(data);
}
