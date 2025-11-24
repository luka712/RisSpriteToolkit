using OpenCvSharp;

namespace Ris.SpriteToolkit;

/// <summary>
/// Extensions for OpenCV Mat.
/// </summary>
internal static class OpenCVMatExtensions
{
    /// <summary>
    /// Converts the Mat to RGBA format.
    /// </summary>
    /// <param name="mat">The <see cref="OpenCvSharp.Mat"/>The mat of opencv.</param>
    /// <exception cref="NotSupportedException">
    /// If the number of channels is not supported.
    /// </exception>
    internal static void ToRGBA(this OpenCvSharp.Mat mat)
    {
        if (mat.Channels() == 4)
        {
            return;
        }
        else if (mat.Channels() == 3)
        {
            OpenCvSharp.Cv2.CvtColor(mat, mat, OpenCvSharp.ColorConversionCodes.BGR2RGBA);

            // Fill alpha channel to 255
            for (int y = 0; y < mat.Rows; y++)
            {
                for (int x = 0; x < mat.Cols; x++)
                {
                    Vec3b pixel = mat.At<Vec3b>(y, x);
                    mat.Set(y, x, new Vec4b(pixel[0], pixel[1], pixel[2], 255));
                }
            }
        }
        else if (mat.Channels() == 1)
        {
            OpenCvSharp.Cv2.CvtColor(mat, mat, OpenCvSharp.ColorConversionCodes.GRAY2RGBA);
        }
        else
        {
            throw new NotSupportedException($"Unsupported number of channels: {mat.Channels()}");
        }
    }
}
