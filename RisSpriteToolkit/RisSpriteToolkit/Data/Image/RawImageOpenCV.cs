using System.Runtime.InteropServices;
using OpenCvSharp;

namespace RisSpriteToolkit.Data.Image
{
    /// <summary>
    /// The raw image class for OpenCV images.
    /// </summary>
    public class RawImageOpenCV : RawImage
    {
        private byte[]? bytes;

        /// <summary>
        /// The constructor to create a RawImage from an OpenCV Mat.
        /// </summary>
        /// <param name="openCVImg">The open cv image.</param>
        internal RawImageOpenCV(string filePath, Mat openCVImg)
            : base(filePath, openCVImg.Width, openCVImg.Height, Array.Empty<byte>(), (byte)openCVImg.Channels())
        {
            OpenCVMat = openCVImg;
        }


        /// <summary>
        /// The underlying OpenCV Mat image.
        /// </summary>
        public Mat OpenCVMat { get; }

        /// <inheritdoc/>
        public override byte[] Data
        {
            get
            {
                // Lazy load the byte array only when accessed. No need to store it twice.
                if (bytes == null)
                {
                    // Convert Mat to byte array
                    bytes = new byte[OpenCVMat.Total() * OpenCVMat.ElemSize()];
                    Marshal.Copy(OpenCVMat.Data, bytes, 0, bytes.Length);
                }
                return bytes;
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            OpenCVMat.Dispose();
            base.Dispose();
        }
    }
}