//using System.Drawing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MakeSprite
{
    public abstract class N64Encoding
    {
        public abstract int BitsPerPixel { get; }
        public abstract Format Format { get; }

        public abstract MemoryStream ConvertImage(Image<Rgba32> image);
        public abstract Image<Rgba32> ConvertSprite(Sprite sprite);


        public static int GetDataSize(Image<Rgba32> bitmap, N64Encoding encoding)
        {
            var width = bitmap.Width;
            var height = bitmap.Height;
            var bpp = encoding.BitsPerPixel;
            var size = (int)(width * height * bpp / 8.0);
            return size;
        }

        // TODO: move elsewhere
        public static N64Encoding FormatToEncoding(Format format)
        {
            switch (format)
            {
                case Format.RGBA32: return EncodingRGBA32.Encoding;

                default:
                    throw new NotImplementedException();
            }
        }

        public static byte FormatToBitsPerPixel(Format format)
        {
            switch (format)
            {
                case Format.RGBA32:
                    return 32;

                case Format.RGBA16:
                    return 16;

                case Format.CI8:
                case Format.I8:
                case Format.IA8:
                    return 8;

                case Format.CI4:
                case Format.I4:
                case Format.IA4:
                    return 4;

                default:
                    throw new NotImplementedException();
            }
        }

        public static byte FormatToBytesPerPixel(Format format)
        {
            switch (format)
            {
                case Format.RGBA32:
                    return 4;

                case Format.RGBA16:
                    return 2;

                case Format.CI8:
                case Format.I8:
                case Format.IA8:
                    return 1;

                case Format.CI4:
                case Format.I4:
                case Format.IA4:
                    throw new NotImplementedException("I don't actually know the answer. 0? 1?");
                    //return 1;

                default:
                    throw new NotImplementedException();
            }
        }

        public static double Rgba32ToIntensity(Rgba32 rgba32)
        {
            var grayscale =
                rgba32.R * 0.299 +
                rgba32.G * 0.587 +
                rgba32.B * 0.114;

            return grayscale;
        }

        public static byte Rgba32ToIntensity8(Rgba32 rgba32)
        {
            byte value = (byte)(Rgba32ToIntensity(rgba32) * byte.MaxValue);
            return value;
        }

    }
}
