using TorchSharp;
using static TorchSharp.torch;
using OpenCV.Net;
using System.Runtime.InteropServices;
using System;

namespace MidasTorchModel
{
    public class OpenCVImager
    {
        internal delegate void GCHandleDeleter(IntPtr memory);
        
        [DllImport("LibTorchSharp")]
        internal static extern IntPtr THSTensor_data(IntPtr handle);

        [DllImport("LibTorchSharp")]
        internal static extern IntPtr THSTensor_new(IntPtr rawArray, GCHandleDeleter deleter, IntPtr dimensions, int numDimensions, sbyte type, sbyte dtype, int deviceType, int deviceIndex, [MarshalAs(UnmanagedType.U1)] bool requires_grad);

        public static Tensor ToTensor(IplImage image)
        {
            int width = image.Width;
            int height = image.Height;
            int channels = image.Channels;
            int widthStep = image.WidthStep;

            if (channels == 1)
            {
                return tensor(ToBytes(image), 1L, height, width);
            }

            using (NewDisposeScope())
            {              
                int size = height * width;

                byte[] arrayB = new byte[size];
                byte[] arrayG = new byte[size];
                byte[] arrayR = new byte[size];

                var imageBytes = ToBytes(image);

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int pixelIndex = y * widthStep + x * channels;
                        int arrayIndex = y * width + x;

                        arrayB[arrayIndex] = imageBytes[pixelIndex];
                        arrayG[arrayIndex] = imageBytes[pixelIndex + 1];
                        arrayR[arrayIndex] = imageBytes[pixelIndex + 2];
                    }
                }

                Tensor tensorR = torch.tensor(arrayR, new long[] { 1, image.Height, image.Width });
                Tensor tensorG = torch.tensor(arrayG, new long[] { 1, image.Height, image.Width });
                Tensor tensorB = torch.tensor(arrayB, new long[] { 1, image.Height, image.Width });

                var tensor = cat(new Tensor[] { tensorR, tensorG, tensorB }, 0);

                return tensor.MoveToOuterDisposeScope();
            }
        }

        public unsafe static IplImage ToImage(Tensor tensor)
        {
            if (tensor.Dimensions != 3)
            {
                throw new ArgumentException("Tensor must have 3 dimensions (channels, height, width)");
            }

            if (tensor.dtype != ScalarType.Byte)
            {
                throw new ArgumentException("Tensor element type must be byte for IplDepth.U8");
            }

            if (!tensor.is_contiguous())
            {
                throw new ArgumentException("Tensor must be contiguous");
            }

            var channels = (int)tensor.shape[0];
            var height = (int)tensor.shape[1];
            var width = (int)tensor.shape[2];

            var res = THSTensor_data(tensor.Handle);
            var image = new IplImage(new OpenCV.Net.Size(width, height), IplDepth.U8, channels, res);

            IplImage output = new IplImage(new OpenCV.Net.Size(width, height), IplDepth.U8, channels);
            CV.Copy(image, output);

            return output;

        }

        private static byte[] ToBytes(IplImage image)
        {
            var imageBytes = new byte[image.WidthStep * image.Height];
            Marshal.Copy(image.ImageData, imageBytes, 0, imageBytes.Length);
            return imageBytes;
        }
    }
}